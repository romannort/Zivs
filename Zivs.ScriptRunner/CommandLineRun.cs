using System;
using System.Diagnostics;
using System.IO;

namespace Zivs.ScriptRunner
{
    internal class CommandLineRun : ICommandExecuter
    {

        public IExecutionResult Execute(ICommandParams commandArguments)
        {
            IExecutionResult result = RunCmd(commandArguments);

            return result;
        }

        private IExecutionResult RunCmd(ICommandParams arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = arguments.Executable,
                Arguments = string.Format("{0} {1}", arguments.Command, arguments.Arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            IExecutionResult result = new ExecutionResult();

            try
            {
                Debug.WriteLine(startInfo.Arguments);
                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        result.Value = reader.ReadToEnd();
                    }

                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        // Collect error data
                        string errorOutput = process.StandardError.ReadToEnd();
                        result.Errors.Add(errorOutput);

                        Debug.WriteLine(errorOutput);
                    }
                }
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                Debug.WriteLine(e.Message);                
            }

            return result;
        }
    }    
}
