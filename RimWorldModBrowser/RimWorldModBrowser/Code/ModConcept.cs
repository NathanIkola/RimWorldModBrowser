using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// Represents the core concept of a RimWorld mod
    /// </summary>
    public class ModConcept : IComparable
    {
        #region Public properties
        /// <summary>
        /// The mod's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The mod's author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The string the view can bind to that displays the author's name
        /// </summary>
        public string AuthorString => Strings.AuthorBy.Replace("{0}", Author);

        /// <summary>
        /// The mod's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The path to the mod's root folder
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The path to the mod's image
        /// </summary>
        public string ImagePath
        {
            get
            {
                string path = Path + @"\About\Preview.png";
                if (File.Exists(path))
                    return path;
                return null;
            }
        }

        /// <summary>
        /// The list of paths to the DLLs for this mod
        /// </summary>
        public List<string> DllPaths { get; set; }

        /// <summary>
        /// Returns whether or not this mod has DLLs available
        /// </summary>
        public bool HasDlls => !string.IsNullOrWhiteSpace(Settings.Lookup(Constants.DnSpyPath)) && (DllPaths?.Count ?? 0) > 0;

        /// <summary>
        /// The mod ID for the mod (if present)
        /// </summary>
        public string SteamId { get; set; }

        /// <summary>
        /// Returns whether or not this mod has an associated Steam ID
        /// </summary>
        public bool HasSteamId => !string.IsNullOrEmpty(SteamId);
        #endregion

        #region Static methods
        /// <summary>
        /// Takes in an XML file and attempts to load a ModConcept from it
        /// </summary>
        /// <param name="xmlPath">The XML file to parse</param>
        /// <returns>A ModConcept object</returns>
        /// <remarks>Removes any leading \r\n sequences to make printing easier</remarks>
        public static ModConcept ParseFromXML(string xmlPath)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(xmlPath));
            Debug.Assert(File.Exists(xmlPath));
            Debug.Assert(xmlPath.EndsWith(@"\About\About.xml"));

            XmlDocument aboutXml = new();
            try { aboutXml.Load(xmlPath); }
            catch (XmlException) { return null; }

            string name = GetElementByTagName(aboutXml, "name");
            string author = GetElementByTagName(aboutXml, "author");
            string description = GetElementByTagName(aboutXml, "description");
            string modPath = xmlPath.Replace(@"\About\About.xml", "");
            string steamId = TryFindSteamId(xmlPath);

            return new()
            {
                Name = name,
                Author = author,
                Description = TrimDescription(description),
                Path = modPath,
                SteamId = steamId,
                DllPaths = GetDlls(modPath)
            };
        }

        /// <summary>
        /// Trims leading newlines from <paramref name="description"/>
        /// </summary>
        /// <param name="description">The description to trim</param>
        /// <returns><paramref name="description"/> with no leading newlines</returns>
        public static string TrimDescription(string description)
        {
            while (description is not null && (description.StartsWith("\n") || description.StartsWith("\r")))
                description = description.TrimStart('\r').TrimStart('\n');
            return description;
        }

        /// <summary>
        /// Try to get the Steam ID of this mod by checking the PublishedFileId.txt file
        /// or if that isn't present, tries to get the ID from the folder path -- this
        /// only works for workshop mods
        /// </summary>
        /// <param name="xmlPath">The path passed into <see cref="ParseFromXML"/></param>
        /// <returns>The steam ID if found, or <see langword="null"/> if not</returns>
        private static string TryFindSteamId(string xmlPath)
        {
            string steamIdPath = xmlPath.Replace("About.xml", "PublishedFileId.txt");
            if (File.Exists(steamIdPath))
            {
                string[] steamIds = Array.Empty<string>();
                try { steamIds = File.ReadAllLines(steamIdPath); }
                catch (Exception) { }

                if (steamIds?.Length > 0)
                    return steamIds[0];
            }

            steamIdPath = steamIdPath.Replace(@"About\PublishedFileId.txt", "");
            if (steamIdPath.Contains("294100"))
                return System.IO.Path.GetDirectoryName(steamIdPath + @"\").Split(@"\").Last();

            return null;
        }

        /// <summary>
        /// Search the entire directory for DLLs and return their paths
        /// </summary>
        /// <param name="modPath">The path to check</param>
        /// <returns>A list of DLL paths</returns>
        private static List<string> GetDlls(string modPath)
        {
            Debug.Assert(Directory.Exists(modPath));

            List<string> dlls = new();
            foreach(string file in Directory.GetFiles(modPath,"*.dll"))
                dlls.Add(file);

            foreach (string directory in Directory.GetDirectories(modPath))
                dlls.AddRange(GetDlls(directory));

            return dlls;
        }

        /// <summary>
        /// Gets the inner text of an XML element if possible
        /// </summary>
        /// <param name="xmlDocument">The document to search in</param>
        /// <param name="tagName">The tag to locate</param>
        /// <returns>The inner text of that element, or <see langword="null"/> if it doesn't exist</returns>
        /// <remarks>Just cleans up the code</remarks>
        private static string GetElementByTagName(XmlDocument xmlDocument, string tagName)
        {
            Debug.Assert(xmlDocument is not null);
            Debug.Assert(!string.IsNullOrEmpty(tagName));

            return xmlDocument.GetElementsByTagName(tagName)?.Item(0)?.InnerText;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns the name of the mod
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (obj is ModConcept mod)
                return Name.CompareTo(mod.Name);
            return 1;
        }
        #endregion
    }
}