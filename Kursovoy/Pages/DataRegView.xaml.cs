﻿using Kursovoy.Models;
using Kursovoy.Windows;
using System;
using System.Collections;
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

namespace Kursovoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataRegView.xaml
    /// </summary>
    public partial class DataRegView : Window
    {
        
        string fnd = "";
        public DataRegView()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            PatientView.ItemsSource = helper.GetContext().Patient.ToList();
            if (fnd != "")
            {
                
                    
                    var data = helper.GetContext().Patient.OrderBy(Client => Client.ID_Patient).Where(Client => Client.FName.Contains(fnd) || Client.Name.Contains(fnd) || Client.LName.Contains(fnd) || Client.PhoneNumber.Contains(fnd) || Client.Address.Contains(fnd)).ToList();
                    PatientView.ItemsSource = data;
                }
            }
        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            //frame.Navigate(new PatientPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            Close();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientView.SelectedIndex == -1)
            {
                MessageBox.Show("Выделите строку для удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (PatientView.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        var idi = (PatientView.SelectedItem as Patient).ID_Patient;

                        var StaffDelete = helper.GetContext().Patient.Where(p => p.ID_Patient == idi).FirstOrDefault();
                        helper.GetContext().Patient.Remove(StaffDelete);
                        helper.GetContext().SaveChanges();
                        MessageBox.Show("Пациент удален", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
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
            AddPatient frm = new AddPatient();
            frm.ShowDialog();
            Load();
        }

        private void rebButton_Click(object sender, RoutedEventArgs e)
        {

            var St = PatientView.SelectedItem as Patient;
            EditPatient frm = new EditPatient(St);
            frm.ShowDialog();
            Load();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            fnd = ((TextBox)sender).Text;
            Load();
        }

        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (PatientView.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пациента", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (PatientView.SelectedIndex != -1)
            {
                try
                {



                    var idi = PatientView.SelectedItem as Patient;
                    NewAppointment frm2 = new NewAppointment(idi);
                    frm2.ShowDialog();


                }
                catch
                {
                    MessageBox.Show("Ошибка удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        
    }
}

