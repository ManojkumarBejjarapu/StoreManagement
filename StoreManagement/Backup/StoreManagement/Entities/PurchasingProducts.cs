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
    public class PurchasingProducts
    {
        const string SUCCESS_MSG = "Successfully {0} a book";
        const string Failure_MSG = "Unable to {0} a book";
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }


        public Response Create(PurchasingProducts objPurchasingProducts)
        {
            SQLConnector.DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string stInsert = @"INSERT INTO [PurchasingProducts]([Name],[Quantity] ,[Price])
                VALUES(@Name,@Quantity,@Price)";
            string[] strAddParameterName = new string[] { "Name", "Quantity", "Price" };
            object[] objAddparametervalue = new object[] { objPurchasingProducts.Name, objPurchasingProducts.Quantity, objPurchasingProducts.Price };
            if (!objdb.ExecuteQuery(stInsert, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "insert"));

            else

                return new Response(5555, string.Format(SUCCESS_MSG, "inserted"));
        }

        public Response Delete(PurchasingProducts objPurchasingProducts)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strDelete = "DELETE FROM [PurchasingProducts] WHERE Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objPurchasingProducts.Id };
            if (!objdb.ExecuteQuery(strDelete, strAddParameterName, objAddparametervalue))

                return new Response(9999, string.Format(Failure_MSG, "delete"));
            else

                return new Response(5555, string.Format(SUCCESS_MSG, "deleted"));
        }

        public Response Update(PurchasingProducts objPurchasingProducts)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strUpsate = @"UPDATE [PurchasingProducts]
                  SET [Name] = @Name, 
                           [Quantity] = @Quantity,
                     [Price] = @Price
                WHERE Id=@Id";
            string[] strAddParameterName = new string[] { "Name", "Quantity", "Price", "Id" };
            object[] objAddparametervalue = new object[] { objPurchasingProducts.Name, objPurchasingProducts.Quantity, objPurchasingProducts.Price, objPurchasingProducts.Id };
            if (!objdb.ExecuteQuery(strUpsate, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "update"));
            else
                return new Response(5555, string.Format(SUCCESS_MSG, "updated"));
        }

        public static PurchasingProducts Get(int Id)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);

            PurchasingProducts objPurchasingProducts = new PurchasingProducts();
            string strGetOneRecord = "SELECT * FROM [PurchasingProducts] where Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objPurchasingProducts.Id };
            System.Data.DataTable objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);
            return objPurchasingProducts;
        }

        public static List<PurchasingProducts> GetAll()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            List<PurchasingProducts> lstPurchasingProducts = new List<PurchasingProducts>();
            PurchasingProducts objPurchasingProducts = new PurchasingProducts();

            string strGetOneRecord = "SELECT * FROM [PurchasingProducts]";
            string[] strAddParameterName = new string[] { };
            object[] objAddparametervalue = new object[] { };
            DataTable objtab = new DataTable();
            objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);

            for (int i = 0; i < objtab.Rows.Count; i++)
            {
                int intId;
                int intQuantity;
                decimal decPrice;
                objPurchasingProducts = new PurchasingProducts();
                int.TryParse(objtab.Rows[i]["Id"].ToString(), out intId);
                objPurchasingProducts.Id = intId;
                objPurchasingProducts.Name = objtab.Rows[i]["Name"] != null ? objtab.Rows[i]["Name"].ToString() : string.Empty;
                int.TryParse(objtab.Rows[i]["Quqntity"].ToString(), out intQuantity);
                objPurchasingProducts.Quantity = intQuantity;
                decimal.TryParse(objtab.Rows[i]["Price"].ToString(), out decPrice);
                objPurchasingProducts.Price = decPrice;
                lstPurchasingProducts.Add(objPurchasingProducts);
            }
            return lstPurchasingProducts;

        }

    }
}