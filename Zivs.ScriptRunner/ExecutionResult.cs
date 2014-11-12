using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zivs.ScriptRunner
{
    public class ExecutionResult: IExecutionResult
    {
        public string Value { get; set; }

        public ICollection<string> Errors { get; set; }

        public bool HasErrors
        {
            get { return Errors.Any(); }
        }

        public ExecutionResult()
        {
            Value = string.Empty;

            Errors = new List<string>();
        }
    }

    public interface IExecutionResult
    {

        string Value { get; set; }

        ICollection<string> Errors { get; set; }

        bool HasErrors { get; }

    }
}
