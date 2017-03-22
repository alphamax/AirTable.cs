using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTable.Core
{
    /// <summary>
    /// Main entry point of the airtable API.
    /// </summary>
    public class AirTableConnector
    {
        /// <summary>
        /// Store private key.
        /// </summary>
        public string APIKey { get; set; }
        /// <summary>
        /// Store enpoint URL.
        /// </summary>
        public string EndPointUrl { get; set; }
        /// <summary>
        /// Store the version of the API.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Create a connector with the given parameters.
        /// Endpoint & Verison have default values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="endPointUrl"></param>
        /// <param name="version"></param>
        public AirTableConnector(string key, string endPointUrl= "https://api.airtable.com", string version = "v0")
        {
            APIKey = key;
            EndPointUrl = endPointUrl;
            Version = version;
        }

        /// <summary>
        /// Get a base folowing the Id & the Name.
        /// </summary>
        /// <param name="baseId"></param>
        /// <param name="baseName"></param>
        /// <returns></returns>
        public Base ExtractBase(string baseId, string baseName)
        {
            return new Base(baseId, baseName, EndPointUrl, Version, APIKey);
        }

    }
}
