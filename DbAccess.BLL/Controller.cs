using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DbAccess.DAL;
using System.Data;

namespace DbAccess.BLL
{
    public static class Controller
    {
        //static string connStr = "";
        //static string message = "";

        public static Dictionary<string, string> ItemsSourceComboBox()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            Dictionary<string, string> itemsDic = new Dictionary<string, string>();

            for (int i = 0; i < connectionStringsSection.ConnectionStrings.Count; i++)
            {
                itemsDic.Add(connectionStringsSection.ConnectionStrings[i].Name,
                      connectionStringsSection.ConnectionStrings[i].ConnectionString);
            }


            //Dictionary<string, string> itemsDic = new Dictionary<string, string>();
            //for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            //{
            //    itemsDic.Add(ConfigurationManager.ConnectionStrings[i].Name,
            //          ConfigurationManager.ConnectionStrings[i].ConnectionString);
            //}
            return itemsDic;
        }

        public static string GetConnectionStringRuntime()
        {
            // получение конфигурации connStr приложения (App.config) из exe runtime
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            return connectionStringsSection.ConnectionStrings.ToString();
        }

        public static void SaveChangesAppConfig(string connStrName, string changedConnStr)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[connStrName].ConnectionString = changedConnStr;
            config.Save();
        }

        public static Boolean TestDbconnection(string connStr, out string message)
        {
            message = "";
            Boolean test = false;

            test = DataContext.CheckConnection(connStr, out string msg);
            
            message = msg;
            return test;
        }

        public static DataViewManager MakeRequest(string connStr, string queryExpression, out string message)
        {
            message = "";

            if (queryExpression.Contains("create"))
                Create(connStr, queryExpression, out message);

            else if (queryExpression.Contains("insert"))
                Update(connStr, queryExpression, out message);

            else if (queryExpression.Contains("update"))
                Update(connStr, queryExpression, out message);

            else if (queryExpression.Contains("delete"))
                Delete(connStr, queryExpression, out message);

            else message = "данный запрос в не поддерживается";

            return Read(connStr, queryExpression, out message);
        }

        static void Create(string connStr, string queryExpression, out string message)
        {
            message = "";
            DataContext.DbExecuteNonQuery(connStr, queryExpression, out message);
        }

        static DataViewManager Read(string connStr, string queryExpression, out string message)
        {
            message = "";
            DataViewManager dataView = DataContext.DbExecuteReader(connStr, queryExpression, out message);
            return dataView;
        }

        static void Update(string connStr, string queryExpression, out string message)
        {
            message = "";
            DataContext.DbExecuteNonQuery(connStr, queryExpression, out message);
        }

        static void Delete(string connStr, string queryExpression, out string message)
        {
            message = "";
            DataContext.DbExecuteNonQuery(connStr, queryExpression, out message);
        }



    }
}
