using Elastic.Code;
using Elastic.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elastic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalyzerSearchPage : ContentPage
    {
        SearchBar searchBar;
        Label total;

        public ObservableCollection<ResultObj> ItemList { get; set; }

        public AnalyzerSearchPage()
        {
            InitializeComponent();

            BindingContext = new AnalyzerSearchPageViewModel();

            // total lb
            total = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Total match: 0",
                FontSize = 16
            };
            // search bar
            searchBar = new SearchBar
            {
                Placeholder = "Enter keyword: ",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,

                SearchCommand = new Command(async () =>
                {
                    //resultLabel.Text = "Result: " + searchBar.Text + " is what you want.";
                    string user_input = searchBar.Text;
                    SearchBoxModel model = new SearchBoxModel(user_input);
                    model.result = await model.getData_Analyzer();

                    total.Text = "Total match: " + model.result.Count.ToString();

                    ItemList = new ObservableCollection<ResultObj>();
                    foreach (var item in model.result)
                    {
                        ItemList.Add(item);
                    }

                    BindingContext = new AnalyzerSearchPageViewModel(ItemList);


                })
            };


            headerLayout.Children.Add(searchBar);
            headerLayout.Children.Add(total);
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }



    public class AnalyzerSearchPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ResultObj> Items { get; set; }

        public ObservableCollection<Grouping<string, ResultObj>> ItemsGrouped { get; }

        public AnalyzerSearchPageViewModel()
        {
            Items = new ObservableCollection<ResultObj>();
            Items.Add(new ResultObj()
            {
                title = "waiting...",
                description = "waiting...",
                district = "waiting...",
                image = "waiting..."
            });
        }

        public AnalyzerSearchPageViewModel(ObservableCollection<ResultObj> ItemList)
        {
            Items = ItemList;
            
            RefreshDataCommand = new Command(
                async () => await RefreshData());

        }
        public string Input { get; set; }


        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}
