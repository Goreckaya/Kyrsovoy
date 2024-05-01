using Kursovoy.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Логика взаимодействия для AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        Appointment st;
        public AppointmentWindow(Appointment stu)
        {
            InitializeComponent();
           

            var IdPacient = stu.ID_Patient;            
            var NamePacient = helper.GetContext().Patient.Where(P => P.ID_Patient == IdPacient);
            string Name = "";
            foreach (var intem in NamePacient)
            {

                Name = intem.FName + " " + intem.Name + " " + intem.LName;
                

            }
            PacientName.Text = Name;

            var appoint = helper.GetContext().Appointment.Where(P => P.ID_Patient == IdPacient).FirstOrDefault();
            string dateString = stu.Date.ToString("dd.MM.yyyy");
            Date.Text = dateString;
            Time.Text = stu.Time.ToString();

            var IdDepartment = appoint.ID_Department;
            var NameDepartment = helper.GetContext().Department.Where(P => P.ID_Department == IdDepartment).FirstOrDefault();
            string NameDep = NameDepartment.Name;         
            Department.Text = NameDep;

            var IdStatusPriema = appoint.ID_StatusPriema;
            var NameStatusPriema = helper.GetContext().StatusPriema.Where(P => P.ID_StatusPriema == IdStatusPriema).FirstOrDefault();
            string NameSt = NameStatusPriema.Name;
            StatusPriema.Text = NameSt;

            var IdOfficer = appoint.ID_MedicalOfficer;
            var NameOfficer = helper.GetContext().MedicalOfficer.Where(P => P.ID_MedicalOfficer == IdOfficer);
            int NumberCabinetid = 0;
            foreach (var intem in NameOfficer)
            {

                Name = intem.FName + " " + intem.Name + " " + intem.LName;
                NumberCabinetid = Convert.ToInt32(intem.ID_Cabinet);

            }           
            Officer.Text = Name;

           
            var NameCabinet = helper.GetContext().Cabinet.Where(P => P.ID_Сabinet == NumberCabinetid);
            int NumberCabinetName = 0;
            foreach (var intem in NameCabinet)
            {

                
                NumberCabinetName = Convert.ToInt32(intem.Nomber);

            }
            NumberCabinet.Text = NumberCabinetName.ToString();
        }

        private void PDFSave_Clicl(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDoc;
                pd.PrintDocument(idp.DocumentPaginator, Title);
            }

        }
    }
}
