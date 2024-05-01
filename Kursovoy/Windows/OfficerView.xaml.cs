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
    /// Логика взаимодействия для OfficerView.xaml
    /// </summary>
    public partial class OfficerView : Window
    {
        string fnd = "";
        public OfficerView()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            Officer.ItemsSource = helper.GetContext().MedicalOfficer.ToList();            
            if (fnd != "")
            {


                var data = helper.GetContext().Patient.OrderBy(Client => Client.ID_Patient).Where(Client => Client.FName.Contains(fnd) || Client.Name.Contains(fnd) || Client.LName.Contains(fnd) || Client.PhoneNumber.Contains(fnd) || Client.Address.Contains(fnd)).ToList();
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
                MessageBox.Show("Выберите пациента", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (Officer.SelectedIndex != -1)
            {
                try
                {



                    var St = Officer.SelectedItem as MedicalOfficer;
                    EditMedicalOfficer frm = new EditMedicalOfficer(St);
                    frm.ShowDialog();


                }
                catch
                {
                    MessageBox.Show("Ошибка", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
           
            
        }
    }
}

       

