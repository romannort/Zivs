using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zivs.ScriptRunner
{
    public interface ICommandParams
    {
        string Executable { get; set; }

        string Command { get; set; }

        string Arguments { get; set; }
    }
}
