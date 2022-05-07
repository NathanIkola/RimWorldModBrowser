using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// The class that holds all of the settings for the application
    /// </summary>
    public static class Settings
    {
        #region Private properties
        /// <summary>
        /// Hash table of dependencies being stored in the settings
        /// </summary>
        private static Dictionary<string, string> Dependencies { get; set; } = new();
        #endregion

        #region Dictionary interactions
        /// <summary>
        /// Looks up a setting by key
        /// </summary>
        /// <param name="key">The settings key to look the object up by</param>
        /// <returns>The string corresponding to <paramref name="key"/>, or <see langword="default"/> if it doesn't exist</returns>
        public static string Lookup(string key)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(key));
            if (Dependencies.ContainsKey(key))
                return Dependencies[key];
            return default;
        }

        /// <summary>
        /// Sets a setting
        /// </summary>
        /// <param name="key">The key to set</param>
        /// <param name="value">The value to set the setting to</param>
        public static void Set(string key, string value)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(key));
            Dependencies[key] = value;
        }
        #endregion

        #region Save and load
        /// <summary>
        /// Saves the settings as the specified <paramref name="filename"/>
        /// </summary>
        /// <param name="filename">The file to save the settings as</param>
        public static void Save(string filename = "settings.xml")
        {
            filename = GetValidXMLFile(filename);
            Debug.Assert(filename is not null);

            using FileStream stream = new(filename, FileMode.Create);
            XmlSerializer serializer = new(typeof(SettingsEntry[]));
            serializer.Serialize(stream, GetSettingsEntries());
        }

        /// <summary>
        /// Loads the settings from the specified <paramref name="filename"/>
        /// </summary>
        /// <param name="filename">The file to load the settings from</param>
        public static void Load(string filename = "settings.xml")
        {
            filename = GetValidXMLFile(filename);
            Debug.Assert(filename is not null);

            using FileStream stream = new(filename, FileMode.OpenOrCreate);
            XmlSerializer serializer = new(typeof(SettingsEntry[]));
            try
            {
                SettingsEntry[] entries = serializer.Deserialize(stream) as SettingsEntry[];
                PutSettingsEntries(entries);
            }
            catch (Exception) { }
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Given a <paramref name="filename"/>, returns a valid XML file name 
        /// or <see langword="null"/> if the provided name is inherently invalid
        /// </summary>
        /// <param name="filename">The name of the file to use</param>
        /// <returns>A valid XML file name or <see langword="null"/> if the provided name is inherently invalid</returns>
        /// <remarks>Appends .xml if <paramref name="filename"/> doesn't already have it</remarks>
        private static string GetValidXMLFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || filename.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
                return null;

            filename = filename.Trim();
            if (!filename.ToLower().EndsWith(".xml"))
                filename += ".xml";

            return filename;
        }

        /// <summary>
        /// Returns the array of SettingsEntry objects that correspond to the current hash table
        /// </summary>
        /// <returns></returns>
        private static SettingsEntry[] GetSettingsEntries()
        {
            List<SettingsEntry> entries = new();
            foreach (KeyValuePair<string, string> dependency in Dependencies)
                entries.Add(new() { Key = dependency.Key, Value = dependency.Value });
            return entries.ToArray();
        }

        /// <summary>
        /// Puts an array of SettingsEntry objects into the hash table
        /// </summary>
        /// <param name="entries">The list of entries to put into the hash table</param>
        /// <remarks>Clears the settings hash table</remarks>
        private static void PutSettingsEntries(SettingsEntry[] entries)
        {
            Dependencies.Clear();
            foreach (SettingsEntry entry in entries)
                Set(entry.Key, entry.Value);
        }
        #endregion
    }

    /// <summary>
    /// Represents a single entry in the settings hash table
    /// </summary>
    /// <remarks>Only used during serialization/deserialization</remarks>
    [Serializable]
    public class SettingsEntry
    {
        /// <summary>
        /// The key for this entry
        /// </summary>
        [XmlAttribute]
        public string Key;

        /// <summary>
        /// The value of this entry
        /// </summary>
        [XmlAttribute]
        public string Value;
    }
}