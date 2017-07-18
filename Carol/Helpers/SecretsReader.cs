using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Carol.Helpers
{
    public static class SecretsReader
    {
        private static JObject secrets;

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
