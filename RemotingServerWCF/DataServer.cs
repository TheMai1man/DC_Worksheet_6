using System;
using System.IO;
using System.ServiceModel;
using DatabaseLib;
using DataServerLib;
using FaultLib;
using System.Drawing;
using System.Drawing.Imaging;

namespace DataTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        private Database db;

        public DataServer()
        {
            db = new Database();
        }

        public int GetNumEntries()
        {
            return db.GetNumRecords();
        }

        public int GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Byte[] profilePic)
        {
            try
            {
                acctNo = db.GetAcctNoByIndex(index);
                pin = db.GetPINByIndex(index);
                bal = db.GetBalanceByIndex(index);
                fName = db.GetFirstNameByIndex(index);
                lName = db.GetLastNameByIndex(index);
                profilePic = BitmapToByteArray(db.GetProfilePicByIndex(index));
            }
            catch(ArgumentOutOfRangeException e)
            {
                IndexFault iFault = new IndexFault(e.Message, index);
                throw new FaultException<IndexFault>(iFault, e.Message);
            }

            return index;
        }

        public void GetValuesForSurname(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Byte[] profilePic)
        {
            acctNo = 0; pin = 0; bal = 0; fName = ""; lName = ""; profilePic = null;
            Boolean found = false;

            try
            {
                for ( int ii = 0; ii < db.GetNumRecords(); ii++ )
                {
                    if( db.GetLastNameByIndex(ii).Contains(searchTerm) )
                    {
                        acctNo = db.GetAcctNoByIndex(ii);
                        pin = db.GetPINByIndex(ii);
                        bal = db.GetBalanceByIndex(ii);
                        fName = db.GetFirstNameByIndex(ii);
                        lName = db.GetLastNameByIndex(ii);
                        profilePic = BitmapToByteArray(db.GetProfilePicByIndex(ii));

                        found = true;

                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                IndexFault iFault = new IndexFault(e.Message, db.GetNumRecords());
                throw new FaultException<IndexFault>(iFault, e.Message);
            }

            if (!found)
            {
                SearchFault sFault = new SearchFault("Failed search by last name.", searchTerm);
                throw new FaultException<SearchFault>(sFault, "Failed search by last name.");
            }

        }

        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                byteArray = stream.ToArray();
            }

            return byteArray;
        }
    }
}