using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DbAccess.BLL;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "";

            #region Test Connection to DB
            /*
            Controller.TestDbconnection("SqlOleDb", out message);
            Console.WriteLine(message);

            Controller.TestDbconnection("SqlClient", out message);
            Console.WriteLine(message);
            */
            #endregion Test Connection to DB

            #region Create Table in DB
            /*
            StringBuilder OleDbExpression = new StringBuilder();
            OleDbExpression.Append($"Create Table Model");
            OleDbExpression.Append("(");
            OleDbExpression.Append("ModelID int,");
            OleDbExpression.Append("Name nvarchar(100),");
            OleDbExpression.Append("ManufacturerID int,");
            OleDbExpression.Append("SMCSFamilyID int,");
            OleDbExpression.Append("Image nvarchar(100)");
            OleDbExpression.Append(")");

            Controller.Create("SqlOleDb", OleDbExpression.ToString(), out message);
            Console.WriteLine(message);
            */
            #endregion Create Table in DB

            #region Insert Table in DB

            StringBuilder OleDbExpression = new StringBuilder();
            OleDbExpression.Append($"INSERT INTO Model");

            OleDbExpression.Append("(");
            OleDbExpression.Append("ModelID,");
            OleDbExpression.Append("Name, ");
            OleDbExpression.Append("ManufacturerID,");
            OleDbExpression.Append("SMCSFamilyID,");
            OleDbExpression.Append("Image ");
            OleDbExpression.Append(") ");

            OleDbExpression.Append("VALUES ");

            OleDbExpression.Append("(");
            OleDbExpression.Append("1,");
            OleDbExpression.Append("'Samsung', ");
            OleDbExpression.Append("22, ");
            OleDbExpression.Append("33, ");
            OleDbExpression.Append("'Image' ");
            OleDbExpression.Append(") ");

            Controller.Create("SqlOleDb", OleDbExpression.ToString(), out message);
            Console.WriteLine(message);

            #endregion Create Table in DB


            #region ReadData Table in DB
            /*
            StringBuilder OleDbExpression = new StringBuilder();
            OleDbExpression.Append($"SELECT TOP 5 ");
            OleDbExpression.Append($"intEquipmentID, ");
            OleDbExpression.Append($"intGarageRoom, ");
            OleDbExpression.Append($"intManufacturerID, ");
            OleDbExpression.Append($"intModelID ");
            OleDbExpression.Append($"FROM newEquipment ");

            Controller.Read("SqlOleDb", OleDbExpression.ToString(), out message);
            Console.WriteLine(message);
            */
            #endregion ReadData Table in DB

            Console.ReadKey();
        }
    }
}
