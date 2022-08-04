using Newtonsoft.Json;
using NotSpotify.MVVM.Model;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotSpotify.MVVM.ViewModel
{
    
    internal class MainViewModel
    {

        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PupolateCollection();
        }

        void PupolateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQA8RzdSvLMTf8Ap_nkXqsHp91N1Ctl2YUuNvPCjQ3lM518sxSb87GgrkaJyBZcm8jS_eLspMcq4c_HwdaMT7vFcrikH_jLgDKvQaEwUq1h6sXtlo62qKzaRt8zCRQThXyb8VV6zTgJ5-sokIl7nnKx-CpwuN8oAc7rVrn7UxiErwviUAaVMZ5C-8I_OGD62hSw", "Bearer");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");


            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
               track.Duration = "2:32";
               Songs.Add(track);
                
            }
        }
    }
}
