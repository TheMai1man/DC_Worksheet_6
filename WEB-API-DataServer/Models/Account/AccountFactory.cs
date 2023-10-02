
namespace WEB_API_DataServer.Models.Account
{
    public class AccountFactory
    {
        private Random rand = new Random();

        private string GetFirstName()
        {
            return randomName();
        }

        private string GetLastName()
        {
            return randomName();
        }

        private uint GetPIN()
        {
            return (uint)rand.Next(0, 9999);
        }

        private uint GetAcctNo()
        {
            return (uint)rand.Next(10000000, 99999999);
        }

        private int GetBalance()
        {
            return rand.Next(0, 99999);
        }

        
        private string randomName()
        {
            string name = "";

            name += (char)rand.Next(65, 90);                // intial is capital

            for (int ii = 0; ii < rand.Next(2, 10); ii++)   // subsequent letters 
            {
                name += (char)rand.Next(97, 122);
            }

            return name;
        }


        public void MakeAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();
        }

    }
}
