namespace Zivs.ScriptRunner.Python
{
    public class Python27Params : ICommandParams
    {
        private const string Python27StandardPath = "C:\\Python27\\python.exe";

        private string executable;

        public string Executable
        {
            get { return executable ?? Python27StandardPath; }
            set { executable = value; }
        }

        public string Command { get; set; }

        public string Arguments { get; set; }

        public Python27Params()
        {
            Command = string.Empty;
            Arguments = string.Empty;
        }
    }
}
