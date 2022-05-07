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
        public string ImagePath => Path + @"\About\Preview.png";

        /// <summary>
        /// The list of paths to the DLLs for this mod
        /// </summary>
        public List<string> DllPaths { get; set; }

        /// <summary>
        /// Returns whether or not this mod has DLLs available
        /// </summary>
        public bool HasDlls => Settings.Lookup(Constants.DnSpyPath) is not null && (DllPaths?.Count ?? 0) > 0;

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

            XmlDocument aboutXml = new();
            aboutXml.Load(xmlPath);

            // attempt to get the Steam ID
            string steamIdPath = xmlPath.Replace("About.xml", "PublishedFileId.txt");
            string steamId = null;
            if (File.Exists(steamIdPath))
            {
                string[] steamIds = File.ReadAllLines(steamIdPath);
                foreach (string id in steamIds)
                    if (!string.IsNullOrEmpty(id))
                        steamId = id;
            }
            else
            {
                steamIdPath = steamIdPath.Replace(@"About\PublishedFileId.txt", "");
                if (steamIdPath.Contains("294100"))
                    steamId = System.IO.Path.GetDirectoryName(steamIdPath + @"\").Split(@"\").Last();
            }

            string modPath = xmlPath.Replace(@"\About\About.xml", "");

            ModConcept concept = new()
            {
                Name = GetElementByTagName(aboutXml, "name"),
                Author = GetElementByTagName(aboutXml, "author"),
                Description = GetElementByTagName(aboutXml, "description"),
                Path = xmlPath.Replace(@"About\About.xml", ""),
                SteamId = steamId,
                DllPaths = GetDlls(modPath)
            };

            while (concept.Description is not null && (concept.Description.StartsWith("\n") || concept.Description.StartsWith("\r")))
                concept.Description = concept.Description.TrimStart('\r').TrimStart('\n');

            return concept;
        }

        /// <summary>
        /// Search the entire directory for DLLs and return their paths
        /// </summary>
        /// <param name="modPath">The path to check</param>
        /// <returns>A list of DLL paths</returns>
        private static List<string> GetDlls(string modPath)
        {
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