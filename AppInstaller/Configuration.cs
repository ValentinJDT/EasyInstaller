using AppInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppInstaller
{
    public class Configuration
    {

        public string title;

        [JsonProperty("default_workdir")]
        public string defaultWorkdir;

        public string author;

        [JsonProperty("temp_directory")]
        public string tempDirectory = Environment.GetEnvironmentVariable("TEMP");

        public List<ContentElement> contents = new List<ContentElement>();

        public List<RequiredElement> requires = new List<RequiredElement>();

        public void InstallRequires()
        {
            foreach(var required in requires)
            {
                required.Install(defaultWorkdir, tempDirectory);
                int percentage = ((requires.IndexOf(required)+1) / requires.Count) * 100;
            }
        }

        public void Install()
        {
            foreach (var content in contents)
            {
                content.Install(defaultWorkdir);
                int percentage = ((contents.IndexOf(content) + 1) / contents.Count) * 100;
            }
        }

        public static Configuration Load()
        {
            string jsonContent = Encoding.UTF8.GetString(Resources.setup);
            return JsonConvert.DeserializeObject<Configuration>(jsonContent);
        }
    }
}
