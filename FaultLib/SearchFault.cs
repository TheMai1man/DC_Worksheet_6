using System.Runtime.Serialization;

namespace FaultLib
{
    [DataContract]
    public class SearchFault
    {
        private string detail;
        private string operation;

        public SearchFault(string message, string searchTerm)
        {
            this.detail = message;
            this.operation = "Attempted to find last name containing: " + searchTerm;
        }

        [DataMember]
        public string Message
        {
            get { return this.detail; }
            set { this.detail = value; }
        }

        [DataMember]
        public string Operation
        {
            get { return this.operation; }
            set { this.operation = value; }
        }
    }
}