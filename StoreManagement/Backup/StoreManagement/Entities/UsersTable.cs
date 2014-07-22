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
    public class UsersTable
    {
        const string SUCCESS_MSG = "Successfully {0} a book";
        const string Failure_MSG = "Unable to {0} a book";
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Password { set; get; }



        public Response Create(UsersTable objUsersTable)
        {
            SQLConnector.DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string stInsert = @"INSERT INTO [UsersTable]([Name] ,[Password])
             VALUES (@Name,@Password)";
            string[] strAddParameterName = new string[] { "Name", "Password"};
            object[] objAddparametervalue = new object[] { objUsersTable.Name, objUsersTable.Password };
            if (!objdb.ExecuteQuery(stInsert, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "insert"));

            else

                return new Response(5555, string.Format(SUCCESS_MSG, "inserted"));
        }

        public Response Delete(UsersTable objUsersTable)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strDelete = "DELETE FROM [UsersTable] WHERE Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objUsersTable.Id };
            if (!objdb.ExecuteQuery(strDelete, strAddParameterName, objAddparametervalue))

                return new Response(9999, string.Format(Failure_MSG, "delete"));
            else

                return new Response(5555, string.Format(SUCCESS_MSG, "deleted"));
        }

        public Response Update(UsersTable objUsersTable)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            string strUpsate = @"UPDATE [SuperMarket].[dbo].[UsersTable]
            SET [Name] = @Name
            ,[Password] = @Password, nvarchar(50),>
             WHERE Id=@Id ";
            string[] strAddParameterName = new string[] { "Name", "Password","Id" };
            object[] objAddparametervalue = new object[] { objUsersTable.Name, objUsersTable.Password, objUsersTable.Id };
            if (!objdb.ExecuteQuery(strUpsate, strAddParameterName, objAddparametervalue))
                return new Response(9999, string.Format(Failure_MSG, "update"));
            else
                return new Response(5555, string.Format(SUCCESS_MSG, "updated"));
        }

        public static UsersTable Get(int workerId)
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);

            UsersTable objUsersTable = new UsersTable();
            string strGetOneRecord = "SELECT * FROM [UsersTable] where Id=@Id";
            string[] strAddParameterName = new string[] { "Id" };
            object[] objAddparametervalue = new object[] { objUsersTable.Id };
            System.Data.DataTable objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);
            return objUsersTable;
        }

        public static List<UsersTable> GetAll()
        {
            DBOperations objdb = new DBOperations(Properties.Settings.Default.payRoll);
            List<UsersTable> lstUsersTable = new List<UsersTable>();
            UsersTable objUsersTable = new UsersTable();

            string strGetOneRecord = "SELECT * FROM [UsersTable]";
            string[] strAddParameterName = new string[] { };
            object[] objAddparametervalue = new object[] { };
            DataTable objtab = new DataTable();
            objtab = objdb.ExecuteGetAllQuery(strGetOneRecord, strAddParameterName, objAddparametervalue);

            for (int i = 0; i < objtab.Rows.Count; i++)
            {
                int intId;
                objUsersTable = new UsersTable();
                int.TryParse(objtab.Rows[i]["Id"].ToString(), out intId);
                objUsersTable.Id = intId;
                objUsersTable.Name = objtab.Rows[i]["Name"] != null ? objtab.Rows[i]["Name"].ToString() : string.Empty;
                objUsersTable.Password = objtab.Rows[i]["Password"] != null ? objtab.Rows[i]["Password"].ToString() : string.Empty;
                lstUsersTable.Add(objUsersTable);
            }
            return lstUsersTable;

        }


    }
}