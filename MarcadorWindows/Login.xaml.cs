using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace MarcadorWindows
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Conexion c1 = new Conexion();
        public Login()
        {
            InitializeComponent();
            


        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            c1.establecerConexion();
            if (txtUser.Text != "" && txtPass.Text != "")
                {
                    
                    MySqlCommand cmd=new MySqlCommand ("select nombre,pass FROM usuarios WHERE nombre ='" + txtUser.Text + "' AND pass ='" + txtPass.Text + "'");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        MessageBox.Show("Successfully Sign In!");
                    }
                    else
                    {
                        MessageBox.Show("Username And Password Not Match!");
                    }
            }
            
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
