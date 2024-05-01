using Kursovoy.Model;
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

namespace Kursovoy.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditMedicalOfficer.xaml
    /// </summary>
    public partial class EditMedicalOfficer : Window
    {
        MedicalOfficer st;
        public EditMedicalOfficer(MedicalOfficer stu)
        {
            InitializeComponent();
            st = stu;

           /* int GenderID = stu.ID_Gender;
            var GenderList = helper.GetContext().Gender;
            foreach (var intem in GenderList)
            {
                Gender.Items.Add(intem.Name);
            }
            var GenderName = helper.GetContext().Gender.Where(p => p.ID_Gender == GenderID).FirstOrDefault();
            Gender.Text = GenderName.Name;*/
            // для того чтобы присвоить текст комбобоксу из айди гендера.

            int DepartmentID = stu.ID_Department;
            var DepartmentList = helper.GetContext().Department;
            foreach (var intem in DepartmentList)
            {
                Department.Items.Add(intem.Name);
            }
            var DepartmentName = helper.GetContext().Department.Where(p => p.ID_Department == DepartmentID).FirstOrDefault();
            Department.Text = DepartmentName.Name;
            // для того чтобы присвоить текст комбобоксу из айди специализации.

            int CabinetID = Convert.ToInt32(stu.ID_Cabinet);
            var CabinetList = helper.GetContext().Cabinet;
            foreach (var intem in CabinetList)
            {
                Cabinet.Items.Add(intem.Nomber);
            }
            var CabinettName = helper.GetContext().Cabinet.Where(p => p.ID_Сabinet == CabinetID).FirstOrDefault();
            Cabinet.Text = CabinettName.Nomber;
            // для того чтобы присвоить текст комбобоксу из айди кабинета. 

            Smena.ItemsSource = Smens;
            FName.Text = st.FName;
            Name.Text = st.Name;
            LName.Text = st.LName;
            DateOfBirth.SelectedDate = st.DateOfBirth;            
            Address.Text = st.Address;
            Diplom.Text = st.Diplom;
            Institute.Text = st.Institute;

            var SmenaName = helper.GetContext().MedicalOfficer.Where(p => p.ID_MedicalOfficer == stu.ID_MedicalOfficer).FirstOrDefault();
            Smena.Text = SmenaName.Smena;
        }
        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                st.FName = FName.Text;
                st.Name = Name.Text;
                st.LName = LName.Text;
                st.DateOfBirth = (DateTime)DateOfBirth.SelectedDate;
                st.Address = Address.Text;
               // st.ID_Gender = Gender.SelectedIndex + 1;
                st.ID_Department = Department.SelectedIndex + 1;
                st.ID_Cabinet = Cabinet.SelectedIndex + 1;
                st.Smena = Smena.SelectedItem.ToString();
                st.Diplom = Diplom.Text;
                st.Institute = Institute.Text;

                helper.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно изменены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        public string[] Smens { get; set; } =
        {
            "1",
            "2"
        };
    }
}

