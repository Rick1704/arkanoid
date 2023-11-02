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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        bool goLeft;
        bool goRight;
        bool isGameOver;
        private DispatcherTimer timer;
        private Canvas canvas;
        private TranslateTransform transform = new TranslateTransform();


        int score;
        int ballX;
        int ballY;
        int playerSpeed;
        Rectangle[] rect = new Rectangle[8];

        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();

            setupGame();
        }



        private void setupGame()
        {
            score = 0;
            ballX = 0;
            ballY = 0;
            playerSpeed = 0;

            canvas = new Canvas();
            Canvas.SetTop(gridJuego, gridJuego.Height);
            Canvas.SetRight(gridJuego,gridJuego.Width);
      
            for (int i = 0; i < rect.Length; i++)
            {
                rect[i] = (Rectangle)FindName("bloque" + (i + 1));
                rect[i].Fill = new SolidColorBrush(Colors.Red);

            }

            // Inicializa el temporizador con un intervalo de 1 segundo (1000 milisegundos)
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0);

            // Define un controlador de eventos para el evento Tick del temporizador
            timer.Tick += Timer_Tick;

            // Inicia el temporizador
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double nuevaPosicion = 0, limiteIzquierdo = 0, limiteDerecho = 0;
            
            label.Content = goLeft;
            Rectangle s = (Rectangle)FindName("plataforma");
            transform = s.RenderTransform as TranslateTransform;
            limiteIzquierdo = 0;
            limiteDerecho = this.ActualWidth - s.Width;
            double posicionPlataforma = Canvas.GetLeft(s); // Obtener la posición actual de la plataforma
            txtScore.Content = " ";

            if (posicionPlataforma < limiteIzquierdo)
            {

                // La plataforma ha excedido el límite izquierdo
                // Realiza las acciones necesarias, como ajustar la posición
                txtScore.Content = " aaaaaaaa";

            }
            else if (posicionPlataforma > limiteDerecho)
            {
                // La plataforma ha excedido el límite derecho
                // Realiza las acciones necesarias, como ajustar la posición
                txtScore.Content = " bbbbbbbbbb";
            }


            // Actualizar la posición de la plataforma
            if (goLeft)
            {
                
                
                transform.X -= 0.2;
            }
            if (goRight)
            {
                
                transform.X += 0.2;
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            else if (e.Key == Key.Right) { goRight = true; }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
                Console.WriteLine(goLeft);
            }
            else if (e.Key == Key.Right) { goRight = false; }
        }
    }
}
