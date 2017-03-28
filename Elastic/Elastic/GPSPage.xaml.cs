using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace Elastic
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GPSPage : ContentPage
    {
        Geocoder geoCoder;
        double latitude = 0;
        double longitude = 0;
        DateTimeOffset timestamp;

        public GPSPage()
        {
            InitializeComponent();
            AutoDetectGPS();
        }

        private async void AutoDetectGPS()
        {
            
            // get GPS location
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 20;

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 60000); // 1 minute timeout
                if (position == null)
                {
                    return;
                }
                    
                latitude = position.Latitude;
                longitude = position.Longitude;
                timestamp = position.Timestamp;
            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.ToString();
                System.Diagnostics.Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }

            //try
            //{
            //    ReverseAddress(latitude, longitude);
            //}
            //catch (Exception ex)
            //{
            //    Address.Text = "Cannot get details address at the moment!";
            //}

        }

        private void detectGPS(object sender, EventArgs e)
        {
            TimeStamp.Text = "Time: ";
            Longitude.Text = "Longitude: ";
            Latitude.Text = "Latitude: ";
            Address.Text = "Address: ";
            // get GPS location
            try
            {
                if (latitude == 0 && longitude == 0)
                {
                    AutoDetectGPS();
                }
            }
            catch (Exception ex)
            {
                TimeStamp.Text = "Unable to get location, may need to increase timeout! ";
                System.Diagnostics.Debug.WriteLine(ex.ToString()); 
            }

            try
            {
                ReverseAddress(latitude, longitude);
            }
            catch (Exception ex)
            {
                Address.Text = "Cannot get details address at the moment!";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }


        private async void ReverseAddress(double latitude, double longitude)
        {
            try
            {
                geoCoder = new Geocoder();
                var currentPosition = new Position(latitude, longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(currentPosition);
                
                TimeStamp.Text += timestamp.ToString();
                Longitude.Text += longitude.ToString();
                Latitude.Text += latitude.ToString();

                foreach (var address in possibleAddresses)
                {
                    Address.Text += address + "\n";
                }
            }
            catch ( Exception ex)
            {
                Address.Text = ex.ToString();
            }
           
        }
    }



    //class GPSPageViewModel : INotifyPropertyChanged
    //{

    //    public GPSPageViewModel()
    //    {
    //        IncreaseCountCommand = new Command(IncreaseCount);
    //    }

    //    int count;

    //    string countDisplay = "You clicked 0 times.";
    //    public string CountDisplay
    //    {
    //        get { return countDisplay; }
    //        set { countDisplay = value; OnPropertyChanged(); }
    //    }

    //    public ICommand IncreaseCountCommand { get; }

    //    void IncreaseCount() =>
    //        CountDisplay = $"You clicked {++count} times";


    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    //}
}
