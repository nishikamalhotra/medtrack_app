using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using MedTrack.Entity;
using System.Threading.Tasks;

namespace MedTrack.Service
{
    public class DynamoDBService
    {
        public readonly DynamoDBContext DbContext;
        public AmazonDynamoDBClient DynamoClient;

        public DynamoDBService()
        {


            DynamoClient = new AmazonDynamoDBClient("", "", Amazon.RegionEndpoint.USEast1);

            DbContext = new DynamoDBContext(DynamoClient, new DynamoDBContextConfig
            {
                //Setting the Consistent property to true ensures that you'll always get the latest 
                ConsistentRead = true,
                SkipVersionCheck = true
            });
        }

        /// <summary>
        /// The Store method allows you to save a POCO to DynamoDb
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Store<T>(T item) where T : new()
        {
            DbContext.SaveAsync<T>(item);

        }


        /// <summary>
        /// Method Updates and existing item in the table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        async public void UpdateItem<T>(T item) where T : class
        {
            T savedItem = await DbContext.LoadAsync(item);

            if (savedItem == null)
            {
                throw new AmazonDynamoDBException("The item does not exist in the Table");
            }

            await DbContext.SaveAsync(item);
        }

        public Search GetAllItem(String table)
        {
            Table tablename = Table.LoadTable(DynamoClient, table);
            ScanFilter scanFilter = new ScanFilter();
            Search getAllItems = tablename.Scan(scanFilter);
            return getAllItems;
        }

        /// <summary>
        /// Uses the scan operator to retrieve all items in a table
        /// <remarks>[CAUTION] This operation can be very expensive if your table is large</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        async public Task<int> GetAllLocation<T>(string tableName) where T : class
        {
            List<Document> allItems = await (GetAllItem(tableName)).GetRemainingAsync();
            int newID = allItems.Count;
            return newID;
        }

        async public Task<int> GetAllPatient<T>(string tableName) where T : class
        {
            List<Document> allItems = await (GetAllItem(tableName)).GetRemainingAsync();
            int newID = allItems.Count;
            return newID;
        }

        async public Task<int> GetAllPhysician<T>(string tableName) where T : class
        {
            List<Document> allItems = await (GetAllItem(tableName)).GetRemainingAsync();
            int newID = allItems.Count;
            return newID;
        }

        async public Task<int> GetAllPharmacy<T>(string tableName) where T : class
        {
            List<Document> allItems = await (GetAllItem(tableName)).GetRemainingAsync();
            int newID = allItems.Count;
            return newID;
            //pharmacy.PharmacyID = allItems.Count;
        }

        async public Task<int> GetAllPrescription<T>(string tableName) where T : class
        {
            List<Document> allItems = await (GetAllItem(tableName)).GetRemainingAsync();
            int newID = allItems.Count;
            return newID;
        }

        public async void FindMedicineWithBarcode(long barcode, string table)
        {
            GetItemOperationConfig config = new GetItemOperationConfig()
            {
                AttributesToGet = new List<string>() { "Barcode" },
            };

            //  Primitive key = Medicine
            Table tablename = Table.LoadTable(DynamoClient, table);
            //  tablename.GetItemAsync(Primitive Medicine.MedicineID, Primitive sortKey, GetItemOperationConfig config)
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("Barcode", ScanOperator.Equal, barcode);
            ScanOperationConfig ScanConfig = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "MedicineID", "Barcode" },
                Filter = scanFilter
            };
            Search getMedicine = tablename.Scan(ScanConfig);
            List<Document> result = await getMedicine.GetRemainingAsync();
            foreach (Document item in result)
            {
                foreach (string key in item.Keys)
                {
                    DynamoDBEntry dbEntry = item[key];
                    string val = dbEntry.ToString();
                    if (key.ToLower() == "Barcode")
                    {
                        List<string> barcodes = dbEntry.AsListOfString();
                        StringBuilder valueBuilder = new StringBuilder();
                        foreach (string code in barcodes)
                        {
                            valueBuilder.Append(code).Append(", ");
                        }
                        val = valueBuilder.ToString();
                    }
                    Console.WriteLine(string.Format("Property: {0}, value: {1}", key, val));
                }
            }
        }

        public async Task<string> FindPrescriptionForCurrentDate(string date)
        {
            ScanFilter filter = new ScanFilter();
            ScanOperator op = ScanOperator.Equal;
            string attrName = "StartDate";
            filter.AddCondition(attrName, op, date);

            Table tablename = Table.LoadTable(DynamoClient, "Prescription");

            List<Document> prescriptions = await (tablename.Scan(filter)).GetRemainingAsync();
            int newID = prescriptions.Count;
            string medicineName = "";
            string numberOfTime = "";
            string returnValue = "";
            foreach (Document item in prescriptions)
            {
                foreach (string key in item.Keys)
                {

                    if (key == "Barcode")
                    {
                        DynamoDBEntry dbEntry = item[key];
                        string val = dbEntry.ToString();
                        long barcodeValue = Convert.ToInt64(val);
                        //find medicine name 
                        medicineName = await FindMedicineNameByBarcode(barcodeValue);
                    } 

                    if (key == "NumberOfTime")
                    {
                        DynamoDBEntry dbEntry = item[key];
                        numberOfTime = dbEntry.ToString();
                    }
                    returnValue = medicineName + " & " + numberOfTime;    
                                  
                }
            }
            return returnValue;
        }

        public async Task<string> FindMedicineNameByBarcode(long barcode)
        {
            ScanFilter scanFilter = new ScanFilter();
            ScanOperator ope = ScanOperator.Equal;
            string att = "Barcode";
            scanFilter.AddCondition(att, ope, barcode);

            Table table = Table.LoadTable(DynamoClient, "Medicine");

            List<Document> medicine = await (table.Scan(scanFilter)).GetRemainingAsync();
            int id = medicine.Count;
            string val = "";
            foreach (Document item in medicine)
            {
                foreach (string key in item.Keys)
                {

                    if (key == "Name")
                    {
                        DynamoDBEntry dbEntry = item[key];
                        val = dbEntry.ToString();
                    }
                    
                }
            }
            return val;
        }
    }
}
