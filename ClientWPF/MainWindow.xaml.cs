using API_Classes;
using Newtonsoft.Json;
using RestSharp;
using System.Windows;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void IndexButton_Click(object sender, RoutedEventArgs e)
        {
            RestRequest restRequest = new RestRequest("/api/account/values/{index}", Method.Get);
            restRequest.AddUrlSegment("index", SearchBox.Text);
            RestResponse restResponse = restClient.Execute(restRequest);

            DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);
           
            fNameBox.Text = data.fname;
            lNameBox.Text = data.lname;
            acctNoBox.Text = data.acct.ToString();
            pinBox.Text = data.pin.ToString("D4");
            balBox.Text = "$" + data.bal.ToString();
        }

        private void SurnameButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SurnameBox.Text;
            SearchData data = new SearchData();
            data.searchStr = searchTerm;

            RestRequest restRequest = new RestRequest("/api/search/surname", Method.Post);
            restRequest.RequestFormat = RestSharp.DataFormat.Json;
            restRequest.AddBody(data);
            RestResponse restResponse = restClient.Execute(restRequest);

            DataIntermed result = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);

            if (result == null)
            {
                ErrorMsg("No results for that surname!");
            }
            else
            {
                fNameBox.Text = result.fname;
                lNameBox.Text = result.lname;
                acctNoBox.Text = result.acct.ToString();
                pinBox.Text = result.pin.ToString("D4");
                balBox.Text = "$" + result.bal.ToString();
            }
        }
    }
}