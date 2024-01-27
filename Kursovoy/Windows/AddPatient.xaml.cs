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
    /// Логика взаимодействия для AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public AddPatient()
        {
            InitializeComponent();
            Gender.ItemsSource = GenderMass;
        }
        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Patient std = new Patient
                {

                    FName = FName.Text,
                    Name = Name.Text,
                    LName = LName.Text,
                    ID_Gender = Gender.SelectedIndex + 1,
                    DateOfBirth = (DateTime)DateOfBirth.SelectedDate,
                    PhoneNumber = PhoneNumber.Text,
                    Address = Address.Text,

                };
                helper.GetContext().Patient.Add(std);
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
            "Мужчина",
            "Женщина"
        };
    }
}
