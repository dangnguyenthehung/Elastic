using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace Elastic.UWP
{
    public class ImgPicker
    {
        public List<BitmapImage> imagelist { get; set;}
        public ImgPicker()
        {
            openBtn_Click();
        }

        private async void openBtn_Click()
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

                    var image = new BitmapImage();

                    image.SetSource(stream);

                    imagelist.Add(image);

                }

            }
            else
            {

                //

            }
        }

    }
}
