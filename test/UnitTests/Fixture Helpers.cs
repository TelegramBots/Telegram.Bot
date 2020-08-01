using System.IO;

namespace UnitTests
{
    public static class FixtureHelpers
    {
        /// <summary>
        /// Copies test files before unit tests parallel execution starts in order to avoid file access errors
        /// </summary>
        /// <param name="map">Mapping of source to destination files</param>
        public static void CopyTestFiles(
            params (string Src, string Dest)[] map
        )
        {
            foreach (var m in map)
                File.Copy($"Files/{m.Src}", $"Files/{m.Dest}", true);
        }
    }
}
