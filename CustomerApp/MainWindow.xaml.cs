using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace CustomerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection conn = new SqlConnection();

        public MainWindow()
        {
            InitializeComponent();

            conn.ConnectionString =
                "Data Source=W011076SYS\\SQLEXPRESS;" +
                "Initial Catalog=Aufgaben;" +
                "Integrated Security=SSPI;";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand comd = new SqlCommand("AddCustomer", conn);
            comd.CommandType = CommandType.StoredProcedure;
            comd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstNameInput.Text;
            comd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastNameInput.Text;
            comd.Parameters.Add("@Birthdate", SqlDbType.Date).Value = birthdateInput.Text;

            conn.Open();

            try
            {
                if (!String.IsNullOrWhiteSpace(firstNameInput.Text) &&
                    !String.IsNullOrWhiteSpace(lastNameInput.Text) &&
                    !String.IsNullOrWhiteSpace(birthdateInput.Text))
                {
                    comd.ExecuteNonQuery();

                    firstNameInput.Text = "";
                    lastNameInput.Text = "";
                    birthdateInput.Text = "";
                }
            }
            catch (Exception _)
            {
                // Handle Network error
            }

            conn.Close();
        }
    }
}
