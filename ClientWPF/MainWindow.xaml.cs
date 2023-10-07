using API_Classes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private RestClient restClient = new RestClient("http://localhost:5076");

        public MainWindow()
        {
            InitializeComponent();

            RestClient restClient = new RestClient("http://localhost:5076");
            RestRequest restRequest = new RestRequest("/api/default/total", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            int total = JsonConvert.DeserializeObject<int>(restResponse.Content);

            totalBox.Text = totalBox.Text + total;
        }


        // Display error message to user on the GUI
        private void ErrorMsg(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private async void IndexButton_Click(object sender, RoutedEventArgs e)
        {
            DataIntermed data = await IndexSearch(SearchBox.Text);

            ToggleGUI();

            UpdateGUI(data);

            ToggleGUI();
        }


        private async Task<DataIntermed> IndexSearch(string index)
        {
            RestRequest restRequest = new RestRequest("/api/account/values/{index}", Method.Get);
            restRequest.AddUrlSegment("index", index);
            RestResponse restResponse = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);
        }


        private async void SurnameButton_Click(object sender, RoutedEventArgs e)
        {
            DataIntermed data = await SurnameSearch(SurnameBox.Text);

            ToggleGUI();

            UpdateGUI(data);

            ToggleGUI();

            if (data == null)
            {
                ErrorMsg("No results for that surname!");
            }
        }


        private async Task<DataIntermed> SurnameSearch(string surname)
        {
            SearchData data = new SearchData();
            data.SearchStr = surname;

            RestRequest restRequest = new RestRequest("/api/search/surname", Method.Post);
            restRequest.RequestFormat = RestSharp.DataFormat.Json;
            restRequest.AddBody(data);
            RestResponse restResponse = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);
        }


        private void ToggleGUI()
        {
            Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = !(SearchBox.IsReadOnly)));
            Dispatcher.Invoke(new Action(() => SearchButton.IsEnabled = !(SearchButton.IsEnabled)));
            Dispatcher.Invoke(new Action(() => SurnameBox.IsReadOnly = !(SurnameBox.IsReadOnly)));
            Dispatcher.Invoke(new Action(() => SurnameButton.IsEnabled = !(SurnameButton.IsEnabled)));
            Dispatcher.Invoke(new Action(() => SurnameButton.IsEnabled = !(SurnameButton.IsEnabled)));
        }


        private void UpdateGUI(DataIntermed data)
        {
            if (data != null)
            {
                Dispatcher.Invoke(new Action(() => fNameBox.Text = data.Fname));
                Dispatcher.Invoke(new Action(() => lNameBox.Text = data.Lname));
                Dispatcher.Invoke(new Action(() => acctNoBox.Text = data.Acct.ToString()));
                Dispatcher.Invoke(new Action(() => pinBox.Text = data.Pin.ToString("D4")));
                Dispatcher.Invoke(new Action(() => balBox.Text = "$" + data.Bal.ToString()));
            }
        }
    }
}