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
            GridView.Children.Clear();
            pathListContain.Children.Clear();

            var click = DependencyService.Get<IPhotoAction>();
            if (click != null)
            {
                var pathList = await click.openBtn_Click();
                
                var row = 0;
                var col = 0;

                foreach (var item in pathList)
                {
                    pathListContain.Children.Add(
                        new Label()
                        {
                            Text = item,
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        });

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
                        Source = ImageSource.FromFile(item),
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = 100
                    },
                    col, row);

                    col++;
                    
                }
                
                //FileImage.Source = ImageList[0].Path;
                
            }
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            GridView.Children.Clear();
            pathListContain.Children.Clear();

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
            //GridView.Children.Clear();
            pathListContain.Children.Clear();

            var action = DependencyService.Get<IPhotoAction>();
            if (action != null)
            {
                List<string> responseList = await action.Upload();
                foreach (var item in responseList)
                {

                    string itemUrl = formatUrl(item);
                    System.Diagnostics.Debug.WriteLine(itemUrl);

                    pathListContain.Children.Add(
                        new Label()
                        {
                            Text = itemUrl,
                            FontSize = 12,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        }
                        );
                }
            }
            else
            {
                //
            }


            //var row = 0;
            //var col = 0;

            // get image from internet with Uri path -> add to GridView (put in foreach() loop)

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
