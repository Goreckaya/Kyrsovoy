using Kursovoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Kursovoy.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Window
    {
        Patient st;
        int DepartmentID;
        int MedicalOfficerID;
        public NewAppointment(Patient stu)
        {
            InitializeComponent();           
            st = stu;

            var DepartmentList = helper.GetContext().Department;
            foreach (var intem in DepartmentList)
            {
                Department.Items.Add(intem.Name);
            }



            FName.Text = st.FName;
            Name.Text = st.Name;
            LName.Text = st.LName;




        }
        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MedicalOfficer.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите врача!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }              
                
               
                Appointment std = new Appointment
                {
                    ID_Patient = st.ID_Patient,                    
                    ID_Department = DepartmentID,
                    Date = (DateTime)Date.SelectedDate,
                    ID_MedicalOfficer = MedicalOfficerID


                };
                helper.GetContext().Appointment.Add(std);
                helper.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Выберите дату!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




        }
        public string[] GenderMass { get; set; } =
       {
            "Мужчина",
            "Женщина"
        };

        private void Department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
            
            DepartmentID = Department.SelectedIndex + 1;
            var MedicalOfficeList = helper.GetContext().MedicalOfficer.Where(P => P.ID_Department == DepartmentID);
            foreach (var intem in MedicalOfficeList)
            {

                string Name = intem.FName + " " + intem.Name + " " + intem.LName;
                MedicalOfficer.Items.Add(Name);
                MedicalOfficerID = intem.ID_MedicalOfficer;

            }

        }

        private void MedicalOfficer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var CabinetList = helper.GetContext().MedicalOfficer.Where(P => P.ID_MedicalOfficer == MedicalOfficerID).FirstOrDefault();
            var idCabinet = CabinetList.ID_Cabinet;
            var CabinetName = helper.GetContext().Cabinet.Where(P => P.ID_Сabinet == idCabinet).FirstOrDefault();
            Cabinet.Items.Add(CabinetName.Nomber);
        }
    }
}

