using Kursovoy.Model;
using Kursovoy.Pages;
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

namespace Kursovoy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class helper
    {
        public static HospitalKPEntities3 ent;
        internal static bool flag = false;
        internal static int prioritet = 0;

        public static HospitalKPEntities3 GetContext()
        {
            if (ent == null)
            {
                ent = new HospitalKPEntities3();
            }
            return ent;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {

            string password = Password.Password.ToString();
            string login = Login.Text.ToString();
            var EnterAcc = helper.GetContext().Vhod.Where(p => p.Password == password && p.Login == login).FirstOrDefault();
          

            if (EnterAcc != null)
            {
                
                if (EnterAcc.Role == 1)
                {
                    int id = EnterAcc.ID_Vhod;
                    DataView Form4 = new DataView(id);
                    Form4.Show();
                    this.Close();
                }    
               
               
                if (EnterAcc.Role == 2)
                {
                    int id = EnterAcc.ID_Vhod;
                    DataRegView Form5 = new DataRegView(id);
                    Form5.Show();
                    this.Close();
                }
                
                if (EnterAcc.Role == 3)
                {
                    int id = EnterAcc.ID_Vhod;
                    AdminView Form6 = new AdminView(id);
                    Form6.Show();
                    this.Close();
                }

            }            

            else
            {
                MessageBox.Show("Повторите вход ", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }
    }
}

