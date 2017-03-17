//using Plugin.Media;
//using Plugin.Media.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace Elastic
//{

//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class UpLoadImagePage : ContentPage
//    {
//        public UpLoadImagePage()
//        {
//            InitializeComponent();
//           BindingContext = new UpLoadImagePageViewModel();
//        }
//    }

//    public class UpLoadImagePageViewModel : INotifyPropertyChanged
//    {

//        //public UpLoadImagePageViewModel()
//        //{
//        //    IncreaseCountCommand = new Command(IncreaseCount);
//        //}

//        //int count;

//        //string countDisplay = "You clicked 0 times.";
//        //public string CountDisplay
//        //{
//        //    get { return countDisplay; }
//        //    set { countDisplay = value; OnPropertyChanged(); }
//        //}

//        //public ICommand IncreaseCountCommand { get; }

//        //void IncreaseCount() =>
//        //    CountDisplay = $"You clicked {++count} times";


//        //public event PropertyChangedEventHandler PropertyChanged;
//        //void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
//        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

//        // my code
//        public UpLoadImagePageViewModel()
//        {
//            //PickImageCommand = new Command(PickImage);
//        }

//        string api_address = "";

//        string LocalUrl = "Local";
//        string ServerImgUrl = "Server";



        
//        //public ICommand PickImageCommand { get; }
//        public ICommand TakePictureCommand { get; }
//        public ICommand UploadImageCommand { get; }

//        // Binding to here! not LocalUrl variable!
//        public string LocalUrlChange {
//            get { return LocalUrl; }
//            set { LocalUrl = value; OnPropertyChanged(); }
//        }

//        void PickImage() => 
//            LocalUrlChange = "pick button clicked!";


//        // pick img
//        private MediaFile _mediaFile;

//        private async void PickImageCommand(object sender, EventArgs e)
//        {
//            await CrossMedia.Current.Initialize();

//            if (!CrossMedia.Current.IsPickPhotoSupported)
//            {
//                //await DisplayAlert("No PickPhoto",":( No pickPhoto available.","OK");
//                return;
//            }

//            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

//            if(_mediaFile == null)
//            {
//                return;
//            }

//            //Lb_LocalUrl.Text = _mediaFile.Path;

//            //ImgVisualization.Source = ImageSource.FromStream(() =>
//            //{
//            //    return _mediaFile.Get
//            //})
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

//    }
//}
