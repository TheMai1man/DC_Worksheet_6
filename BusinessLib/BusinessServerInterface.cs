using System.ServiceModel;
using FaultLib;
using System;

namespace BusinessLib
{
    [ServiceContract]
    public interface BusinessServerInterface
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