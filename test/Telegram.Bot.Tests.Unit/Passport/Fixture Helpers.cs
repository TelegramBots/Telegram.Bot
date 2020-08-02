using System.IO;

namespace Telegram.Bot.Tests.Unit.Passport
{
    public static class FixtureHelpers
    {
        /// <summary>
        /// Copies test files before unit tests parallel execution starts in order to avoid file access errors
        /// </summary>
        /// <param name="map">Mapping of source to destination files</param>
        public static void CopyTestFiles(params (string Src, string Dest)[] map)
        {
            foreach (var m in map)
            {
                File.Copy($"Files/Passport/{m.Src}", $"Files/Passport/{m.Dest}", true);
            }
        }
    }
}
