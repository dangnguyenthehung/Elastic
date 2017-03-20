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

[assembly: Dependency(typeof(ImgPicker))]
namespace Elastic.UWP
{
    public class ImgPicker : IPicker
    {
        private List<StorageFile> imagelist = new List<StorageFile>();
        
        public ImgPicker()
        {
           // openBtn_Click();
        }

        public async Task<List<StorageFile>> openBtn_Click()
        {
            imagelist = await process();
            return imagelist;
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
