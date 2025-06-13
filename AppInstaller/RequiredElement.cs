using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;

namespace AppInstaller
{
    public class RequiredElement
    {

        public InstallType type = InstallType.Link;
        public string name;
        public List<Link> links = new List<Link>();
        public Action action;

        [JsonProperty("install_path")]
        public string installPath = null;

        public void Install(string defaultWorkdir, string tempDirectory)
        {
            switch (type)
            {
                case InstallType.Link:

                    switch (action)
                    {
                        case Action.Unzip:
                            foreach (var link in links)
                            {
                                string zipPath = tempDirectory + "/" + link.filename;

                                var outputDir = (installPath == "." ? defaultWorkdir : installPath) + "/" + link.filename.Replace(".zip", "") + "/";
                                if(!Directory.Exists(outputDir))
                                {
                                    Directory.CreateDirectory(outputDir);
                                    DownloadFile(link.uri, zipPath);
                                    ZipFile.ExtractToDirectory(zipPath, outputDir);
                                    File.Delete(zipPath);
                                }
                            }
                            break;

                        case Action.Run:
                            foreach (var link in links)
                            {
                                string executionPath = tempDirectory + "/" + link.filename;
                                DownloadFile(link.uri, executionPath);
                                LaunchWaitingProcess(executionPath);
                                File.Delete(executionPath);
                            }
                            break;

                        default:
                            foreach (var link in links)
                            {
                                string dirPath = defaultWorkdir + "/" + (installPath == "." ? "" : (installPath + "/"));

                                if (!Directory.Exists(dirPath))
                                    Directory.CreateDirectory(dirPath);

                                DownloadFile(link.uri, dirPath + link.filename);
                            }
                            break;
                    }

                    break;
                case InstallType.Winget:
                    var exists = WingetProcessInstalled(name);
                    if (!exists)
                    {
                        LaunchWaitingProcess("cmd.exe", $"/C winget install {name}");
                    }
                    break;
                default:
                    MessageBox.Show($"Type d'installation inconnu pour {name}", "Erreur d'installation", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void LaunchWaitingProcess(string processName, string args = "")
        {
            LaunchWaitingProcess(new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processName,
                    Arguments = args
                }
            });
        }

        private void LaunchWaitingProcess(Process process)
        {
            process.Start();
            process.WaitForExit();
        }

        private bool WingetProcessInstalled(string id)
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c winget list --id {id} | find \"{id}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            return output != null && output != "";
        }

        private void DownloadFile(string url, string output)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, output);
            }
        }
    }

    public enum InstallType
    {
        Winget,
        Link,
        Inside
    }

    public enum Action
    {
        Unzip,
        Run
    }
}
