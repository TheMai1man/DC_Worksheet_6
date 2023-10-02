using System.Runtime.Serialization;

namespace FaultLib
{
    [DataContract]
    public class IndexFault
    {
        private string detail;
        private string operation;

        public IndexFault(string message, int index)
        {
            this.detail = message;
            this.operation = "Attempted access of index: " + index.ToString();
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