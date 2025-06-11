using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace AppInstaller
{
    public class ContentElement
    {
        [JsonProperty("install_path")]
        public string installPath;
        public Link link;


        public void Install(string defaultWorkdir)
        {
            string path = defaultWorkdir + "/" + installPath + "/";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var client = new WebClient())
            {
                client.DownloadFile(link.uri, path + link.filename);
            }
        }
    }
}
