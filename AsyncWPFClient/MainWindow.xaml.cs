using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AsyncWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private string searchValue;

        public MainWindow()
        {
            InitializeComponent();

            //factory that generates remote connections to our remote class
            //hides the remote procedure calls(RPC)
            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //set the URL and create the connection
            string URL = "net.tcp://localhost:8200/BusinessService";
            foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchValue = SurnameSearchBox.Text;

            Task<Account> task = new Task<Account>(SurnameSearch);
            task.Start();

            ToggleGUI();

            Account account = await task;

            ToggleGUI();

            if (account != null) {SetGUI(account);}
        }


        private Account SurnameSearch()
        {
            string fName, lName;
            uint acctNo, pin;
            int bal;
            byte[] profilePic;

            try
            {
                foob.GetValuesForSurname(searchValue, out acctNo, out pin, out bal, out fName, out lName, out profilePic);
                return new Account(fName, lName, bal, acctNo, pin, profilePic);
            }
            catch(FaultException<SearchFault> ex)
            {
                ErrorMsg("FaultException<SearchFault>", ex.Detail.Message);
                return null;
            }
        }


        // Convert a byte array to a BitmapImage
        private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage image = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            return image;
        }


        // Display error message to user on the GUI
        private void ErrorMsg(string x, string y)
        {
            fNameBox.Dispatcher.Invoke(new Action(() => fNameBox.Text = x));
            lNameBox.Dispatcher.Invoke(new Action(() => lNameBox.Text = y));
            balBox.Dispatcher.Invoke(new Action(() => balBox.Text = ""));
            acctNoBox.Dispatcher.Invoke(new Action(() => acctNoBox.Text = ""));
            pinBox.Dispatcher.Invoke(new Action(() => pinBox.Text = ""));
            pictureBox.Dispatcher.Invoke(new Action(() => pictureBox.Source = null));
        }


        // Set the result fields on the GUI
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetGUI(Account account)
        {
            fNameBox.Dispatcher.Invoke(new Action(() => fNameBox.Text = account.FName));
            lNameBox.Dispatcher.Invoke(new Action(() => lNameBox.Text = account.LName));
            balBox.Dispatcher.Invoke(new Action(() => balBox.Text = "$" + account.Bal.ToString()));
            acctNoBox.Dispatcher.Invoke(new Action(() => acctNoBox.Text = account.Acct.ToString()));
            pinBox.Dispatcher.Invoke(new Action(() => pinBox.Text = account.Pin.ToString("D4")));
            pictureBox.Dispatcher.Invoke(new Action(() => pictureBox.Source = ByteArrayToBitmapImage(account.Image)));
        }


        // toggles the state of GUI elements depending on async task status
        private void ToggleGUI()
        {
            Dispatcher.Invoke(new Action(() => SurnameSearchBox.IsReadOnly = !(SurnameSearchBox.IsReadOnly)));
            Dispatcher.Invoke(new Action(() => SearchButton.IsEnabled = !(SearchButton.IsEnabled)));
            Dispatcher.Invoke(new Action(() => ProgressBar.IsIndeterminate = !(ProgressBar.IsIndeterminate)));
        }
    }
}
