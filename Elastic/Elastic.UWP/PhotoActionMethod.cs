using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Elastic.Object;
using Elastic.UWP;
using Windows.Storage.Streams;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

[assembly: Dependency(typeof(PhotoActionMethod))]
namespace Elastic.UWP
{
    public class PhotoActionMethod : IPhotoAction
    {
        private List<StorageFile> imagelist = new List<StorageFile>();
        
        public PhotoActionMethod()
        {
           // openBtn_Click();
        }

        public async Task<int> openBtn_Click()
        {
            imagelist.Clear();
            imagelist = await process();
            return imagelist.Count();
        }

        public async Task<int> countImg()
        {
            return imagelist.Count();
        }

        private async Task<List<StorageFile>> process()
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.ViewMode = PickerViewMode.Thumbnail;

            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            openPicker.FileTypeFilter.Add(".jpg");

            openPicker.FileTypeFilter.Add(".png");

            var filelist = await openPicker.PickMultipleFilesAsync();

            if (filelist != null)

            {
                foreach (var files in filelist)

                {

                    var stream = await files.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    //var image = new StorageFile();

                    //await image.SetSource(stream);
                    
                    imagelist.Add(files);

                }

            }
            else
            {

                //

            }
            return imagelist;
        }

        public async Task<List<string>> Upload()
        {
            List<string> listResponse = new List<string>();

            foreach (var file in imagelist)
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
                listResponse.Add(response.Content.ReadAsStringAsync().Result);
            }

            return listResponse;
           
            
        }

        //public async Task<byte[]> ReadFile(StorageFile file)
        //{
        //    byte[] fileBytes = null;
        //    using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
        //    {
        //        fileBytes = new byte[stream.Size];
        //        using (DataReader reader = new DataReader(stream))
        //        {
        //            await reader.LoadAsync((uint)stream.Size);
        //            reader.ReadBytes(fileBytes);
        //        }
        //    }

        //    return fileBytes;
        //}


    }
}
