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
    /// Логика взаимодействия для AddMedOfficer.xaml
    /// </summary>
    public partial class AddMedOfficer : Window
    {
        public AddMedOfficer()
        {
            InitializeComponent();
            Gender.ItemsSource = GenderMass;
            Department.ItemsSource = DepartmentMass;
            Cabinet.ItemsSource = CabinetMass;
        }
        private void AddMedOfficer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                /*if (Gender.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите пол!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (PhoneNumber.Text.Length <= 5)
                {
                    MessageBox.Show("Укажите номер телефона!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (StajRaboty.Text.Length <= 2)
                {
                    MessageBox.Show("Укажите  стаж работы!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Smena.Text.Length < 1)
                {
                    MessageBox.Show("Укажите  смену!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
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



               /* if (PhoneNumber.Text.Length >= 15)
                {
                    MessageBox.Show("Укажите достоверный номер телефона!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }*/
               /* if  (S.Text.Length >= 15)
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
                    MessageBox.Show("Отчество не может быть такой длинной", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Name.Text.Length >= 50)
                {
                    MessageBox.Show("Имя не может быть такой длинной", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }*/


                MedicalOfficer std = new MedicalOfficer
                {

                    FName = FName.Text,
                    Name = Name.Text,
                    LName = LName.Text,
                    ID_Gender = Gender.SelectedIndex + 1,
                    DateOfBirth = (DateTime)DateOfBirth.SelectedDate,
                    Address = Address.Text,
                    ID_Department = Department.SelectedIndex + 1,
                    Smena = Smena.Text,
                    ID_Cabinet = Cabinet.SelectedIndex +1,
                    StajRaboty = StajRaboty.Text,
                    Diplom = Diplom.Text,
                    Institute =Institute.Text,

                };
                helper.GetContext().MedicalOfficer.Add(std);
                helper.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }
        public string[] GenderMass { get; set; } =
        {
            "М",
            "Ж"
        };
        public string[] DepartmentMass { get; set; } =
       {
            "Терапевт",
            "Хирургия",
            "Пульмонолог",
            "Травмотолог",
            "Регистратура"
        };
        public string[] CabinetMass { get; set; } =
       {
            "215",
            "225",
            "105",
            "350",
            "185"
        };
    }
}