// ReSharper disable InconsistentNaming

using System.Security.Cryptography;

namespace Quickstart
{
    /// <summary>
    /// Represents the standard parameters of RSA algorithm
    /// </summary>
    public struct EncryptionKeyParameters
    {
        public byte[] E;
        public byte[] M;
        public byte[] P;
        public byte[] Q;
        public byte[] DP;
        public byte[] DQ;
        public byte[] IQ;
        public byte[] D;

        public static explicit operator EncryptionKeyParameters(RSAParameters parameters) =>
            new EncryptionKeyParameters
            {
                E = parameters.Exponent,
                M = parameters.Modulus,
                P = parameters.P,
                Q = parameters.Q,
                DP = parameters.DP,
                DQ = parameters.DQ,
                IQ = parameters.InverseQ,
                D = parameters.D,
            };

        public static explicit operator RSAParameters(EncryptionKeyParameters parameters) =>
            new RSAParameters
            {
                Exponent = parameters.E,
                Modulus = parameters.M,
                P = parameters.P,
                Q = parameters.Q,
                DP = parameters.DP,
                DQ = parameters.DQ,
                InverseQ = parameters.IQ,
                D = parameters.D,
            };
    }
}
