

namespace WEB_API_DataServer.Models.Account
{
    public class Database
    {
        private static List<Account> accounts = new List<Account>();
        private static AccountFactory acctFactory = new AccountFactory();
        private static int NUM_ACCOUNTS = 10000;

        public static void Generate()
        {
            accounts = new List<Account>();
            for (int ii = 0; ii < NUM_ACCOUNTS; ii++)
            {
                accounts.Add(new Account(acctFactory));
            }
        }

        public static Account GetAcctByIndex(int index)
        {
            if(index >= 0 && index < NUM_ACCOUNTS)
            {
                return accounts[index];
            }
            else
            {
                return null;
            }
        }

        public static int GetNumRecords()
        {
            return NUM_ACCOUNTS;
        }

        public static Account GetAcctBySurname(string name)
        {
            foreach(Account acct in accounts)
            {
                if(acct.lastName.Contains(name))
                {
                    return acct;
                }    
            }

            return null;
        }
    }
}