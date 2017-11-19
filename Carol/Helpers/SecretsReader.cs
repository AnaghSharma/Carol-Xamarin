/*
 * Helper class to retrieve API key from Secrets.json fole
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Carol.Helpers
{
    public static class SecretsReader
    {
        private static JObject secrets;

        /// <summary>
        /// Gets the API key
        /// </summary>
        /// <returns>API Key</returns>
        public static string GetSecrets()
        {
            secrets = JObject.Parse(GetSecretKey());
            return secrets["API_key"].Value<String>();
        }

        public static string GetSecretKey()
        {
            return File.ReadAllText("Secrets.json");
        }
    }
}
