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

namespace MarcadorWindows
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Marcador miMarcador=new Marcador("Jose","Pepe",3);
        private bool estadoEdicion;
        public MainWindow()
        {
            InitializeComponent();
            
            estadoEdicion =false;
            miMarcador.PartidoFinalizado += OnPartidoFinalizado;
        }

        /// <summary>
        /// Método encargado de Actualizar el marcador.
        /// </summary>
        private void ActualizaMarcador()
        {
            TextPointsPlayer1.Text = miMarcador.PuntosLocal;
            TextPointsPlayer2.Text = miMarcador.PuntosVisitante;
            TextSet1Player1.Text = miMarcador.MarcadorLocal[0].ToString();
            TextSet2Player1.Text = miMarcador.MarcadorLocal[1].ToString();
            TextSet3Player1.Text = miMarcador.MarcadorLocal[2].ToString();
            TextSet1Player2.Text = miMarcador.MarcadorVisitante[0].ToString();
            TextSet2Player2.Text = miMarcador.MarcadorVisitante[1].ToString();
            TextSet3Player2.Text = miMarcador.MarcadorVisitante[2].ToString();
        }

        /// <summary>
        /// Delegado encargado de dar un punto local
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            miMarcador.PuntoLocal();
            ActualizaMarcador();
        }


        /// <summary>
        /// Delegado encargado de dar un punto visitante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            miMarcador.PuntoVisitante();
            ActualizaMarcador();
        }
        
        /// <summary>
        /// Delegado encargado de poner los campos de texto en modo edición
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            estadoEdicion = !estadoEdicion;
            if (estadoEdicion)
            {
                BtnEdit.Content = "Edición on";
                BtnEdit.Background = new SolidColorBrush(Colors.DarkGreen);

            }
            else
            {
                BtnEdit.Content = "Edición off";
                BtnEdit.Background = new SolidColorBrush(Colors.DarkRed);
            }
            TextSet1Player1.IsEnabled = estadoEdicion;
            TextSet2Player1.IsEnabled = estadoEdicion;
            TextSet3Player1.IsEnabled = estadoEdicion;
            TextSet1Player2.IsEnabled = estadoEdicion;
            TextSet2Player2.IsEnabled = estadoEdicion;
            TextSet3Player2.IsEnabled = estadoEdicion;
            TextPointsPlayer1.IsEnabled = estadoEdicion;
            TextPointsPlayer2.IsEnabled = estadoEdicion;

        }

        private void OnPartidoFinalizado(object fuente, PartidoEventArgs e)
        {
            BtnEdit.IsEnabled = false;
            BtnPoinsPlayer1.IsEnabled = false;
            BtnPoinsPlayer2.IsEnabled = false;
            MessageBox.Show("El ganador del partido es: "+ e.Ganador);
        }
    }
}
