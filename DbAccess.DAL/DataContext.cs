using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace DbAccess.DAL
{
    public static class DataContext
    {
        private static DbConnection GetConnection(string connStr, out string message)
        {
            DbConnection conn = null;
            message = "";

            try
            {
                if (connStr.Contains("Provider=sqloledb"))
                {
                    conn = (OleDbConnection)conn;
                    conn = new OleDbConnection(connStr);
                    return conn;
                }

                else if (connStr.Contains("SQLEXPRESS"))
                {
                    conn = (SqlConnection)conn;
                    conn = new SqlConnection(connStr);
                    return conn;
                }

                else
                {
                    message = "Строки соединения не найдено";
                    return conn;
                }
            }
            catch (Exception e)
            {
                message = e.ToString();
                string[] msg = message.Split(new char[] { '\n' });
                message = msg[0];

                return conn;
            }


        }

        public static Boolean CheckConnection(string connStr, out string message)
        {
            DbConnection conn = GetConnection(connStr, out message);

            if (conn == null) return false;

            using (conn)
            {
                //conn.ConnectionString = connStr;
                try
                {
                    conn.Open();
                    StringBuilder builder = new StringBuilder();

                    builder.Append(new string('=', 40) + "\n");
                    builder.Append("Подключение выполнено\n\n");
                    builder.Append("Свойства подключения: \n");
                    builder.Append(conn.ConnectionString + "\n\n");
                    builder.Append("База данных: " + conn.Database + "\n");
                    builder.Append("Сервер: " + conn.DataSource + "\n");
                    builder.Append("Версия сервера: " + conn.ServerVersion + "\n");
                    builder.Append("Состояние: " + conn.State + "\n");
                    if (conn is SqlConnection)
                        builder.Append("ID : " + ((SqlConnection)conn).ClientConnectionId + "\n");
                    builder.Append(new string('=', 40));

                    message = builder.ToString();
                    return true;
                }
                catch (Exception e)
                {
                    message = e.ToString();
                    string[] msg = message.Split(new char[] { '\n' });
                    message = msg[0];
                    return false;
                }
            }
        }

        //CREATE, INSERT, UPDATE, DELETE.
        public static void DbExecuteNonQuery(string connStr, string queryExpression, out string message)
        {
            message = "";

            DbConnection conn = GetConnection(connStr, out message);

            using (conn)
            {
                conn.Open();
                //message = ((SqlConnection)conn).ClientConnectionId.ToString();
                if (conn is OleDbConnection)
                {
                    var command = conn.CreateCommand();
                    command.CommandText = queryExpression;

                    try
                    {
                        int number = command.ExecuteNonQuery();
                        message = $"Запрос в БД {conn.Database} выполнен, сделано записей {number}";
                    }
                    catch (Exception e)
                    {
                        message = e.ToString();
                        string[] msg = message.Split(new char[] { '\n' });
                        message = msg[0];
                    }
                }
            }

        }

        // SELECT.
        public static void DbExecuteReaderConsole(string connStr, string queryExpression, out string message)
        {
            message = "";

            DbConnection conn = GetConnection(connStr, out message);

            using (conn)
            {
                conn.Open();
                if (conn is OleDbConnection)
                {
                    var command = conn.CreateCommand();
                    command.CommandText = queryExpression;

                    try
                    {
                        DbDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object fieldValue = reader.GetName(i);
                                Console.Write(fieldValue + "\t");
                            }
                            Console.WriteLine();

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    object fieldValue = reader.GetValue(i);
                                    Console.Write(fieldValue + "\t\t");
                                }
                                Console.WriteLine();
                            }
                        }
                        //message = $"Изменено {reader.RecordsAffected} записей в БД {conn.Database}";

                    }
                    catch (Exception e)
                    {
                        message = e.ToString();
                        string[] msg = message.Split(new char[] { '\n' });
                        message = msg[0];
                    }
                }
            }

        }

        public static DataViewManager DbExecuteReader(string connStr, string queryExpression, out string message)
        {
            message = "";
            DbConnection conn = GetConnection(connStr, out message);
            DataSet dataSet = new DataSet();

            try
            {
                if (conn is OleDbConnection)
                {
                    OleDbCommand command = new OleDbCommand(queryExpression);
                    command.Connection = (OleDbConnection)conn;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataSet);
                }

                if (conn is SqlConnection)
                {
                    SqlCommand command = new SqlCommand(queryExpression);
                    command.Connection = (SqlConnection)conn;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataSet);
                }
            }
            catch (Exception e)
            {
                string tmpMsg = e.ToString();
                string[] msg = tmpMsg.Split(new char[] { '\n' });
                message += msg[0];
            }
    

            DataViewManager dataView = new DataViewManager(dataSet);

            return dataView;
        }

    }
}
