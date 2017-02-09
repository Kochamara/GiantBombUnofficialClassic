using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombUnofficialClassic.ViewModels
{
    public class CategoriesPageViewModel : ViewModelBase
    {
        public CategoriesPageViewModel()
        {

        }

        public async Task InitializeAsync()
        {
            Text = "hello video website";
            var apiKey = Services.ApiKeyManager.GetInstance().GetSavedApiKey();

            string newText = string.Empty;

            var response = await GiantBombApi.Services.VideoRetrievalAgent.GetCategoriesAsync(apiKey, true);
            newText += "SHOWS ONLY\n";
            foreach (var item in response)
            {
                newText += item.Id + " " + item.Name + "\n";
            }
            newText += "\n\n";


            response = await GiantBombApi.Services.VideoRetrievalAgent.GetCategoriesAsync(apiKey, false);
            newText += "ALL THAT SHIT\n";
            foreach (var item in response)
            {
                newText += item.Id + " " + item.Name + "\n";
            }
            newText += "\n\n";

            this.Text = newText;
        }

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged(() => Text);
                }
            }
        }
        private string _text;
    
    }
}