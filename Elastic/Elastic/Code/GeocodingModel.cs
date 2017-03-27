using Elastic.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Code
{
    public class GeocodingModel
    {
        public GeocodingObj result = new GeocodingObj();
        private string latlng = "";

        public GeocodingModel()
        {
            //
        }
        public GeocodingModel(string input)
        {
            latlng = input;
        }

        // analyzer
        public async Task<GeocodingObj> getAddress()
        {
            GeocodingObj data = new GeocodingObj();

            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latlng + "&key=AIzaSyA2qTf0l1JRZQ_jHv_x8vhpl1HH7Lp2UaU";

            data = await getRESTAsync(url);//.ConfigureAwait(continueOnCapturedContext: false);
            return data;
        }

        private async Task<GeocodingObj> getRESTAsync(string url)
        {
            HttpClient client = new HttpClient();
            GeocodingObj Items = new GeocodingObj();

            Uri uri = new Uri(url);

            try
            {
                var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(content);
                    Items = JsonConvert.DeserializeObject<GeocodingObj>(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}
