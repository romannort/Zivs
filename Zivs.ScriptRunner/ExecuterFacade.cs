using System.Text;
using Zivs.ScriptRunner.DataEncoding;

namespace Zivs.ScriptRunner
{
    public class ExecuterFacade
    {
        private readonly ICommandExecuter commandExecuter;

        public bool EnableEncoding { get; set; }

        public ExecuterFacade()
        {
            commandExecuter = new CommandLineRun();
            EnableEncoding = true;
        }

        public IExecutionResult Execute(ICommandParams parameters)
        {
            IExecutionResult result =
                commandExecuter.Execute(new EncodedArgumentParams(parameters) {EncodingStrategy = GetEncodingStrategy()});

            return new EncodedResult(result) {EncodingStrategy = GetEncodingStrategy()};
        }

        private IEncodingStrategy GetEncodingStrategy()
        {
            return EnableEncoding ? 
                (IEncodingStrategy) new Base64EncodingStrategy(Encoding.UTF8) 
                : new DefaultEncodingStrategy();
        }
    }
}
