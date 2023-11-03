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
        bool goRight, colision_muro = false;
        bool isGameOver;
        private DispatcherTimer timer;
        private Canvas canvas;
        private TranslateTransform transform = new TranslateTransform();
        private TranslateTransform transform2 = new TranslateTransform();
        private double pos;
        double altoPantalla;
        double anchoPantalla, velocidad = 2;
        private
            double posX = 0;
        private double Hspeed = 0;
        private double Vspeed = 0;    
        Rectangle s;
        int score;
        int ballX;
        int ballY;
        int playerSpeed;
        Rectangle[] rect = new Rectangle[8];

        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Normal;
            setupGame();
        }



        private void setupGame()
        {
            score = 0;
            ballX = 0;
            ballY = 0;
            playerSpeed = 0;

            anchoPantalla = SystemParameters.PrimaryScreenWidth;
            altoPantalla = SystemParameters.PrimaryScreenHeight;

            // Establecer el tamaño del Canvas basado en el ancho y alto de la pantalla
            CanvasJuego.Width = anchoPantalla;
            CanvasJuego.Height = altoPantalla;

            pos = Canvas.GetTop(ball);

            for (int i = 0; i < rect.Length; i++)
            {
                rect[i] = (Rectangle)FindName("bloque" + (i + 1));
                rect[i].Fill = new SolidColorBrush(Colors.Red);

            }

            // Inicializa el temporizador con un intervalo de 1 segundo (1000 milisegundos)
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1); // Intervalo para aproximadamente 60 FPS
            timer.Tick += Timer_Tick;

            // Inicia el temporizador
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double nuevaPosicion = 0, limiteIzquierdo = 0, limiteDerecho = 0;
            bool eslimiteDerecho = false,eslimiteIzquierdo = false;
            label.Content = goLeft;
            Canvas.SetTop(ball, pos += velocidad);
            s = (Rectangle)FindName("plataforma");
            transform = s.RenderTransform as TranslateTransform;
            transform2 = ball.RenderTransform as TranslateTransform;
            Canvas.SetLeft(s, anchoPantalla / 2 - s.Width / 2);
            Canvas.SetTop(s, altoPantalla / 2 - s.Height / 2);
            limiteIzquierdo = 0;
            limiteDerecho = CanvasJuego.ActualWidth - s.Width;
            Canvas.SetLeft(s, posX);
            double posicionX = Canvas.GetLeft(s);
            double posicionPlataforma = Canvas.GetLeft(s); // Obtener la posición actual de la plataforma
            Canvas.GetTop(s);
            txtScore.Content = colision_muro;
            
            if (Canvas.GetTop(ball)+ball.Height>= (Math.Abs(CanvasJuego.Height-ball.Height)))
            {
                
                if (!colision_muro)
                {
                    velocidad = -velocidad;
                    colision_muro = true;
                }
            }
            

            if (Canvas.GetLeft(s) < limiteIzquierdo)
            {
                eslimiteIzquierdo = true;
                // La plataforma ha excedido el límite izquierdo
                // Realiza las acciones necesarias, como ajustar la posición
               

            }
            else
            {
                eslimiteIzquierdo = false;
            }

            if (Canvas.GetLeft(s) > limiteDerecho)
            {
                eslimiteDerecho = true;
                // La plataforma ha excedido el límite derecho
                // Realiza las acciones necesarias, como ajustar la posición
              
            }
            else
            {
                eslimiteDerecho = false;
            }


            // Actualizar la posición de la plataforma
            if (goLeft && !eslimiteIzquierdo)
            {

                posX -= 4;
            }
            if (goRight && !eslimiteDerecho)
            {
                posX += 4;
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
            }
            else if (e.Key == Key.Right) { goRight = false; }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CanvasJuego.Width = e.NewSize.Width;
            CanvasJuego.Height = e.NewSize.Height;

        }
    }
}
