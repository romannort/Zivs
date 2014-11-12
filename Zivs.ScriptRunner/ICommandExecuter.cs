namespace Zivs.ScriptRunner
{
    public interface ICommandExecuter
    {
        IExecutionResult Execute(ICommandParams commandArguments);
    }
}
