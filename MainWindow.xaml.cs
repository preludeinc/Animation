using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Animation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// A simple animation of a bouncing ball moving around the Canvas. 
    /// The ball changes colours when it hits the Canvas' walls and slows slightly, to simulate the effect of gravity.
    /// This code was written around Halloween, hence the chosen colour palette :).
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DispatcherTimer dispatchTimer;
        Ellipse sphere;

        int diameter = 60;  // Diameter of Sphere
        int posX = 50;
        int posY = 50;
        int speedX = 10;
        int speedY = 10;

        int frameRate = 30;


        private void MyWindowLoaded(object sender, RoutedEventArgs e)
        {
            // A method is generated to set-up the Dispatch Timer, MyWindow_Loaded occurs once the Window is loaded
            DispatcherTimerSetup();

            sphere = new Ellipse();
            sphere.Width = diameter;
            sphere.Height = diameter;

            // Navy is applied to the sphere using the SolidColorBrush Class
            SolidColorBrush solidColourBrush = new SolidColorBrush
            {
                Color = Colors.Navy
            };
            sphere.Fill = solidColourBrush;

        }
        // This function is called once every 20 ms, as set by the frameRate var
        private void DispatcherTimerSetup()
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += DispatchTimerTick;
            dispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, frameRate);     // TimeSpan is set in hrs, mins, sec, ms (determined by frameRate var)
            dispatchTimer.Start();
        }

        private void DispatchTimerTick(object sender, EventArgs e)
        {
            // Before a new instance of the sphere is added, the Canvas is cleared
            myCanvas.Children.Clear();              

            // Sets the sphere's position to the top-left corner
            Canvas.SetTop(sphere, posY);
            Canvas.SetLeft(sphere, posX);

            posX += speedX;
            posY += speedY;
            var collSpeedSlow = 0.98;

            if (posX < 0 || posX + diameter > myCanvas.ActualWidth)         // posX causes the sphere to bounce off the LHS
            {                                                               // (posX > myCanvas.ActualWidth) reverses its direction
                speedX = (int)-(speedX * collSpeedSlow);                    // The ball's speed slows when it hits a wall

                SolidColorBrush solidColourBrush = new SolidColorBrush      // The colour of the ball changes based on its location
                {
                    Color = Colors.OrangeRed
                };
                sphere.Fill = solidColourBrush;
            }

            if (posY < 0 || posY + diameter > myCanvas.ActualHeight)        // posY causes the sphere to bounce off the RHS
            {                                                               // (posY > myCanvas.ActualHeight) reverses its direction
                speedY = speedY *= -1;

                SolidColorBrush solidColourBrush = new SolidColorBrush
                {
                    Color = Colors.Navy
                };
                sphere.Fill = solidColourBrush;
            }

            myCanvas.Children.Add(sphere);                                  // Adds the sphere to the Canvas each frame
        }

        // When the Window is resized the Canvas will follow and also be resized
        private void MyWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
           // Some pixels are deducted from the height of the Main Window to accomodate the Title Bar
            myCanvas.Height = myWindow.ActualHeight - 34;
            myCanvas.Width = myWindow.ActualWidth - 4;
        }
    }
}
