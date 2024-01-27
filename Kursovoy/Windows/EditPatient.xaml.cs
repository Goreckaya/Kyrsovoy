using Kursovoy.Models;
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
    /// Логика взаимодействия для EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        Patient st;
        public EditPatient(Patient stu)
        {
            InitializeComponent();
           
            st = stu;

            int GenderID = stu.ID_Gender;
            var GenderList = helper.GetContext().Gender;
            foreach (var intem in GenderList)
            {
                Gender.Items.Add(intem.Name);
            }
            var GenderName = helper.GetContext().Gender.Where(p => p.ID_Gender == GenderID).FirstOrDefault();
            Gender.Text = GenderName.Name;
            // для того чтобы присвоить текст комбобоксу из айди гендера. 


            FName.Text = st.FName;
            Name.Text = st.Name;
            LName.Text = st.LName;
            DateOfBirth.SelectedDate = st.DateOfBirth;
            PhoneNumber.Text = st.PhoneNumber;
            Address.Text = st.Address;

        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                st.FName = FName.Text;
                st.Name = Name.Text;
                st.LName = LName.Text; 
                st.DateOfBirth = (DateTime)DateOfBirth.SelectedDate;
                st.PhoneNumber = PhoneNumber.Text;
                st.ID_Gender = Gender.SelectedIndex + 1;

                helper.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно изменены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
