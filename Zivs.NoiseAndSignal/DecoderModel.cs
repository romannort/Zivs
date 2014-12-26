using Rnort.CmdRunner.Core;
using Rnort.CmdRunner.Core.Python;


namespace Zivs.NoiseAndSignal
{
    public class DecoderModel
    {
        private const string Command = ".\\zivs1.py";
        private readonly ExecuterFacade executer = new ExecuterFacade();

        private readonly ICommandParams parameters = new Python27Params()
        {
            Command = Command
        };

        public string Decode(string inputString)
        {
            parameters.Arguments = inputString;
            executer.EnableEncoding = false;
            IExecutionResult result = executer.Execute(parameters);
            
            return result.Value;
        }        
    }
}
