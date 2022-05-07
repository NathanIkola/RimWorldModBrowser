using System.Diagnostics;
using System.IO;
using System.Xml;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// Represents the core concept of a RimWorld mod
    /// </summary>
    public class ModConcept
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
        /// The mod's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The path to the mod's root folder
        /// </summary>
        public string Path { get; set; }
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

            ModConcept concept = new()
            {
                Name = GetElementByTagName(aboutXml, "name"),
                Author = GetElementByTagName(aboutXml, "author"),
                Description = GetElementByTagName(aboutXml, "description"),
                Path = xmlPath.Replace(@"About\About.xml", "")
            };

            while (concept.Description is not null && (concept.Description.StartsWith("\n") || concept.Description.StartsWith("\r")))
                concept.Description = concept.Description.TrimStart('\r').TrimStart('\n');

            return concept;
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
        #endregion
    }
}