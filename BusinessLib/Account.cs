using System.Windows.Media.Imaging;

namespace BusinessLib
{
    public class Account
    {
        public string fName;
        public string lName;
        public int bal;
        public uint acct;
        public uint pin;
        public byte[] image;

        public Account(string fName, string lName, int bal, uint acct, uint pin, byte[] image)
        {
            this.fName = fName;
            this.lName = lName;
            this.bal = bal;
            this.acct = acct;
            this.pin = pin;
            this.image = image;
        }

        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }

        public string LName
        {
            get { return lName; }
            set { lName = value; }
        }

        public int Bal
        {
            get { return bal; }
            set { bal = value; }
        }

        public uint Acct
        {
            get { return acct; }
            set { acct = value; }
        }

        public uint Pin
        {
            get { return pin; }
            set { pin = value; }
        }

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}