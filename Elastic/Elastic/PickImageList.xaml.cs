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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elastic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickImageList : ContentPage
    {
        private MediaFile _mediaFile;
        //private List<StorageFile> ImageList;
        //private List<ImageStream> streamList;

        public PickImageList()
        {
            InitializeComponent();
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            //GridView.Children.Clear();
            var click = DependencyService.Get<IPhotoAction>();
            if (click != null)
            {
                var count = await click.openBtn_Click();
                
                var row = 0;
                var col = 0;
                //foreach (var item in ImageList)
                //{

                //    if (row == 3)
                //    {
                //        row = 0;
                //    }
                //    if (col == 3)
                //    {
                //        col = 0;
                //        row++;
                //    }

                //    GridView.Children.Add(new Image()
                //    {
                //        Source = item.Path,
                //        HorizontalOptions = LayoutOptions.Center,
                //        HeightRequest = 100
                //    },
                //    col, row);

                //    col++;

                //}

                //FileImage.Source = ImageList[0].Path;

                LocalPathLabel.Text = count.ToString();

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
            //StorageFile file = ImageList[0];

            GridView.Children.Clear();

            var action = DependencyService.Get<IPhotoAction>();
            List<string> responseList = await action.Upload();

            //var row = 0;
            //var col = 0;

            foreach (var item in responseList)
            {

                string itemUrl = formatUrl(item);
                System.Diagnostics.Debug.WriteLine(itemUrl);

                // get image from internet with Uri path -> add to GridView

                //if (row == 3)
                //{
                //    row = 0;
                //}
                //if (col == 3)
                //{
                //    col = 0;
                //    row++;
                //}
                //GridView.Children.Add(
                //    new Image()
                //    {
                //        Source = new Uri(itemUrl, UriKind.Absolute),
                //        HeightRequest = 50
                //    }, col, row);

                //col++;

                pathList.Children.Add(
                    new Label()
                    {
                        Text = itemUrl,
                        FontSize = 12,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    }
                    );
            }
            

            

            
        }

        private string formatUrl(string originalUrl)
        {
            int startIndex = originalUrl.IndexOf("www");
            originalUrl = originalUrl.Substring(startIndex, originalUrl.Length - startIndex - 1).Replace("\\", "/").Replace("//", "/");
            string newUrl = "http://" + originalUrl;

            return newUrl;
        }
    }
}
