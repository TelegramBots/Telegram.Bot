using System.Reflection;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Telegram.Bot.Passport;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class PassportDecrypterBenchmarks
{
    private delegate void FindDataKeyAndIvDelegate(byte[] secret, byte[] hash, out byte[] dataKey, out byte[] dataIv);

    private static readonly FindDataKeyAndIvDelegate OptimizedFindDataKeyAndIv = CreateFindDataKeyAndIvDelegate();

    private readonly byte[] secret = Enumerable.Range(0, 32).Select(i => (byte)i).ToArray();
    private readonly byte[] hash = Enumerable.Range(32, 32).Select(i => (byte)i).ToArray();

    [Benchmark(Baseline = true)]
    public int Baseline_FindDataKeyAndIv()
    {
        byte[] secretAndHashBytes = new byte[secret.Length + hash.Length];
        Buffer.BlockCopy(secret, 0, secretAndHashBytes, 0, secret.Length);
        Buffer.BlockCopy(hash, 0, secretAndHashBytes, secret.Length, hash.Length);

        using var sha512 = SHA512.Create();
        byte[] dataSecretHash = sha512.ComputeHash(secretAndHashBytes);

        byte[] dataKey = new byte[32];
        byte[] dataIv = new byte[16];
        Buffer.BlockCopy(dataSecretHash, 0, dataKey, 0, 32);
        Buffer.BlockCopy(dataSecretHash, 32, dataIv, 0, 16);

        return dataKey[0] ^ dataIv[0];
    }

    [Benchmark]
    public int Optimized_FindDataKeyAndIv()
    {
        OptimizedFindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

        return dataKey[0] ^ dataIv[0];
    }

    // TODO: Add DecryptDataStreamAsync padding-path benchmarks with deterministic encrypted
    // content and credentials once reusable Passport test vectors are available.

    private static FindDataKeyAndIvDelegate CreateFindDataKeyAndIvDelegate()
    {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

        MethodInfo method = typeof(Decrypter).GetMethod("FindDataKeyAndIv", bindingFlags)
            ?? throw new MissingMethodException(typeof(Decrypter).FullName, "FindDataKeyAndIv");

        return method.CreateDelegate<FindDataKeyAndIvDelegate>();
    }
}
