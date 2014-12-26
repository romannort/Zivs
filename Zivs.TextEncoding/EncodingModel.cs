using Rnort.CmdRunner.Core;
using Rnort.CmdRunner.Core.Python;

namespace Zivs.TextEncoding
{
	internal class EncodingModel
	{
		private const string PathToScript = ".\\zivs3.py";

		private readonly ExecuterFacade executor = new ExecuterFacade();

		private readonly ICommandParams parameters = new Python27Params() {
			Command = PathToScript
		};

		public bool Succeeded { get; set; }

		public string Run(string input)
		{
			parameters.Arguments = input;

			executor.EnableEncoding = true;
			IExecutionResult result = executor.Execute(parameters);
			Succeeded = !result.HasErrors;

			return result.Value;
		}

	}
}
