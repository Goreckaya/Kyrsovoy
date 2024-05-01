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
    /// Логика взаимодействия для AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public AddPatient()
        {
            InitializeComponent();
            Gender.ItemsSource = GenderMass;
            Grajdanstvo.ItemsSource = GrajdanstvoMass;
            Status.ItemsSource = StatusMass;
        }
        private void AddPatient_Click(object sender, RoutedEventArgs e)
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



                if (PhoneNumber.Text.Length >= 15)
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
                    MessageBox.Show("Отчество не может быть такой длинной", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Name.Text.Length >= 50)
                {
                    MessageBox.Show("Имя не может быть такой длинной", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                

                Patient std = new Patient
                {

                    FName = FName.Text,
                    Name = Name.Text,
                    LName = LName.Text,
                    ID_Gender = Gender.SelectedIndex + 1,
                    DateOfBirth = (DateTime)DateOfBirth.SelectedDate,
                    PhoneNumber = PhoneNumber.Text,
                    Address = Address.Text,
                    Snils = Snils.Text,
                    Pasport = Pasport.Text,
                    Polis = Polis.Text,
                    ID_Grajdanstvo = Grajdanstvo.SelectedIndex +1,
                    ID_Status = Status.SelectedIndex +1

                };
                helper.GetContext().Patient.Add(std);
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
            "М",
            "Ж"
        };
        public string[] GrajdanstvoMass { get; set; } =
        {
            "Российская Федерация",
            "Гражданин не РФ"
        };
        public string[] StatusMass { get; set; } =
        {
            "Пенсионер",
            "Инвалид",
            "Без льгот"
        };
    }
}
