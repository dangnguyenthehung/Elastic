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
using Newtonsoft.Json;
using Elastic.Code;

namespace Elastic
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geocoding : ContentPage
    {
        Geocoder geoCoder;

        public Geocoding()
        {
            InitializeComponent();
        }

        private async void detectGPS(object sender, EventArgs e)
        {

            double latitude = 0;
            double longitude = 0;

            TimeStamp.Text = "Time: ";
            Longitude.Text = "Longitude: ";
            Latitude.Text = "Latitude: ";
            AddressComponents.Text = "Address Components: ";
            AddressFormatted.Text = "Formatted Address: ";
            // get GPS location
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 20;

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 60000); // 1 minute timeout
                if (position == null)
                    return;
                latitude = position.Latitude;
                longitude = position.Longitude;

                TimeStamp.Text += position.Timestamp.ToString();
                Longitude.Text += longitude.ToString();
                Latitude.Text += latitude.ToString();


            }
            catch (Exception ex)
            {
                TimeStamp.Text = ex.ToString();
                System.Diagnostics.Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }

            try
            {
                ReverseAddress(latitude, longitude);
            }
            catch (Exception ex)
            {
                AddressComponents.Text = "Cannot get address components at the moment!";
                AddressFormatted.Text = "Cannot get details address at the moment!";
            }


        }

        private async void ReverseAddress(double latitude, double longitude)
        {
            try
            {
                string latlng = latitude.ToString() + "," + longitude.ToString();
                GeocodingModel model = new GeocodingModel(latlng);
                model.result = await model.getAddress();

                var possibleAddresses = model.result;

                //Address.Text += possibleAddresses.results[0].formatted_address;
                
                foreach (var address in possibleAddresses.results[0].address_components)
                {
                    AddressComponents.Text += "\n" + address.types[0] + ": " + address.long_name;
                    
                }
                AddressFormatted.Text += possibleAddresses.results[0].formatted_address;
            }
            catch (Exception ex)
            {
                AddressFormatted.Text = ex.ToString();
            }

        }

        //private void FnGeoCodeApi(string latlng)
        //{
        //    string apiKeyUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng="+ latlng +"&key=AIzaSyA2qTf0l1JRZQ_jHv_x8vhpl1HH7Lp2UaU";
        //    //string strGeoCode = string.Format("address={0}", "Silicon Valley,CA,USA");
        //    string strGeoCodeFullURL = string.Format(Constants.strGeoCodingUrl, strGeoCode);
        //    string strResult = await FnHttpRequest(strGeoCodeFullURL);
        //    if (strResult != Constants.strException)
        //    {
        //        var objGeoCodeClass = JsonConvert.DeserializeObject<GoogleGeoCodeClass>(strResult);
        //        if (objGeoCodeClass.status == "OK")
        //        {
        //            var Position = new LatLng(objGeoCodeClass.results[0].geometry.location.lat, objGeoCodeClass.results[0].geometry.location.lng);
        //            string address = objGeoCodeClass.results[0].formatted_address;
        //        }
        //    }
        //}
        //WebClient webclient;
        //async Task<string> FnHttpRequest(string strUri)
        //{
        //    webclient = new WebClient();
        //    string strResultData;
        //    try
        //    {
        //        strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
        //        Console.WriteLine(strResultData);
        //    }
        //    catch
        //    {
        //        strResultData = Constants.strException;
        //    }
        //    finally
        //    {
        //        if (webclient != null)
        //        {
        //            webclient.Dispose();
        //            webclient = null;
        //        }
        //    }
        //    return strResultData;
        //}

    }

}
