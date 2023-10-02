
namespace WEB_API_DataServer.Models.Account
{
    public class Account
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;

        public Account(AccountFactory acctFactory)
        {
            acctFactory.MakeAccount(out pin, out acctNo, out firstName, out lastName, out balance);
        }
    }
}