using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using DbAccess.BLL;

namespace ADO.NET_hw_lesson2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // для события изменения текста в поле tbConnectionString
        private static int countEvent = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            FillCbSelectConnStr();
        }

        private void FillCbSelectConnStr()
        {
            cbSelectConnStr.ItemsSource = Controller.ItemsSourceComboBox().Select(keyVal => keyVal.Key).ToList();
        }

        private void BtnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            //string connStrName = cbSelectConnStr.SelectedValue.ToString();
            string connStr = tbConnectionString.Text;

            if (string.IsNullOrWhiteSpace(connStr))
            {
                message = "Строка соединения не введена";
            }
            else
            {
                Controller.TestDbconnection(connStr, out message);
            }

            MessageBox.Show(message);
        }

        private void CbSelectConnStr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = "";
            Controller.ItemsSourceComboBox().TryGetValue(cbSelectConnStr.SelectedValue.ToString(), out value);
            tbConnectionString.Text = value;
        }

        private void TbConnectionString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (countEvent != 0)
                btnSaveChangesConnStr.IsEnabled = true;
            countEvent++;
        }

        private void BtnSaveChangesConnStr_Click(object sender, RoutedEventArgs e)
        {
            Controller.SaveChangesAppConfig(cbSelectConnStr.SelectedItem.ToString(), tbConnectionString.Text);
            MessageBox.Show("Сохранено");
        }

        private void btnSendQuery_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            string queryExpression = tbDbQuery.Text;

            Controller.MakeRequest(tbConnectionString.Text, queryExpression, out message);
        }

        private void ViewTable()
        {
            
        }
    }
}
