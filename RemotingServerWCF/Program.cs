using System;
using System.ServiceModel;
using DataServerLib;

namespace DataTier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my WCF Data Server for worksheet 4+5!\n");

            //the actual host service system
            ServiceHost host;
            //represents a tcp/ip binding in the windows network stack
            NetTcpBinding tcp = new NetTcpBinding();

            //bind server to the implementation of DataServer
            host = new ServiceHost(typeof(DataServer));
            //Present the publicly accesible interface to the client
            host.AddServiceEndpoint(typeof(DataServerInterface), tcp, "net.tcp://0.0.0.0:8100/DataService");
            //open the host for business
            host.Open();
            Console.WriteLine("System Online");

            Console.ReadLine();
            //close the host when done
            host.Close();
        }
    }
}