using BusinessLib;
using System;
using System.ServiceModel;

namespace BusinessTier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my WCF Business Server for worksheet 4+5!\n");

            //the actual host service system
            ServiceHost host;
            //represents a tcp/ip binding in the windows network stack
            NetTcpBinding tcp = new NetTcpBinding();

            //bind server to the implementation of DataServer
            host = new ServiceHost(typeof(BusinessServer));
            //Present the publicly accesible interface to the client
            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://localhost:8200/BusinessService");
            //open the host for business
            host.Open();
            Console.WriteLine("System Online");

            // pause for input (keeps server running)
            Console.ReadLine();
            // close the host when done
            host.Close();
        }
    }
}