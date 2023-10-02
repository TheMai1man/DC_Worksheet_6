using System.Drawing;

namespace DatabaseLib
{
    internal class DataStruct
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;
        public Bitmap profilePic;

        public DataStruct(DatabaseGenerator dataGen)
        {
            dataGen.GetNextAccount(out pin, out acctNo, out firstName, out lastName, out balance, out profilePic);
        }
    }
}
