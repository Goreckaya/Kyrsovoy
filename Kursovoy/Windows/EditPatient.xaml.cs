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

            int GrajdanstvoID = stu.ID_Grajdanstvo;
            var GrajdanstvoList = helper.GetContext().Grajdanstvo;
            foreach (var intem in GrajdanstvoList)
            {
                Grajdanstvo.Items.Add(intem.Name);
            }
            var GrajdanstvoName = helper.GetContext().Grajdanstvo.Where(p => p.ID_Grajdanstvo == GrajdanstvoID).FirstOrDefault();
            Grajdanstvo.Text = GrajdanstvoName.Name;
            // для того чтобы присвоить текст комбобоксу из айди гражданство.

            int StatusID = stu.ID_Status;
            var StatusList = helper.GetContext().Status;
            foreach (var intem in StatusList)
            {
                Status.Items.Add(intem.Name);
            }
            var StatusName = helper.GetContext().Status.Where(p => p.ID_Status == StatusID).FirstOrDefault();
            Status.Text = StatusName.Name;
            // для того чтобы присвоить текст комбобоксу из айди статус.

            FName.Text = st.FName;
            Name.Text = st.Name;
            LName.Text = st.LName;
            DateOfBirth.SelectedDate = st.DateOfBirth;
            PhoneNumber.Text = st.PhoneNumber;
            Address.Text = st.Address;
            Snils.Text = st.Snils;
            Pasport.Text = st.Pasport;
            Polis.Text = st.Polis;
                    


        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (Gender.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите пол пациента!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (PhoneNumber.Text.Length <= 5)
                {
                    MessageBox.Show("Укажите номер телефона!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Snils.Text.Length <= 5)
                {
                    MessageBox.Show("Укажите  номер снилс!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (FName.Text.Length <= 0)
                {
                    MessageBox.Show("Укажите фамилию", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (LName.Text.Length <= 0)
                {
                    MessageBox.Show("Укажите отчество", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Name.Text.Length <= 0)
                {
                    MessageBox.Show("Укажите имя", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }



                if (PhoneNumber.Text.Length > 11)
                {
                    MessageBox.Show("Укажите достоверный номер телефона!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Snils.Text.Length >= 15)
                {
                    MessageBox.Show("Укажите достоверный номер снилс!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (FName.Text.Length >= 50)
                {
                    MessageBox.Show("Фамилия не может быть такой длиной", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (LName.Text.Length >= 50)
                {
                    MessageBox.Show("Отчество не может быть таким длинным", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Name.Text.Length >= 50)
                {
                    MessageBox.Show("Имя не может быть таким длинным", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                st.FName = FName.Text;
                st.Name = Name.Text;
                st.LName = LName.Text; 
                st.DateOfBirth = (DateTime)DateOfBirth.SelectedDate;
                st.PhoneNumber = PhoneNumber.Text;
                st.ID_Gender = Gender.SelectedIndex + 1;
                st.ID_Grajdanstvo = Grajdanstvo.SelectedIndex + 1;
                st.ID_Status = Status.SelectedIndex + 1;
                st.Address = Address.Text;
                st.Snils = Snils.Text;
                st.Pasport = Pasport.Text;
                st.Polis = Polis.Text;
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
