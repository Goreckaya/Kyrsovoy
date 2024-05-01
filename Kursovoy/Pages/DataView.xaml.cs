
using Kursovoy.Model;
using Kursovoy.Windows;
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
    /// Логика взаимодействия для DataView.xaml
    /// </summary>
    public partial class DataView : Window
    {
        int idMedicalOfficer;
        public DataView(int idOfficer)
        {
            InitializeComponent();
            idMedicalOfficer = idOfficer;
            var user = helper.GetContext().MedicalOfficer.Where(p => p.ID_Vhod == idOfficer).FirstOrDefault();
            string smena = "";
            if (user.Smena == "1")
            {
                smena = "с 8 до 12";
            }
            if (user.Smena == "2")
            {
                smena = "с 13 до 17";
            }
            Name.Text = user.FName + " " + user.Name + " " + user.LName;
           

            Load();
        }
        public void Load()
        {
            var user = helper.GetContext().MedicalOfficer.Where(p => p.ID_Vhod == idMedicalOfficer).FirstOrDefault();
           
            AppointmentView.ItemsSource = helper.GetContext().Appointment.Where(p => p.ID_MedicalOfficer == user.ID_MedicalOfficer).ToList();
           
        }
    
        

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            Close();
        }
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentView.SelectedIndex == -1)
            {
                MessageBox.Show("Выделите запись для удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (AppointmentView.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        var idi = (AppointmentView.SelectedItem as Appointment).ID_Appointment;

                        var StaffDelete = helper.GetContext().Appointment.Where(p => p.ID_Appointment == idi).FirstOrDefault();
                        helper.GetContext().Appointment.Remove(StaffDelete);
                        helper.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();

                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void rebButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentView.SelectedIndex == -1)
            {
                MessageBox.Show("Выделите пациента для оформления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (AppointmentView.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите оформить пациента?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        var idi = AppointmentView.SelectedItem as Appointment;
                        PatienOformlenie frm = new PatienOformlenie(idi);
                        frm.ShowDialog();
                        Load();
                        


                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка оформления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
           
        }
    }
}
