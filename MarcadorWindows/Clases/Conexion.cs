using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarcadorWindows
{
     class Conexion
    {
        MySqlConnection conex = new MySqlConnection();

        static String servidor = "localhost";
        static String bd = "marcador";
        static String user = "root";
        static String pass = "root";
        static String port = "3306";

        String cadenaConexion = "server=" + servidor + ";" + "port=" + port + ";" + "user id=" + user + ";" + "password=" + pass + ";" + "database=" + bd + ";";

        public MySqlConnection establecerConexion()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                MessageBox.Show("Conectado a la base de datos");

            }catch(MySqlException e)
            {
                MessageBox.Show("Error al conectar: "+e.ToString());
            }
            return conex;
        }
        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, conex);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
