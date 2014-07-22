using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SQLConnector;
using System.Data;
using SuperMarket.ResponceClass;


namespace SuperMarket.Classes
{
    public class Sales
    {
        const string SUCCESS_MSG = "Successfully {0} a book";
        const string Failure_MSG = "Unable to {0} a book";
        public int Id { get; set; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public decimal SP { set; get; }


       public static Response Create(Sales objSales)
        {
            SQLConnector.DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string stInsert = @"INSERT INTO [Sales]  ([Name] ,[Quantity],[SP])
                VALUES (@Name,@Quantity,@SP)";
            string[] strAddParameterName = new string[] { "Name",  "Quqntity", "SP" };
            object[] objAddparametervalue = new object[] { objSales.Name, objSales.Quantity, objSales.SP};
            if (!objdb.ExecuteQuery(stInsert, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "insert"));

            else

                return new Response(5555, string.Format(SUCCESS_MSG, "inserted"));
        }

     /*  public  static Response Delete(Sales objSales)
         {
             DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
             string strDelete = "DELETE FROM [UsersTable] WHERE Id=@Id";
             string[] strAddParameterName = new string[] { "Id" };
             object[] objAddparametervalue = new object[] { objSales.Id };
             if (!objdb.ExecuteQuery(strDelete, strAddParameterName, objAddparametervalue))

                 return new Response(9999, string.Format(Failure_MSG, "delete"));
             else

                 return new Response(5555, string.Format(SUCCESS_MSG, "deleted"));
         }

         public Response Update()
         {
             DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
             string strUpsate = @"UPDATE [Sales]
               SET [Name] = @Name
                  ,[Quantity] = @Quantity
                  ,[SP] = @SP
                          WHERE Id=@Id ";
             string[] strAddParameterName = new string[] { "Name", "Quantity", "SP", "Id" };
             object[] objAddparametervalue = new object[] { this.Name, this.Quantity,this.SP, this.Id };
             if (!objdb.ExecuteQuery(strUpsate, strAddParameterName, objAddparametervalue))
                 return new Response(9999, string.Format(Failure_MSG, "update"));
             else
                 return new Response(5555, string.Format(SUCCESS_MSG, "updated"));
         }*/

         public static Sales Get(int Id)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            Sales objSales = new Sales();
            string strGetOneRecord = "SELECT * FROM [Sales] where Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objSales.Id };
            System.Data.DataTable objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);
            return objSales;
        }

      /*   public static List<Sales> GetAll()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            List<Sales> lstSales = new List<Sales>();
            Sales objSales = new Sales();
            string strGetOneRecord = "SELECT * FROM [Sales]";
            string[] strAddParameterName = new string[] { };
            object[] objAddparametervalue = new object[] { };
            DataTable objtab = new DataTable();
            objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);

            for (int i = 0; i < objtab.Rows.Count; i++)
            {
                int intId;
                int intQuqntity;
                decimal decSP;
                objSales = new Sales();
                int.TryParse(objtab.Rows[i]["Id"].ToString(), out intId);
                objSales.Id = intId;
                objSales.Name = objtab.Rows[i]["Name"] != null ? objtab.Rows[i]["Name"].ToString() : string.Empty;               
                int.TryParse(objtab.Rows[i]["Quantity"].ToString(), out intQuqntity);
                objSales.Quantity= intQuqntity;
                decimal.TryParse(objtab.Rows[i]["SP"].ToString(), out decSP);
                objSales.SP = decSP;
                lstSales.Add(objSales);
            }
            return lstSales;
        }
        */
    }
}