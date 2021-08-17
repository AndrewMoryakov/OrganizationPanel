using System;
using System.Windows.Forms;

namespace OrganizationPanel
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string pathToExportingCsv = "d:\\out_eployees.csv";//args[0];
            string pathToImportingCsv = "d:\\employees_storage.csv";
            char csvDivider = ';';
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(pathToImportingCsv, pathToExportingCsv, 
                "Data Source=.;Initial Catalog=Organization; Integrated Security=True;", csvDivider));
        }
    }
}