using AppInstaller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppInstaller
{
    public class Configuration
    {

        public string name;

        [JsonProperty("default_workdir")]
        public string defaultWorkdir;

        public string author;

        [JsonProperty("temp_directory")]
        public string tempDirectory = Environment.GetEnvironmentVariable("TEMP");

        public List<ContentElement> contents = new List<ContentElement>();

        public List<RequiredElement> requires = new List<RequiredElement>();

        public event EventHandler<ProgressEventArgs> RequireProgressEventHandler;
        public event EventHandler<ProgressEventArgs> ContentProgressEventHandler;

        public void InstallRequires()
        {
            foreach(var required in requires)
            {
                required.Install(ResolveEnvironmentVariables(defaultWorkdir) + "/" + name, ResolveEnvironmentVariables(tempDirectory));
                int percentage = ((requires.IndexOf(required)+1) / requires.Count) * 100;
                RequireProgressEventHandler?.Invoke(this, new ProgressEventArgs(percentage, requires.IndexOf(required) + 1, requires.Count));
            }
        }

        public void Install()
        {
            foreach (var content in contents)
            {
                content.Install(ResolveEnvironmentVariables(defaultWorkdir) + "/" + name);
                int percentage = ((contents.IndexOf(content) + 1) / contents.Count) * 100;
                ContentProgressEventHandler?.Invoke(this, new ProgressEventArgs(percentage, contents.IndexOf(content) + 1, contents.Count));
            }
        }

        public static Configuration Load()
        {
            string jsonContent = Encoding.UTF8.GetString(Resources.setup);
            return JsonConvert.DeserializeObject<Configuration>(jsonContent);
        }

        public static string ResolveEnvironmentVariables(string input)
        {
            // Use a regular expression to find all environment variables in the input string
            return Regex.Replace(input, @"%(\w+)%", match =>
            {
                // Get the environment variable name
                string envVarName = match.Groups[1].Value;

                // Get the value of the environment variable
                string envVarValue = Environment.GetEnvironmentVariable(envVarName);

                Console.WriteLine($"Resolving environment variable: {envVarName} = {envVarValue}");

                // Return the environment variable value or the original match if the variable is not defined
                return envVarValue ?? match.Value;
            });
        }
    }

    public class ProgressEventArgs : EventArgs
    {
        public int percentage { get; }
        public int already { get; }
        public int max { get; }

        public ProgressEventArgs(int percentage, int already, int max)
        {
            this.percentage = percentage;
            this.already = already;
            this.max = max;
        }
    }
}
