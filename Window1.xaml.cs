using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data.SqlClient;
using mysnakegame;
using System.Timers;
//using System.Windows.Shapes.Polygon namespace;

namespace WpfApplication1
{
    using System.Windows.Threading;
    using mysnakegame;

    public partial class Window1 : Window
    {
        // This list describes the Bonus Red pieces of Food on the Canvas
        private List<Point> bonusPoints = new List<Point>();

        // This list describes the body of the snake on the Canvas
        private List<Point> snakePoints = new List<Point>();



        private Brush snakeColor = Brushes.Black;
        private enum SIZE
        {
            THIN = 40,
            NORMAL = 60,
            THICK = 200,
        };
       private enum MOVINGDIRECTION
      {
        UPWARDS = 8,//8
      DOWNWARDS =2,//2
            TOLEFT = 4,//3
            TORIGHT = 6,//6
        };
        //SpriteFont georgia;
        private TimeSpan FAST = new TimeSpan(1);
        private TimeSpan MODERATE = new TimeSpan(10000);
        private TimeSpan SLOW = new TimeSpan(50000);
        private TimeSpan DAMNSLOW = new TimeSpan(500000);



        private Point startingPoint = new Point(100, 100);
        private Point currentPosition = new Point();

        // Movement direction initialisation
        private int direction = 0;

        //Placeholder for the previous movement direction
         //The snake needs this to avoid its own body.
        private int previousDirection = 0;

       //change the size of the snake. 
         // Possible sizes are THIN, NORMAL and THICK
        private int headSize = (int)SIZE.THICK;



        private int length = 100;
        private int score = 0;
        private Random rnd = new Random();



        public Window1()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);

            /*  speed of the snake. 
             * FAST, MODERATE, SLOW and DAMNSLOW */
            timer.Interval = FAST;
            timer.Start();


            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startingPoint);
            currentPosition = startingPoint;

            // Instantiate Food Objects
            for (int n = 0; n < 10; n++)
            {
                paintBonus(n);
            }
        }



        private void paintSnake(Point currentposition)
        {

            // This method is used to paint a frame of the snake´s body
             //each time it is caled.


            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = snakeColor;
            newEllipse.Width = headSize;
            newEllipse.Height = headSize;

            Canvas.SetTop(newEllipse, currentposition.Y);
            Canvas.SetLeft(newEllipse, currentposition.X);

            int count = paintCanvas.Children.Count;
            paintCanvas.Children.Add(newEllipse);
            snakePoints.Add(currentposition);


            // Restrict the tail of the snake
            if (count > length)
            {
                paintCanvas.Children.RemoveAt(count - length + 9);
                snakePoints.RemoveAt(count - length);
            }
        }


        private void paintBonus(int index)
        {
            Point bonusPoint = new Point(rnd.Next(5, 1200), rnd.Next(5, 550));



            Ellipse newEllipse = new Ellipse();
            //EllipseGeometry newEllipseGeometry= new EllipseGeometry();
            //newEllipseGeometry.Fill = Brushes.Wheat;
            newEllipse.Fill = Brushes.Red;
            newEllipse.Fill = Brushes.Yellow;
            //EllipseGeometry newEllipseGeometry = new EllipseGeometry();
            
             //   = Brushes.Red;
           // newEllipseGeometry.Width = headSize;
            //newEllipseGeometry.Height = headSize;

            newEllipse.Width = headSize;
            newEllipse.Height = headSize;

            Canvas.SetTop(newEllipse, bonusPoint.Y);
            Canvas.SetLeft(newEllipse, bonusPoint.X);
           // Canvas.SetRight(newEllipse, bonusPoint.Z);
            paintCanvas.Children.Insert(index, newEllipse);
            bonusPoints.Insert(index, bonusPoint);

        }


        private void timer_Tick(object sender, EventArgs e)
        {
            
            //Expanding body of sanke to moving directions

            switch (direction)
            {
                case (int)MOVINGDIRECTION.DOWNWARDS:
                    currentPosition.Y += 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.UPWARDS:
                    currentPosition.Y -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TOLEFT:
                    currentPosition.X -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TORIGHT:
                    currentPosition.X += 1;
                    paintSnake(currentPosition);
                    break;
             //   case(int)
            }

            // Restrict to boundaries of the Canvas
            if ((currentPosition.X < 5) || (currentPosition.X > 1200) ||
                (currentPosition.Y < 5) || (currentPosition.Y > 550))
                GameOver();

            // Hitting a bonus Point causes the lengthen-Snake Effect
            int n = 0;
            foreach (Point point in bonusPoints)
            {

                if ((Math.Abs(point.X - currentPosition.X) < headSize) &&
                    (Math.Abs(point.Y - currentPosition.Y) < headSize))
                {
                    length += 10;
                    score += 10;

                    // In the case of food consumption, erase the food object
                    // from the list of bonuses as well as from the canvas
                    bonusPoints.RemoveAt(n);
                    paintCanvas.Children.RemoveAt(n);
                    paintBonus(n);
                    break;
                }
                n++;
            }

            // Restrict hits to body of Snake


            for (int q = 0; q < (snakePoints.Count - headSize * 2); q++)
            {
                Point point = new Point(snakePoints[q].X, snakePoints[q].Y);
                if ((Math.Abs(point.X - currentPosition.X) < (headSize)) &&
                     (Math.Abs(point.Y - currentPosition.Y) < (headSize)))
                {
                    GameOver();
                    break;
                }

            }

        }



        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {



            switch (e.Key)
            {
                case Key.Down:
                    if (previousDirection != (int)MOVINGDIRECTION.UPWARDS)
                        direction = (int)MOVINGDIRECTION.DOWNWARDS;
                    break;
                case Key.Up:
                    if (previousDirection != (int)MOVINGDIRECTION.DOWNWARDS)
                        direction = (int)MOVINGDIRECTION.UPWARDS;
                    break;
                case Key.Left:
                    if (previousDirection != (int)MOVINGDIRECTION.TORIGHT)
                        direction = (int)MOVINGDIRECTION.TOLEFT;
                    break;
                case Key.Right:
                    if (previousDirection != (int)MOVINGDIRECTION.TOLEFT)
                        direction = (int)MOVINGDIRECTION.TORIGHT;
                    break;

            }
            previousDirection = direction;

        }


        private void GameOver()
        {



            //scb.new SqlCommandBuilder(sda);
            //sda.update(db);
            MessageBox.Show("Snake died ...! Your score is " + score.ToString(), "Sorry" ,MessageBoxButton.OK, MessageBoxImage.Hand);
            //Window2 win = new Window2();
            //Window2 window2 = new Window2(TextBox1.Text);
            //Window2.ShowDialog();
            //   this.Close();

            //try

            //{
           // SqlConnection con = new SqlConnection("Data Source=PC4SKV/MSSQLSERVER2014;Initial Catalog=dbtables;Integrated Security=True");

           // con.Open();
           // SqlCommand cmd= new SqlCommand("UPDATE dbusers SET score = @score WHERE user= 'sravan'",con);


           // cmd.Parameters.AddWithValue("@score",score);
           // cmd.ExecuteNonQuery();

            //con.Close();

            //}
           // catch(Exception e)
            //{

           // }

            //Window2 sw = new Window2(score);

            //sw.Show();
            //this.Close();


        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //        {

        //          SqlConnection con = new SqlConnection("Data Source=PC4SKV/MSSQLSERVER2014;Initial Catalog=dbtables;Integrated Security=True");

        //        con.Open();
        //      SqlCommand cmd= new SqlCommand("UPDATE dbusers SET score=@score WHERE id=98",con);


        //    cmd.Parameters.AddWithValue("@score",score);
        //  cmd.ExecuteNonQuery();

        //con.Close();

        //  public string TextBox1 { get; set; }
    }
    //}

}