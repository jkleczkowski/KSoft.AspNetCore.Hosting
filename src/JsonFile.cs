namespace Microsoft.AspNetCore.Hosting
{
    public class JsonFile
    {
        public string Path { get; set; }
        public bool Optional { get; set; }
        public JsonFile()
        {

        }
        public JsonFile(string path, bool optional = true)
        {
            this.Path = path;
            this.Optional = optional;
        }
    }
}