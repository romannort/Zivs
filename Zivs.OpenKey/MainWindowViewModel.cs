using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Zivs.OpenKey.Models;
using Zivs.SupportUi;

namespace Zivs.OpenKey
{
    public class MainWindowViewModel : ViewModelBase
    {
        private InputParams inputParams;
        private ObservableCollection<OutputParams> results;
        private readonly ScriptRunnerModel scriptRunner = new ScriptRunnerModel();
        private RelayCommand decodingCommand;

        public MainWindowViewModel()
        {
            inputParams = new InputParams();
            inputParams.A = "5";
            inputParams.B = "19";
            inputParams.P = "127";
            inputParams.U = "2";
            inputParams.Message = "Hello World";
        }

        public InputParams InputParams
        {
            get { return inputParams; }
            set
            {
                inputParams = value;
            }
        }

        #region Props

        public string A
        {
            get { return inputParams.A; }
            set
            {
                if (inputParams.A != value)
                {
                    inputParams.A = value;
                    OnPropertyChanged(() => A);
                }
            }
        }

        public string B
        {
            get { return inputParams.B; }
            set
            {
                if (inputParams.B != value)
                {
                    inputParams.B = value;
                    OnPropertyChanged(() => B);
                }
            }
        }

        public string P
        {
            get { return inputParams.P; }
            set
            {
                if (inputParams.P != value)
                {
                    inputParams.P = value;
                    OnPropertyChanged(() => P);
                }
            }
        }

        public string U
        {
            get { return inputParams.U; }
            set
            {
                if (inputParams.U != value)
                {
                    inputParams.U = value;
                    OnPropertyChanged(() => U);
                }
            }
        }

        public string Message
        {
            get { return inputParams.Message; }
            set
            {
                if (inputParams.Message != value)
                {
                    inputParams.Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }

        #endregion


        public ObservableCollection<OutputParams> Results
        {
            get { return results; }
            set
            {
                if (results != value)
                {
                    results = value;
                    OnPropertyChanged(() => Results);
                }
            }
        }

        public RelayCommand DecodingCommand
        {
            get { return decodingCommand ?? (decodingCommand = new RelayCommand(InputsValid, ExecuteCommand)); }
            set { decodingCommand = value; }
        }

        public object CommandParameter
        {
            get { return inputParams.ToJson(); }
        }

        private bool InputsValid(object obj)
        {
            return InputParams.Valid();
        }

        private void ExecuteCommand(object param)
        {
            string result = scriptRunner.Run((string)CommandParameter);

            if (scriptRunner.Succeded)
            {
                IList<OutputParams> deserializedResults = JsonConvert.DeserializeObject<IList<OutputParams>>(result);
                if (deserializedResults != null)
                {
                    Results = new ObservableCollection<OutputParams>(deserializedResults);
                }
            }
        }

        protected new void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.OnPropertyChanged(propertyExpression, () => decodingCommand.RaiseCanExecuteChanged());
        }
    }
}
