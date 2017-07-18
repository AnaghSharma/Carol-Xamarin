using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Carol.Helpers
{
    public class SecretsReader
    {
        public string GetSecrets()
        {
            JObject secrets = JObject.Parse(GetSecretKey());
            return secrets["API_key"].Value<String>();
        }

        public string GetSecretKey()
        {
            return File.ReadAllText("Secrets.json");
        }
    }
}
