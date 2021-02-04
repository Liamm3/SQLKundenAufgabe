using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
                if (!string.IsNullOrWhiteSpace(firstNameInput.Text) &&
                    !string.IsNullOrWhiteSpace(lastNameInput.Text) &&
                    !string.IsNullOrWhiteSpace(birthdateInput.Text))
                {
                    comd.ExecuteNonQuery();

                    firstNameInput.Text = "";
                    lastNameInput.Text = "";
                    birthdateInput.Text = "";
                }
            }
            catch (Exception _)
            {
                // Handle some error
            }

            conn.Close();
        }
    }
}
