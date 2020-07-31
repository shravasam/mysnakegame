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
using WpfApplication1;
using System.Data.SqlClient;

namespace mysnakegame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("Data Source=PC4SKV/MSSQLSERVER2014;Initial Catalog=dbtables;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            dbtablesEntities dbe=new dbtablesEntities();
            dbusers db =new dbusers();
            db.user=t1.Text;
            db.nationality=t2.Text;
            db.date = t3.DisplayDateStart;
            dbe.dbusers.Add(db);
            dbe.SaveChanges();
            Window1 sw= new Window1();
            sw.Show();
            this.Close();





        }
    }
}

