using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataAccess
    {
        static private string GetConnectionString()
        {
            return "Data Source=neslihan.database.windows.net;Initial Catalog=dataRaspberry;User ID=neslihan;Password=Nesli0623*---";
        }
        static public void ExecuteCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        static public void Insert(string tableName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                Delete(tableName);
                string command = "Insert into " + tableName + " (";

                foreach (var param in parameters)
                {
                    command += param.ParameterName + ",";
                }
                command = command.Substring(0, command.Length - 1);
                command += ") values (";

                foreach (var param in parameters)
                {
                    if (param.SqlDbType == SqlDbType.Int)
                    {
                        command += param.Value + ",";

                    }
                    else
                    {
                        command += "'" + param.Value + "'" + ",";
                    }


                }
                command = command.Substring(0, command.Length - 1);
                command += ")";
                ExecuteCommand(command);


            }
        }

        static public DataTable GetTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = "Select * from " + tableName;

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TABLENAME", tableName);
                command.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        static public DataTable ExecProcedure(string procedureName,SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = "Exec " + procedureName+ " ";

                foreach (var param in parameters)
                {
                    query += param.Value + ",";
                }
                query = query.Substring(0, query.Length - 1);
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        static public DataTable ExecNoParameterProcedure(string procedureName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = "Exec " + procedureName;

                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        static public DataTable GetTable(string tableName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string query = "Select * From " + tableName + " Where ";

                foreach (SqlParameter parameter in parameters)
                {
                    if (Convert.ToString(parameter.Value) != "")
                    {
                        query += parameter.ParameterName.Substring(1) + " ='" + parameter.Value + "' AND ";

                    }
                }
                query = query.Substring(0, query.Length - 5);
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                command.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        static public void Delete(string tableName)
        {
            string query = "Delete From " + tableName;


         

            ExecuteCommand(query);
        }
        static public void Delete(SqlParameter[] parameters, string tableName)
        {
            string query = "Delete From " + tableName + " WHERE  ";

           

                foreach (var parameter in parameters)
                {
                    if (parameter.SqlDbType == SqlDbType.Int)
                        query += parameter.ParameterName + "=" + parameter.Value + " AND ";
                    else
                        query += parameter.ParameterName + "='" + parameter.Value + "' AND ";
                }
            
            query = query.Substring(0, query.Length - 4);

            ExecuteCommand(query);
        }
        static public void Update(SqlParameter[] parameters, string tableName, SqlParameter[] whereParameters)
        {
            string query = "Update  " + tableName + " Set ";

            foreach (var parameter in parameters)
            {
                if (parameter.SqlDbType == SqlDbType.Int)
                    query += parameter.ParameterName + "=" + parameter.Value + ",";
                else
                    query += parameter.ParameterName + "='" + parameter.Value + "',";

            }
            query = query.Substring(0, query.Length - 1);
            //çok yanlış yapmıssın tasarımı sankih ayır vhayır uğraşmıyım diye oyle yaptım inner joıinleş şimdi sıkıntı olcak ama dur bi dkya yine orda var urunid

            if (whereParameters.Length > 0)
            {
                query += " where ";

                foreach (var parameter in whereParameters)
                {
                    if (parameter.SqlDbType == SqlDbType.Int)
                        query += parameter.ParameterName + "=" + parameter.Value + " AND";
                    else
                        query += parameter.ParameterName + "='" + parameter.Value + "' AND";
                }
            }
            query = query.Substring(0, query.Length - 3);
            ExecuteCommand(query);
        }

    }
}
