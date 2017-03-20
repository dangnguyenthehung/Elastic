using Elastic.Object;
using Elastic.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(UploadImage))]
namespace Elastic.UWP
{
    public class UploadImage : IUpload
    {
        public async Task<string> Upload(StorageFile file)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://dangnguyenthehung.somee.com/UploadToServer/api/Files/Upload");

            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent content = new StringContent("fileToUpload");
            form.Add(content, "fileToUpload");
            var stream = await file.OpenStreamForReadAsync();
            content = new StreamContent(stream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "fileToUpload",
                FileName = file.Name
            };
            form.Add(content);

            var response = await client.PostAsync(client.BaseAddress, form);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
