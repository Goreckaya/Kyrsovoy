using Kursovoy.Model;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Window
    {
        Patient st;
        int DepartmentID;
        int MedicalOfficerID;
        int StatusPriemaID;
        int start;
        string Time;
        string DepartmentName;
        string StatusPriemaName;
        private List<DateTime> occupiedTimes = new List<DateTime>();
        public NewAppointment(Patient stu)
        {
            InitializeComponent();           
            st = stu;
             

            var DepartmentList = helper.GetContext().Department;
            foreach (var intem in DepartmentList)
            {
                if (intem.Name != "Регистратура")
                {
                    Department.Items.Add(intem.Name);
                }    
                
            }

            var StatusPriemaList = helper.GetContext().StatusPriema;
            foreach (var intem in StatusPriemaList)
            {
                StatusPriema.Items.Add(intem.Name);
            }
           
            FName.Text = st.FName;
            Name.Text = st.Name;
            LName.Text = st.LName;

        }
        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MedicalOfficer.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите врача!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }              
                
               
                Appointment std = new Appointment
                {
                    ID_Patient = st.ID_Patient,                    
                    ID_Department = DepartmentID,
                    Date = (DateTime)Date.SelectedDate,
                    ID_MedicalOfficer = MedicalOfficerID,
                    ID_StatusPriema = StatusPriemaID,
                    Time = Time.ToString(),


                };
                helper.GetContext().Appointment.Add(std);
                helper.GetContext().SaveChanges();

                AppointmentWindow Form5 = new AppointmentWindow(std);
                Form5.ShowDialog();
                MessageBox.Show("Запись создана!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            MedicalOfficer.Items.Clear();
            DepartmentName = Department.SelectedValue.ToString();
            var DepartN = helper.GetContext().Department.Where(P => P.Name == DepartmentName).FirstOrDefault();
            DepartmentID = DepartN.ID_Department;
           

            var MedicalOfficeList = helper.GetContext().MedicalOfficer.Where(P => P.ID_Department == DepartmentID);
            foreach (var intem in MedicalOfficeList)
            {

                string Name = intem.FName + " " + intem.Name + " " + intem.LName;
                MedicalOfficer.Items.Add(Name);


            }

        }

        private void StatusPriema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatusPriemaName = StatusPriema.SelectedValue.ToString();
            var StatusPr = helper.GetContext().StatusPriema.Where(P => P.Name == StatusPriemaName).FirstOrDefault();
            StatusPriemaID = StatusPr.ID_StatusPriema;
        }

        private void MedicalOfficer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Cabinet.Items.Clear();
                if(MedicalOfficer.SelectedItem == null)
                {
                    return;
                }
                string Name = MedicalOfficer.SelectedItem.ToString();
                
                string[] splitName = Name.Split(' ');

                string lastName = splitName[0];
                string firstName = splitName[1];
                string middleName = splitName[2];
                var IDMedic = helper.GetContext().MedicalOfficer.Where(P => P.FName == lastName && P.Name == firstName && P.LName == middleName).FirstOrDefault();
                MedicalOfficerID = Convert.ToInt32(IDMedic.ID_MedicalOfficer);
                var CabinetList = helper.GetContext().MedicalOfficer.Where(P => P.ID_MedicalOfficer == MedicalOfficerID).FirstOrDefault();
                var idCabinet = CabinetList.ID_Cabinet;
                var CabinetName = helper.GetContext().Cabinet.Where(P => P.ID_Сabinet == idCabinet).FirstOrDefault();
                Cabinet.Items.Add(CabinetName.Nomber);
                
                Buttons();
               












            }
            catch 
            {
                
                
                MedicalOfficer.Items.Clear();
                Department.Items.Clear();
                var DepartmentList = helper.GetContext().Department;
                foreach (var intem in DepartmentList)
                {
                    Department.Items.Add(intem.Name);
                }
                MessageBox.Show("Выберите дату!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }   
            
                
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Time1.Children.Clear();
            Time2.Children.Clear();
            Buttons();
        }

        public void pagButton_Click(object sender, RoutedEventArgs e)
        {
            
            start = Convert.ToInt32(((Button)sender).Tag.ToString());
            Button clickedButton = (Button)sender;
            clickedButton.IsEnabled = false;
            Time = clickedButton.Content.ToString();
        }

        public void Buttons()
        {
            try
            {
                if (MedicalOfficer.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите врача!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Time1.Children.Clear();
                Time2.Children.Clear();
                var smena = helper.GetContext().MedicalOfficer.Where(P => P.ID_MedicalOfficer == MedicalOfficerID).FirstOrDefault();
                if (smena.Smena == "1")
                {
                    var time = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time2 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time3 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time4 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time5 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time6 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "8:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time7 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time8 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time9 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                   
                    var time10 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time11 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time12 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "9:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time13 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time14 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time15 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time16 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time17 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time18 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "10:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time19 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time20 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time21 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time22 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time23 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time24 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "11:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    
                    if (time == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 0;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time2 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 1;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time3 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 2;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time4 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 3;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time5 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 4;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time6 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "8:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time7 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 6;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time8 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time9 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time10 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time11 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time12 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "9:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time13 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time14 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time15 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time16 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time17 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time18 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "10:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time19== 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time20 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time21 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time22 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time23 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time24 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "11:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    

                }
                if (smena.Smena == "2")
                {
                    
                    var time7 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time8 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time9 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time10 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time11 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time12 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "13:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time13 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time14 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time15 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time16 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time17 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time18 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "14:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time19 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time20 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time21 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time22 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time23 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time24 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "15:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    
                    var time26 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:00" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time27 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:10" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time28 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:20" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time29 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:30" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time30 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:40" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    var time31 = helper.GetContext().Appointment.Where(P => P.Date == (DateTime)Date.SelectedDate && P.Time == "16:50" && P.ID_MedicalOfficer == MedicalOfficerID).Count();
                    

                   
                    if (time7 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 6;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time8 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 7;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time9 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 8;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time10 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time11 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time12 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "13:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time13 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }

                    if (time14 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time15 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time16 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time17 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time18 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "14:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time1.Children.Add(myButton);
                    }
                    if (time19 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time20 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time21 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time22 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time23 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time24 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "15:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time26 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:00";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time27 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:10";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time28 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:20";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time29 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:30";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time30 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:40";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                    if (time31 == 0)
                    {
                        Button myButton = new Button();
                        myButton.Height = 30;
                        myButton.Content = "16:50";
                        myButton.Width = 40;
                        myButton.HorizontalAlignment = HorizontalAlignment.Center;
                        myButton.Tag = 5;
                        myButton.Click += new RoutedEventHandler(pagButton_Click);
                        Time2.Children.Add(myButton);
                    }
                }

            }
            catch
            {
                return;
            }
           
        }

        private void TimeReset_Click(object sender, RoutedEventArgs e)
        {
            
            Buttons();
        }
    }
}

