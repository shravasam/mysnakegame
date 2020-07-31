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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace mysnakegame
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        //SqlDataAdapter sda;
        //SqlCommandBuilder scb;
       //dbusers=db;
        //SqlConnection con = new SqlConnection("Data Source=PC4SKV/MSSQLSERVER2014;Initial Catalog=dbtables;Integrated Security=True");

        public Window2(int score)
        {
            InitializeComponent();
            score.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
           
                   
            //con.SelectCommand=new SqlCommand("SELCET * from dbtables")
          // sda=new SqlDataAdapter("SELECT id,user,date,nationality,score FROM dbusers", con);
             //db= new dbusers();
            //sda.Fill(db);
            //DataGridView1.DataSource=db;  
        }
    }
}
