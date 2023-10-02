using System.ServiceModel;
using DataServerLib;
using BusinessLib;
using FaultLib;
using System;
using System.Runtime.CompilerServices;

namespace BusinessTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BusinessServer : BusinessServerInterface
    {
        private NetTcpBinding tcp;
        private string URL = "net.tcp://localhost:8100/DataService";
        private ChannelFactory<DataServerInterface> foobFactory;
        private DataServerInterface foob;
        private static uint LogNumber = 0;


        public BusinessServer()
        {
            tcp = new NetTcpBinding();
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        public int GetNumEntries()
        {
            int result = foob.GetNumEntries();
            Log("fetched number of entries in database: " + result);
            return result;
        }


        // deprecated from worksheet 2+3
        public int GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] profilePic)
        {
            try
            {
                foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out profilePic);
            }
            catch(FaultException<IndexFault> e)
            {
                throw new FaultException<IndexFault>(e.Detail, e.Detail.Message);
            }

            return index;
        }


        public void GetValuesForSurname(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Byte[] profilePic)
        {
            try
            {
                foob.GetValuesForSurname(searchTerm, out acctNo, out pin, out bal, out fName, out lName, out profilePic);
                Log("Search by surname called with search term \"" + searchTerm + "\" with success");
            }
            catch (FaultException<SearchFault> e)
            {
                Log("Search by surname called with search term \"" + searchTerm + "\" and has failed");
                throw new FaultException<SearchFault>(e.Detail, e.Detail.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string logString)
        {
            LogNumber++;
            Console.WriteLine(LogNumber + ": " + logString);
        }
    }
}
