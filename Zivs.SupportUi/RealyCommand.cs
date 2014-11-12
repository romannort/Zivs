using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Zivs.SupportUi
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> canExecute;

        private readonly Action<object> execute;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public virtual void RaiseCanExecuteChanged()
        {
            EventHandler tempEventHandler = CanExecuteChanged;
            if (tempEventHandler != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => tempEventHandler(this, new EventArgs())));
            }
        }
    }
}
