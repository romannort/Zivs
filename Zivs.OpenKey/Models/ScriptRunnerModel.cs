using Zivs.ScriptRunner;
using Zivs.ScriptRunner.Python;

namespace Zivs.OpenKey.Models
{
    public class ScriptRunnerModel
    {
        private const string PathToScript = ".\\zivs2.py";

        private readonly ExecuterFacade executer = new ExecuterFacade();

        private readonly ICommandParams parameters = new Python27Params()
        {
            Command = PathToScript
        };

        public bool Succeded { get; set; }

        public string Run(string input)
        {
            parameters.Arguments = input;
            
            executer.EnableEncoding = true;
            IExecutionResult result = executer.Execute(parameters);
            Succeded = !result.HasErrors;

            return result.Value;
        }
    }
}
