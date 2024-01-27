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
    /// Логика взаимодействия для DataRegView.xaml
    /// </summary>
    public partial class DataRegView : Window
    {
        public DataRegView()
        {
            InitializeComponent();
        }
        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PatientPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            Close();
        }
    }
}

