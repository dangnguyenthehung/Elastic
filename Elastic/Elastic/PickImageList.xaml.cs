using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elastic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickImageList : ContentPage
    {
        private MediaFile _mediaFile;

        public PickImageList()
        {
            InitializeComponent();
        }


        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            GridView.Children.Clear();
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":( No pickPhoto available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
            {
                return;
            }

            LocalPathLabel.Text = _mediaFile.Path;
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            var row = 0;
            var col = 0;
            for (var i = 0; i < 6; i++)
            {
                if (row == 2)
                {
                    row = 0;
                }
                if (col == 3)
                {
                    col = 0;
                    row++;
                }
                Image img = new Image()
                {
                    Source = ImageSource.FromStream(() =>
                    {
                        return _mediaFile.GetStream();
                    }),
                    HorizontalOptions = LayoutOptions.Center,
                    HeightRequest = 50
                };
                GridView.Children.Add(img, col, row);
                col++;
            }
            //    if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            //    {
            //        // move layout under the status bar


            ////
            //    }


        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "myImage.jpg"
            });

            if (_mediaFile == null)
                return;

            LocalPathLabel.Text = _mediaFile.Path;
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
        }
        private async void UploadFile_Clicked(object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"",
                $"\"{_mediaFile.Path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = "http://dangnguyenthehung.somee.com/UploadToServer/api/Files/Upload";
            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

            RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
