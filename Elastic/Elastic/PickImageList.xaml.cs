using Elastic.Object;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<StorageFile> ImageList;
        private List<ImageStream> streamList;

        public PickImageList()
        {
            InitializeComponent();
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            GridView.Children.Clear();
            var click = DependencyService.Get<IPicker>();
            if (click != null)
            {
                ImageList = await click.openBtn_Click();
                var count = ImageList.Count();
                
                var row = 0;
                var col = 0;
                foreach (var item in ImageList)
                {
                   
                    //var img = new ImageStream();
                    //img.fileBytes = await click.ReadFile(item);
                    
                    //img.fileName = item.Name;
                    //img.filePath = item.Path;
                    //streamList.Add(img);

                    if (row == 3)
                    {
                        row = 0;
                    }
                    if (col == 3)
                    {
                        col = 0;
                        row++;
                    }

                    GridView.Children.Add(new Image()
                    {
                        Source = item.Path,
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = 100
                    },
                    col, row);

                    col++;

                }
                FileImage.Source = ImageList[0].Path;

            }
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
            StorageFile file = ImageList[0];

            var action = DependencyService.Get<IUpload>();
            string respone = await action.Upload(file);

            RemotePathLabel.Text = respone;
            //var form = new MultipartFormDataContent();

            //HttpContent content = new StringContent("fileToUpload");
            //form.Add(content, "fileToUpload");
            //var stream = await file.OpenStreamForReadAsync();
            //content = new StreamContent(stream);
            //content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            //{
            //    Name = "fileToUpload",
            //    FileName = file.Name
            //};

            //content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"",
            //    $"\"{_mediaFile.Path}\"");

            // test code



            //var httpClient = new HttpClient();

            //var uploadServiceBaseAddress = "http://dangnguyenthehung.somee.com/UploadToServer/api/Files/Upload";
            //var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, form);

            //RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
