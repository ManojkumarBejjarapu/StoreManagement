using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SqlConnector;
using System.Data;
using SuperMarket.ResponceClass;

namespace SuperMarket.Classes
{
    public class Items
    {
        const string SUCCESS_MSG = "Successfully {0}  book";
        const string Failure_MSG = "Unable to {0} a book";
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CP { set; get; }
        public decimal SP { set; get; }
        public int StockAvailable { get; set; }

        public Response Create()
        {
            SqlConnector.DBOperations objdb = new DBOperations(StoreManagement.Properties.Settings.Default.StoreManagement);
            string stInsert = @"INSERT INTO [Items]([Name],[CP],[SP],[StockAvailable])
             VALUES(@Name, @CP,@SP, @StockAvailable";
            string[] strAddParameterName = new string[] { "Name", "CP", "SP" ,"StockAvailable"};
            object[] objAddparametervalue = new object[] { this.Name, this.CP, this.SP,this.StockAvailable };
            if (!objdb.QuerryExecutor(stInsert, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "insert"));

            else

                return new Response(5555, string.Format(SUCCESS_MSG, "inserted"));
        }

        public Response Delete()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strDelete = "DELETE FROM [Items] WHERE Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { this.Id };
            if (!objdb.ExecuteQuery(strDelete, strAddParameterName, objAddparametervalue))

                return new Response(9999, string.Format(Failure_MSG, "delete"));
            else

                return new Response(5555, string.Format(SUCCESS_MSG, "deleted"));
        }

        public Response Update()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strUpsate = @"UPDATE [Items]
            SET [Name] = @Name
            ,[CP] = @CP
             ,[SP] = @SP
            ,[StockAvailable] = @StockAvailable, int,>
                WHERE Id=@Id ";
            string[] strAddParameterName = new string[] { "Name", "CP", "SP", "Id" };
            object[] objAddparametervalue = new object[] { this.Name, this.CP, this.SP, this.Id };
            if (!objdb.ExecuteQuery(strUpsate, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "update"));
            else
                return new Response(5555, string.Format(SUCCESS_MSG, "updated"));
        }

        public static Items Get(int workerId)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);

            Items objItems = new Items();
            string strGetOneRecord = "SELECT * FROM [Items] where Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objItems.Id };
            System.Data.DataTable objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);
            return objItems;
        }

        public static List<Items> GetAll()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            List<Items> lstItems = new List<Items>();
            Items objItems = new Items();

            string strGetOneRecord = "SELECT * FROM [Items]";
            string[] strAddParameterName = new string[] { };
            object[] objAddparametervalue = new object[] { };
            DataTable objtab = new DataTable();
            objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);

            for (int i = 0; i < objtab.Rows.Count; i++)
            {
                int intId;
                int intStockAvailable;
                decimal decCP;
                decimal decSP;
                objItems = new Items();
                int.TryParse(objtab.Rows[i]["Id"].ToString(), out intId);
                objItems.Id = intId;
                objItems.Name = objtab.Rows[i]["Name"] != null ? objtab.Rows[i]["Name"].ToString() : string.Empty;
                decimal.TryParse(objtab.Rows[i]["CP"].ToString(), out decCP);
                objItems.CP = decCP;
                decimal.TryParse(objtab.Rows[i]["SP"].ToString(), out decSP);
                objItems.SP = decSP;
                lstItems.Add(objItems);
            }
            return lstItems;

        }


    }
}