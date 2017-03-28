using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Elastic.Object;
using Elastic.Droid;
using System.Threading.Tasks;

[assembly: Dependency(typeof(PhotoActionMethod))]
namespace Elastic.Droid
{
    public class PhotoActionMethod : IPhotoAction
    {
        //private List<StorageFile> imagelist = new List<StorageFile>();
        private List<string> path_List = new List<string>();

        public PhotoActionMethod()
        {
            // openBtn_Click();
        }

        public async Task<List<string>> openBtn_Click()
        {
            //
            return path_List;
        }

        public List<string> pathList()
        {
            return path_List;
        }

        //private async Task<List<StorageFile>> process()
        //{
        //    FileOpenPicker openPicker = new FileOpenPicker();

        //    openPicker.ViewMode = PickerViewMode.Thumbnail;

        //    openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

        //    openPicker.FileTypeFilter.Add(".jpg");

        //    openPicker.FileTypeFilter.Add(".png");

        //    var filelist = await openPicker.PickMultipleFilesAsync();

        //    if (filelist != null)

        //    {
        //        foreach (var files in filelist)

        //        {

        //            var stream = await files.OpenAsync(Windows.Storage.FileAccessMode.Read);

        //            //var image = new StorageFile();

        //            //await image.SetSource(stream);

        //            imagelist.Add(files);
        //            path_List.Add(files.Path);

        //        }

        //    }
        //    else
        //    {

        //        //

        //    }
        //    return imagelist;
        //}

        public async Task<List<string>> Upload()
        {
            List<string> listResponse = new List<string>();

            //foreach (var file in imagelist)
            //{
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri("http://dangnguyenthehung.somee.com/UploadToServer/api/Files/Upload");

            //    MultipartFormDataContent form = new MultipartFormDataContent();
            //    HttpContent content = new StringContent("fileToUpload");
            //    form.Add(content, "fileToUpload");
            //    var stream = await file.OpenStreamForReadAsync();
            //    content = new StreamContent(stream);
            //    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            //    {
            //        Name = "fileToUpload",
            //        FileName = file.Name
            //    };
            //    form.Add(content);

            //    var response = await client.PostAsync(client.BaseAddress, form);
            //    listResponse.Add(response.Content.ReadAsStringAsync().Result);
            //}

            return listResponse;


        }
    }
}