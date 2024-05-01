using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using Kursovoy.Model;

namespace Kursovoy.Windows
{
    /// <summary>
    /// Логика взаимодействия для PatienOformlenie.xaml
    /// </summary>
    public partial class PatienOformlenie : Window
    {
        Appointment std;
        public PatienOformlenie(Appointment stu)
        {
            InitializeComponent();
            std = stu; 
            var Appointment = helper.GetContext().Appointment.Where(p => p.ID_Appointment == stu.ID_Appointment).FirstOrDefault();
            var Patient = helper.GetContext().Patient.Where(p => p.ID_Patient == Appointment.ID_Patient).FirstOrDefault();
            Name.Text = Patient.Name;
            FName.Text = Patient.FName;
            LName.Text = Patient.LName;  
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            var Appointment = helper.GetContext().Appointment.Where(p => p.ID_Appointment == std.ID_Appointment).FirstOrDefault();
            var Patient = helper.GetContext().Patient.Where(p => p.ID_Patient == Appointment.ID_Patient).FirstOrDefault();
            string Name = Patient.Name;
            string FName = Patient.FName;
            string LName = Patient.LName;
            string HEAL = Heal.Text.ToString();
            string Simptom = Simptoms.Text.ToString();
            DateTime Date = (DateTime)bol.SelectedDate.Value.Date;
            DateTime DateStart = std.Date;
            string snils =Patient.Snils.ToString();

            LechenieWindow lech = new LechenieWindow(Name, FName, LName, HEAL, Simptom, Date, DateStart, snils);
            
            helper.GetContext().Appointment.Remove(Appointment);
            helper.GetContext().SaveChanges();
            
            
            lech.ShowDialog();
            MessageBox.Show("Данные успешно сохранены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();



        }
    }
}
