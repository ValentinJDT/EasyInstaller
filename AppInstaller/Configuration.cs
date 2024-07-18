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

        string title;

        [JsonProperty("default_workdir")]
        string defaultWorkdir;

        [JsonProperty("temp_directory")]
        string tempDirectory = Environment.GetEnvironmentVariable("TEMP");

        List<ContentElement> contents = new List<ContentElement>();

        List<RequiredElement> requires = new List<RequiredElement>();


        public void InstallRequires()
        {
            foreach(var required in requires)
            {
                required.Install(defaultWorkdir, tempDirectory);
            }

        }

        public static Configuration Load()
        {
            string jsonContent = Encoding.UTF8.GetString(Resources.setup);
            return JsonConvert.DeserializeObject<Configuration>(jsonContent);
        }
    }
}
