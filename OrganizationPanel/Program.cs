using System;
using System.Threading;
using System.Windows.Forms;

namespace OrganizationPanel
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += (sender, eventArgs) => MessageBox.Show(eventArgs.Exception.Message);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            string pathToExportingCsv = args[0]; //"d:\\out_eployees.csv";
            string pathToImportingCsv = args[1]; //"d:\\employees_storage.csv";
            char csvDivider = args[2][0];
            string connectionString = args[3];
            if (args.Length == 0)
            {
                pathToExportingCsv = "out_eployees.csv";
                pathToImportingCsv = "employees_storage.csv";
                csvDivider = ';';
                connectionString = "Data Source=.;Initial Catalog=Organization; Integrated Security=True;";
            }
            else {
                pathToExportingCsv = args[0]; //"d:\\out_eployees.csv";
                pathToImportingCsv = args[1]; //"d:\\employees_storage.csv";
                csvDivider = args[2][0];
                connectionString = args[3];
            }
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(pathToImportingCsv, pathToExportingCsv, connectionString, csvDivider));
        }
    }
}