using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace Kursovoy.Windows
{
    /// <summary>
    /// Логика взаимодействия для LechenieWindow.xaml
    /// </summary>
    public partial class LechenieWindow : Window
    {
        public LechenieWindow(string Name, string FName, string LName, string HEAL, string Simptom,DateTime Dating, DateTime DatingStart, string snils)
        {
            InitializeComponent();
            string dateString = Dating.ToString("dd.MM.yyyy");
            string dateStringStart = DatingStart.ToString("dd.MM.yyyy");

            Datings.Text = dateString.ToString();
            DatingsStart.Text = dateStringStart.ToString();
            PacientName.Text = FName + " " + Name + " " + LName;
            Sim.Text = Simptom;
            Healing.Text = HEAL;
            PacientSNILS.Text = snils;
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
