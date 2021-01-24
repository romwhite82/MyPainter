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

namespace MyPainter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        List<Point> pnt = new List<Point>();
        List<Line> lns = new List<Line>();
        List<Ellipse> elps = new List<Ellipse>();
        List<UIElement> myUi = new List<UIElement>();
        Line tempLine = new Line();
        
        bool isPainting;
        

        private Ellipse myEllipse (double X, double Y)
        {
            Ellipse elps = new Ellipse();
            elps.Width = elSize.Value;
            elps.Height = elSize.Value;
            elps.StrokeThickness = 2;
           
            switch (comboColor.SelectedItem.ToString())
            {
                case "Чёрный": elps.Stroke = Brushes.Black;
                    break;
                case "Красный": elps.Stroke = Brushes.Red;
                    break;
                case "Голубой": elps.Stroke = Brushes.Blue;
                    break;
                case "Зелёный":  elps.Stroke = Brushes.Green;
                    break;
                case "Оранжевый": elps.Stroke = Brushes.Orange;
                    break;
                case "Жёлтый": elps.Stroke = Brushes.Yellow;
                    break;
                default:
                    elps.Stroke = Brushes.Black;
                    break;
            }
            elps.Margin = new Thickness(X - 2, Y - 2, 0, 0);
            elps.Tag = "MyEllipse";
            return elps;
        }

        private void myColor()
        {
            comboColor.Items.Add("Черный");
            comboColor.Items.Add("Красный");
            comboColor.Items.Add("Голубой");
            comboColor.Items.Add("Зелёный");
            comboColor.Items.Add("Оранжевый");
            comboColor.Items.Add("Жёлтый");
        }

        public MainWindow()
        {
            InitializeComponent();
            myColor();
            comboColor.SelectedIndex = 0;
        }

        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isPainting = true;
        }

        private void Canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPainting = false;
        }

        private void Canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            Line l2 = new Line();
            

            if (isPainting == true)
            {
                var Position = Mouse.GetPosition(Canvas1);
                Canvas0.Children.Add(myEllipse(Position.X, Position.Y));
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
               
                Canvas1.Children.Clear();
                l2.Stroke = Brushes.Red;
                l2.StrokeThickness = 3;
                l2.X1 = pnt[pnt.Count - 1].X;
                l2.Y1 = pnt[pnt.Count - 1].Y;
                var Position = Mouse.GetPosition(Canvas1);
                l2.X2 = Position.X;
                l2.Y2 = Position.Y;
                l2.Tag = "MyLine";
                Canvas1.Children.Add(l2);
            }
  
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
            Canvas0.Children.Clear();
        }

        private void Canvas1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Position = Mouse.GetPosition(Canvas1);
            Point npnt = new Point(Position.X, Position.Y);
            lns.Clear();
            pnt.Clear();
            pnt.Add(npnt);

            foreach (Line l in Canvas1.Children)
            {
                lns.Add(l);
            }
            Canvas1.Children.Clear();

            foreach (Line l in lns) Canvas0.Children.Add(l);
            
        }

        private void Canvas1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var Position = Mouse.GetPosition(Canvas1);
            Point npnt1 = new Point(Position.X, Position.Y);
            pnt.Add(npnt1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement myElements in Canvas0.Children)
            {
                myUi.Add(myElements);
            }
        }
    }
}
