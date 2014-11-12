using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Zivs.SupportUi;

namespace Zivs.NoiseAndSignal
{
    public class MainWindowViewModel : ViewModelBase
    {
        private String encodedString;
        private DecoderModel model;
        private RelayCommand decodingCommand;
        private string decodedData;
        private string library;


        public String EncodedString
        {
            get { return encodedString; }
            set
            {
                if (encodedString != value)
                {
                    encodedString = value;
                    OnPropertyChanged(() => EncodedString);
                }
            }
        }

        public String DecodedData
        {
            get { return decodedData; }
            set
            {
                if (decodedData != value)
                {
                    decodedData = value;
                    OnPropertyChanged(() => DecodedData);
                }
            }
        }

        public String Library
        {
            get { return library; }
            set
            {
                if (library != value)
                {
                    library = value;
                    OnPropertyChanged(() => Library);
                }
            }
        }

        public ICommand DecodingCommand
        {
            get
            {
                if (decodingCommand == null)
                {
                    decodingCommand = new RelayCommand(
                        (param) => InputsValid(),
                        (param) => DecodeString((string) param));
                }
                return decodingCommand;
            }
        }

        public MainWindowViewModel()
        {
            model = new DecoderModel();
            EncodedString = @"111101101001101111101011001000100000011100101";
            Library =
                @"'ARMIY', 'MIRTA', 'MICAR', 'UTYTA', 'MARIY', 'CITRA', 'TARTU', 'RACIY', 'TRATA', 'MARTA', 'MARAT', 'TATRA', 'ARTUR', 'TIARA', 'ARAMA', 'TIMUR', 'MUMIY'";
        }


        private void DecodeString(string inputString)
        {
            DecodedData = @"Decoding...";

            DecodedData = Task<string>.Factory.StartNew(() => model.Decode(inputString)).Result;
        }

        private bool InputsValid()
        {
            return !string.IsNullOrEmpty(EncodedString) &&
                   !string.IsNullOrWhiteSpace(EncodedString) &&
                   !string.IsNullOrEmpty(Library) &&
                   !string.IsNullOrWhiteSpace(Library);
        }

        protected new void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.OnPropertyChanged(propertyExpression,
                () => decodingCommand.RaiseCanExecuteChanged());
        }
    }
}
