
using System.Collections.Generic;

namespace DatabaseLib
{
    public class Database
    {
        List<DataStruct> accounts;
        private DatabaseGenerator dataGen = new DatabaseGenerator();

        public Database()
        {
            accounts = new List<DataStruct>();
            for (int ii = 0; ii < 100; ii++)
            {
                accounts.Add(new DataStruct(dataGen));
            }
        }

        public Account GetAcctByIndex(int index)
        {
            return accounts[index];
        }

        public int GetNumRecords()
        {
            return accounts.Count;
        }
    }
}