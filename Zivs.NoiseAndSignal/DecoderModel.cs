using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zivs.NoiseAndSignal
{
    public class DecoderModel
    {
        private const string command = ".\\zivs1.py";

        public string Decode(string encodedString)
        {
            string result = RunCmd(command, encodedString);

            return result;
        }

        private static string RunCmd(string command, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "C:\\Python27\\python.exe";

            startInfo.Arguments = string.Format("{0} {1}", command, args);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;

            string result;
            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

    }

}
