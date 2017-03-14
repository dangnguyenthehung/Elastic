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
    public class SearchBoxModel
    {
        //public ResultObj result = new ResultObj();
        public List<ResultObj> result = new List<ResultObj>();
        private string searchStr = "";

        // constructor
        public SearchBoxModel()
        {
            //   
        }
        public SearchBoxModel(string input)
        {
            searchStr = input;
        }
        
        // method - api elastic
        public async Task<List<ResultObj>> getData()
        {
            List<ResultObj> data = new List<ResultObj>();

            string url = "http://dangnguyenthehung.somee.com/elasticTest/api/elasticDemo/?title=" + searchStr;

            data = await getRESTAsync(url);//.ConfigureAwait(continueOnCapturedContext: false);
            return data;
        }

        public async Task<List<ResultObj>> getRESTAsync(string url)
        {
            HttpClient client = new HttpClient();
            List<ResultObj> Items = new List<ResultObj>();

            Uri uri = new Uri(url);

            try
            {
                var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ResultObj>>(content);
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
