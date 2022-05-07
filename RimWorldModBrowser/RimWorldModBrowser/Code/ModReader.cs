using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// Explores a directory for RimWorld mods
    /// </summary>
    public static class ModReader
    {
        /// <summary>
        /// Gets a list of all mods in teh specified <paramref name="directory"/>
        /// </summary>
        /// <param name="directory">The directory to check</param>
        public static ModConcept[] GetModsInDirectory(string directory)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(directory));
            Debug.Assert(Directory.Exists(directory));

            List<ModConcept> mods = new();

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                string xmlPath = subdirectory + @"\About\About.xml";
                if (File.Exists(xmlPath))
                    mods.Add(ModConcept.ParseFromXML(xmlPath));
                else if (DirectoryIsEmpty(subdirectory))
                    Directory.Delete(subdirectory, false);
            }

            return mods.ToArray();
        }

        /// <summary>
        /// Checks whether a directory is empty
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>Whether or not the directory at the specified <paramref name="path"/> is empty</returns>
        private static bool DirectoryIsEmpty(string path)
        {
            IEnumerable<string> items = Directory.EnumerateFileSystemEntries(path);
            using IEnumerator<string> en = items.GetEnumerator();
            return !en.MoveNext();
        }
    }
}