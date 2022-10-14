using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcadorWindows
{
    public class PartidoEventArgs:EventArgs
    {
        public String Ganador { get; set; }
    }
    internal class Marcador
    {
        private int[][] marcadorSets;
        private int[] puntos;
        private string[] valorPuntos = { "00", "15", "30", "40", "AV" };
        private int SetActivo;
        private int SetsLocales, SetsVisitantes;
        private bool partidoFinalizado;
        private int numSets;
        private const int local = 0;
        private const int visitante = 1;
        private bool tieBreak;

        //Delegado para el evento de finalización del partido
        
        //Paso 1: crear delegado
        public delegate void GestorEventoPartidoFinalizado(object fuente, PartidoEventArgs args);
        
        //Paso 2: Crear evento basado en el delegado
        public event GestorEventoPartidoFinalizado PartidoFinalizado;
        //public event EventHandler<PartidoEventArgs> PartidoFinalizado;

        //Paso 3: Iniciar el evento
        protected virtual void OnPartidoFinalizado(string ganador)
        {
            if(PartidoFinalizado!=null)
            {
                PartidoFinalizado(this, new PartidoEventArgs() { Ganador = ganador });
            }
        }

        /// <summary>
        /// Constructor por defecto del Marcador
        /// </summary>
        public Marcador()
        {
            int i, j;
            marcadorSets = new int[2][];
            marcadorSets[local]=new int[3];
            marcadorSets[visitante] = new int[3];
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    marcadorSets[i][j] = 0;
                }
            }
            puntos = new int[2];
            JugadorLocal = "Player 1";
            JugadorVisitante = "Player 2";
            numSets = 3;
            SetActivo = 0;
            partidoFinalizado = false;
            SetsLocales = 0;
            SetsVisitantes = 0;
            tieBreak = false;
        }


        /// <summary>
        /// Constructor parametrizado del Marcador
        /// </summary>
        /// <param name="jugadorLocal"> Nombre del jugador Local</param>
        /// <param name="jugadorVisitante">Nombre del jugador Visitante</param>
        /// <param name="numeroSets">Numero de sets del partido</param>
        public Marcador(string jugadorLocal, string jugadorVisitante, int numeroSets)
        {
            int i, j;
            marcadorSets = new int[2][];
            marcadorSets[local] = new int[3];
            marcadorSets[visitante] = new int[3];
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    marcadorSets[i][j] = 0;
                }
            }
            puntos = new int[2];
            JugadorLocal = jugadorLocal;
            JugadorVisitante = jugadorVisitante;
            numSets = numeroSets;
            SetActivo = 0;
            partidoFinalizado = false;
            SetsLocales = 0;
            SetsVisitantes = 0;
            tieBreak=false;
        }


        //Propiedades
        public string JugadorLocal { get; set; }
        public string JugadorVisitante { get; set; }

        public string PuntosLocal
        {
            get
            {
                if (tieBreak)
                {
                    return(puntos[local].ToString());
                }
                else
                {
                    return valorPuntos[puntos[local]];
                }
                
            }
        }

        public string PuntosVisitante
        {
            get
            {
                if (tieBreak)
                {
                    return puntos[visitante].ToString();
                }
                else
                {
                    return valorPuntos[puntos[visitante]];
                }
            }
        }

        public int[] MarcadorLocal
        {
            get
            {
                return marcadorSets[local];
            }
        }

        public int[] MarcadorVisitante
        {
            get
            {
                return marcadorSets[visitante];
            }
        }

        /// <summary>
        /// Añade un punto al jugador local
        /// </summary>
        public void PuntoLocal()
        {
            puntos[local]++;
            if(!partidoFinalizado)
            {
                if (tieBreak)
                {
                    /*Trabajamos por valores, el juego se gana cuando un jugador llega a 7 o más y saca dos de ventaja al otro jugador*/
                    if ((puntos[local] >= 7) && ((puntos[local] - puntos[visitante]) >= 2))
                    {
                        puntos[local] = 0;
                        puntos[visitante] = 0;
                        JuegoLocal();
                        tieBreak = false;
                    }
                }
                else
                {
                    /* Trabajamos con indices que van de 0 a 4, en el vector valorPuntos tenemos las conversiones de esos puntos a formato tenis o padel.
                     * de esta forma el indice 0 se corresponde con el valor 00, el 1 con 15, 2 con 30 y asi sucesivamente.
                     * Comprobamos que cuando lleguemos a 4 sacamos 2 de ventaja al otro jugador para conseguir el juego, si no sacamos 2 de ventaja estaremos en una situacion de ventaja
                     * por lo que si el otro jugador estaba en ventaja y conseguimos punto estaremos en iguales, si estamos en iguales nos pondremos con ventaja
                     * y si estabamos en ventaja, nos separaremos en 2 por lo que será nuestro el juego*/
                    if ((puntos[local] >= 4) && ((puntos[local] - puntos[visitante]) >= 2))
                    {
                        puntos[local] = 0;
                        puntos[visitante] = 0;
                        JuegoLocal();
                    }
                    else if ((puntos[local] >= 4) && (puntos[visitante] >= 4))
                    {
                        puntos[visitante]--;
                        puntos[local]--;
                    }
                    //Debug.WriteLine("local: {0}", valorPuntos[puntos[local]]);
                    //Debug.WriteLine("visitante: {0}", valorPuntos[puntos[visitante]]);
                }
            }
            


        }
        /// <summary>
        /// Añade un punto al jugador visitante
        /// </summary>
        public void PuntoVisitante()
        {
            puntos[visitante]++;
            if(!partidoFinalizado)
            {
                if (tieBreak)
                {
                    /*Trabajamos por valores, el juego se gana cuando un jugador llega a 7 o más y saca dos de ventaja al otro jugador*/
                    if ((puntos[visitante] >= 7) && ((puntos[visitante] - puntos[local]) >= 2))
                    {
                        puntos[local] = 0;
                        puntos[visitante] = 0;
                        JuegoVisitante();
                        tieBreak = false;
                    }
                }
                else
                {
                    /* Trabajamos con indices que van de 0 a 4, en el vector valorPuntos tenemos las conversiones de esos puntos a formato tenis o padel.
                     * de esta forma el indice 0 se corresponde con el valor 00, el 1 con 15, 2 con 30 y asi sucesivamente.
                     * Comprobamos que cuando lleguemos a 4 sacamos 2 de ventaja al otro jugador para conseguir el juego, si no sacamos 2 de ventaja estaremos en una situacion de ventaja
                     * por lo que si el otro jugador estaba en ventaja y conseguimos punto estaremos en iguales, si estamos en iguales nos pondremos con ventaja
                     * y si estabamos en ventaja, nos separaremos en 2 por lo que será nuestro el juego*/
                    if ((puntos[visitante] >= 4) && ((puntos[visitante] - puntos[local]) >= 2))
                    {
                        puntos[local] = 0;
                        puntos[visitante] = 0;
                        JuegoVisitante();

                    }
                    else if ((puntos[visitante] >= 4) && (puntos[local] >= 4))
                    {
                        puntos[visitante]--;
                        puntos[local]--;
                    }
                    //Debug.WriteLine("local", valorPuntos[puntos[local]]);
                    //Debug.WriteLine("visitante", valorPuntos[puntos[visitante]]);
                }
            }
            

        }

        /// <summary>
        /// Añade un juego al jugador Local
        /// </summary>
        private void JuegoLocal()
        {
            marcadorSets[local][SetActivo]++;
            if (tieBreak)
            {
                SetLocal();
            }
            else
            {
                if ((marcadorSets[local][SetActivo] >= 6) && ((marcadorSets[local][SetActivo] - marcadorSets[visitante][SetActivo]) >= 2))
                {
                    SetLocal();

                }
            }
            
            //Debug.WriteLine("Set local {0}", marcadorSets[local, SetActivo]);
            //Debug.WriteLine("Set visitante {0}", marcadorSets[visitante, SetActivo]);
            
            //Si en el set vamos 6-6 pasamos a situacion de tieBreak
            if ((marcadorSets[local][SetActivo] == 6)&& (marcadorSets[visitante][SetActivo] == 6))
            {
                tieBreak = true;
                puntos[local] = 0;
                puntos[visitante] = 0;
            }
        }
        /// <summary>
        /// Añade un juego al jugador visitante
        /// </summary>
        private void JuegoVisitante()
        {
            marcadorSets[visitante][SetActivo]++;
            if (tieBreak)
            {
                SetVisitante();
            }
            else
            {
                if ((marcadorSets[visitante][SetActivo] >= 6) && ((marcadorSets[visitante][SetActivo] - marcadorSets[local][SetActivo]) >= 2))
                {
                    SetVisitante();
                }
            }
            //Debug.WriteLine("Set local  {0}", marcadorSets[local, SetActivo]);
            //Debug.WriteLine("Set visitante  {0}", marcadorSets[visitante, SetActivo]);

            //Si en el set vamos 6-6 pasamos a situacion de tieBreak
            if ((marcadorSets[local][SetActivo] == 6) && (marcadorSets[visitante][SetActivo] == 6))
            {
                tieBreak = true;
                puntos[local] = 0;
                puntos[visitante] = 0;
            }
        }
        /// <summary>
        /// Añade un set al jugador Local
        /// </summary>
        private void SetLocal()
        {
            SetsLocales++;
            if (SetsLocales > (numSets / 2))
            {
                partidoFinalizado = true;
                OnPartidoFinalizado(JugadorLocal);
            }
            else
            {
                SetActivo++;
            }
        }
        /// <summary>
        /// Añade un set al jugador visitante
        /// </summary>
        private void SetVisitante()
        {
            SetsVisitantes++;
            if (SetsVisitantes > (numSets / 2))
            {
                partidoFinalizado = true;
                OnPartidoFinalizado(JugadorVisitante);
            }
            else
            {
                SetActivo++;
            }
        }




    }
}
