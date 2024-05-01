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

namespace Kursovoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {

        string fnd = "";
        public AdminView(int id)
        {
            InitializeComponent();
            var user = helper.GetContext().MedicalOfficer.Where(p => p.ID_Vhod == id).FirstOrDefault();
            Name.Text = "Добро пожаловать: Селеванов Иван Иванович ";
                //"" + user.FName + " " + user.Name + " " + user.LName + " ";

        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            Close();
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            fnd = ((TextBox)sender).Text;

        }



        private void MedOfficer_Click(object sender, RoutedEventArgs e)
        {

            FrmMain.Navigate(new MedOfficerPage());
            Name.Visibility = Visibility.Hidden;


        }
    }
}
