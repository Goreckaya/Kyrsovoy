﻿using Kursovoy.Models;
using Kursovoy.Pages;
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

namespace Kursovoy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class helper
    {
        public static HospitalEntities3 ent;
        internal static bool flag = false;
        internal static int prioritet = 0;

        public static HospitalEntities3 GetContext()
        {
            if (ent == null)
            {
                ent = new HospitalEntities3();
            }
            return ent;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {

            string password = Password.Text.ToString();
            string login = Login.Text.ToString();
            var EnterAcc = helper.GetContext().Vhod.Where(p => p.Password == password && p.Login == login).FirstOrDefault();
          

            if (EnterAcc != null)
            {
                
                if (EnterAcc.Role == 1)
                {
                    DataView Form4 = new DataView();
                    Form4.Show();
                    this.Close();
                }    
               
               
                if (EnterAcc.Role == 2)
                {

                    DataRegView Form5 = new DataRegView();
                    Form5.Show();
                    this.Close();
                }

            }            

            else
            {
                MessageBox.Show("Повторите вход ", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }
    }
}

