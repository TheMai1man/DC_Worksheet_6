using System;
using System.ServiceModel;
using FaultLib;

namespace DataServerLib
{
    [ServiceContract]
    public interface DataServerInterface
    {
        [OperationContract]
        int GetNumEntries();
        [OperationContract]
        [FaultContract(typeof(IndexFault))]
        int GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out byte[] profilePic);
        [OperationContract]
        [FaultContract(typeof(SearchFault))]
        void GetValuesForSurname(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Byte[] profilePic);
    }
}