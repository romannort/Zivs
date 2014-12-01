using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Zivs.SupportUi;

namespace Zivs.TextEncoding
{
    public class MainWindowViewModel: ViewModelBase
    {
        private string initialText;
        private string encodedText;
        private string decodedText;
        private string sizeBefore;
        private string sizeAfter;
        private string compressionLevel;

	    public ObservableCollection<EncodedSymbol> EncodedSymbols
	    {
		    get { return encodedSymbols; }
		    set
		    {
			    encodedSymbols = value;
			    OnPropertyChanged(() => EncodedSymbols);
		    }
	    }

	    public string InitialText
        {
            get { return initialText; }
            set
            {
                if (initialText != value)
                {
                    initialText = value;
                    OnPropertyChanged(() => InitialText);
                }
            }
        }

        public string EncodedText
        {
            get { return encodedText; }
            set
            {
                if (encodedText != value)
                {
                    encodedText = value;
                    OnPropertyChanged(() => EncodedText);
                }
            }
        }

        public string DecodedText
        {
            get { return decodedText; }
            set
            {
                if (decodedText != value)
                {
                    decodedText = value;
                    OnPropertyChanged(() => DecodedText);
                }
            }
        }

        public string SizeBefore
        {
            get { return sizeBefore; }
            set
            {
                if (sizeBefore != value)
                {
                    sizeBefore = value;
                    OnPropertyChanged(() => SizeBefore);
                }
            }
        }

        public string SizeAfter
        {
            get { return sizeAfter; }
            set
            {
                if (sizeAfter != value)
                {
                    sizeAfter = value;
                    OnPropertyChanged(() => SizeAfter);
                }
            }
        }

        public string CompressionLevel
        {
            get { return compressionLevel; }
            set
            {
                if (compressionLevel != value)
                {
                    compressionLevel = value;
                    OnPropertyChanged(() => CompressionLevel);
                }
            }
        }

	    public ICommand EncodingCommand { get; set; }

		public ICommand DecodingCommand { get; set; }

	    private readonly EncodingModel model;

	    private ObservableCollection<EncodedSymbol> encodedSymbols;

	    private IDictionary<string, string> Alphabet { get; set; } 

	    public MainWindowViewModel()
	    {
			model = new EncodingModel();

		    EncodingCommand = new RelayCommand((param) => true, (param) => {

			    EncodingParams @params = new EncodingParams() {
				    Text = InitialText
			    };

			    string @out = model.Run(@params.ToJson());

			    if (model.Succeeded) {
				    DecodingParams decodingParams = JsonConvert.DeserializeObject<DecodingParams>(@out);

				    EncodedSymbols =
					    new ObservableCollection<EncodedSymbol>(
						    decodingParams.Alphabet.Select(kvp => new EncodedSymbol() {Symbol = kvp.Key, Code = kvp.Value}));

				    EncodedText = decodingParams.Text;
				    Alphabet = decodingParams.Alphabet;
				    CompressionLevel = decodingParams.CompressionLevel;
				    SizeBefore = (InitialText.Length * 8).ToString(CultureInfo.InvariantCulture);
					SizeAfter = EncodedText.Length.ToString(CultureInfo.InvariantCulture);
			    }
		    });

		    DecodingCommand = new RelayCommand((param) => true, (param) => {
			    DecodingParams @params = new DecodingParams() {
				    Text = EncodedText,
				    Alphabet = Alphabet
			    };

			    string @out = model.Run(@params.ToJson());

			    if (model.Succeeded) {
				    DecodingParams decodingParams = JsonConvert.DeserializeObject<DecodingParams>(@out);
					DecodedText = decodingParams.Text;
			    }
		    });
	    }
    }

    public class EncodedSymbol
    {
        public string Symbol { get; set; }

        public string Code { get; set; }
    }
}
