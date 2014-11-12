using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Zivs.OpenKey.Models;
using Zivs.SupportUi;

namespace Zivs.OpenKey
{
    public class MainWindowViewModel: ViewModelBase
    {
        private InputParams inputParams;
        private ObservableCollection<OutputParams> results;
        private readonly ScriptRunnerModel scriptRunner = new ScriptRunnerModel();
        private RelayCommand decodingCommand;

        public MainWindowViewModel()
        {
            inputParams = new InputParams();
            inputParams.A = "5";
            inputParams.B = "17";
            inputParams.P = "253";
            inputParams.U = "2";
            inputParams.Message = "Hello World";
        }

        public InputParams InputParams
        {
            get { return inputParams; }
            set
            {
                if (inputParams != value)
                {
                    inputParams = value;
                    OnPropertyChanged(() => InputParams);
                }
            }
        }

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
            get { return InputParams.ToJson(); }
        }

        private bool InputsValid(object obj)
        {
            return InputParams.Valid();
        }

        private void ExecuteCommand(object param)
        {
            string result = scriptRunner.Run((string) param);

            if (scriptRunner.Succeded)
            {
                IList<OutputParams> deserializedResults = JsonConvert.DeserializeObject<IList<OutputParams>>(result);
                if (deserializedResults != null)
                {
                    Results = new ObservableCollection<OutputParams>(deserializedResults);
                }
            }
        }
    }
}
