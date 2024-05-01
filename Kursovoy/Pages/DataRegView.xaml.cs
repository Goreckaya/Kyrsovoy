using Kursovoy.Model;
using Kursovoy.Windows;
using System;
using System.Collections;
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
    /// Логика взаимодействия для DataRegView.xaml
    /// </summary>
    public partial class DataRegView : Window
    {
        
        string fnd = "";
        public DataRegView(int id)
        {
            InitializeComponent();
            var user = helper.GetContext().MedicalOfficer.Where(p => p.ID_Vhod == id).FirstOrDefault();
            Name.Text = "Добро пожаловать: " + user.FName + " " + user.Name + " " + user.LName + " ";
            
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

       

       /* private void Officer_Click(object sender, RoutedEventArgs e)
        {
            
            FrmMain.Navigate(new OfficerViewPage());
            Name.Visibility = Visibility.Hidden;
           

        }*/

        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            FrmMain.Navigate(new PatientPage());
            Name.Visibility = Visibility.Hidden;
        }
    }
}

