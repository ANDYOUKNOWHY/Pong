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
using System.Windows.Threading;

namespace WpfApp8
{
    
    public partial class MainWindow : Window
    {
        DispatcherTimer casovacHry = new DispatcherTimer(); 
        List<Ellipse> odstranit = new List<Ellipse>();
       
        int spawning = 60; 
        int aktualni; 
        int topScore = 0; 
        int health = 350;
        int poziceX;
        int pozicey; 
        int score = 0; 
        double rust = 0.7; 
        Random pozicovani = new Random(); 
       
        
        
        Brush brush;
        public MainWindow()
        {
            InitializeComponent();
            
            casovacHry.Tick += Loop; 
            casovacHry.Interval = TimeSpan.FromMilliseconds(20);
            casovacHry.Start();
            aktualni = spawning; 
            
            
        }
        private void Loop(object sender, EventArgs e)
        {
           
            txtScore.Content = "Score: " + score;
            txtLastScore.Content = "Last Score: " + topScore;
           
            aktualni -= 2;
            
            if (aktualni < 1)
            {
                
                aktualni = spawning;
                
                poziceX = pozicovani.Next(15, 700);
                pozicey = pozicovani.Next(50, 350);
                
                brush = new SolidColorBrush(Color.FromRgb((byte)pozicovani.Next(1, 255), (byte)pozicovani.Next(1, 255), (byte)pozicovani.Next(1, 255)));
               
                Ellipse circle = new Ellipse
                {
                    Tag = "circle",
                    Height = 10,
                    Width = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Fill = brush
                };
               
                Canvas.SetLeft(circle, poziceX);
                Canvas.SetTop(circle, pozicey);
               
                MyCanvas.Children.Add(circle);
            }
            
            foreach (var x in MyCanvas.Children.OfType<Ellipse>())
            {
                
                x.Height += rust; 
                x.Width += rust;
                x.RenderTransformOrigin = new Point(0.5, 0.5);
                if (x.Width > 70)
                {
                   
                    odstranit.Add(x);
                    health -= 15; 
                   
                }
            }
            
            if (health > 1)
            {
                
                healthBar.Width = health;
            }
            else
            {
                
                GameOverFunction();
            }
           
            foreach (Ellipse i in odstranit)
            {
                
                MyCanvas.Children.Remove(i);
            }
           
            if (score > 5)
            {
                
                spawning = 25;
            }
            
            if (score > 20)
            {
                
                spawning = 15;
                rust = 1.5;
            }
        }
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
           
            if (e.OriginalSource is Ellipse)
            {
               
                Ellipse circle = (Ellipse)e.OriginalSource;
                
                MyCanvas.Children.Remove(circle);
               
                score++;
                
                
            }
        }
        private void GameOverFunction()
        {
           
            casovacHry.Stop();
          
            MessageBox.Show("Game Over" + Environment.NewLine + "You Scored: " + score + Environment.NewLine + "Click Ok to play again!", "Moo Says: ");
           
            foreach (var y in MyCanvas.Children.OfType<Ellipse>())
            {
               
                odstranit.Add(y);
            }
           
            foreach (Ellipse i in odstranit)
            {
                MyCanvas.Children.Remove(i);
            }
        
            rust = .6;
            spawning = 60;
            topScore = score;
            score = 0;
            aktualni = 5;
            health = 350;
            odstranit.Clear();
            casovacHry.Start();
        }
    }
}

