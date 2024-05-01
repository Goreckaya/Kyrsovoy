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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursovoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedOfficerPage.xaml
    /// </summary>
    public partial class MedOfficerPage : Page
    {
        string fnd = "";
        public MedOfficerPage()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            Officer.ItemsSource = helper.GetContext().MedicalOfficer.ToList();
            if (fnd != "")
            {


                var data = helper.GetContext().MedicalOfficer.OrderBy(Client => Client.ID_MedicalOfficer).Where(Client => Client.FName.Contains(fnd) || Client.Name.Contains(fnd) || Client.LName.Contains(fnd) || Client.Address.Contains(fnd)).ToList();
                Officer.ItemsSource = data;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fnd = ((TextBox)sender).Text;
            Load();
        }

        private void rebButton_Click(object sender, RoutedEventArgs e)
        {
            if (Officer.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите сотрудника", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (Officer.SelectedIndex != -1)
            {
                try
                {



                    var St = Officer.SelectedItem as MedicalOfficer;
                    EditMedicalOfficer frm = new EditMedicalOfficer(St);
                    frm.ShowDialog();
                    Load();


                }
                catch
                {
                    MessageBox.Show("Ошибка", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (Officer.SelectedIndex == -1)
            {
                MessageBox.Show("Выделите строку для удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (Officer.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите удалить этого работника?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        var idi = (Officer.SelectedItem as MedicalOfficer).ID_MedicalOfficer;

                        var StaffDelete = helper.GetContext().MedicalOfficer.Where(p => p.ID_MedicalOfficer == idi).FirstOrDefault();
                        helper.GetContext().MedicalOfficer.Remove(StaffDelete);
                        helper.GetContext().SaveChanges();
                        MessageBox.Show("Работник удален", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();

                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddMedOfficer frm = new AddMedOfficer();
            frm.ShowDialog();
            Load();
        }
    }
}
