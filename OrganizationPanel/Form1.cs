using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OrganizationPanel
{
	public partial class Form1 : Form
	{
		private UnitOfWork _uow;
		private int _indexOfRowIdCell = 0;
		private string _sqlConnection;
		private int? _idOfSelectedRow => (int?) dataGridView1.CurrentRow?.Cells[_indexOfRowIdCell].Value;
		private CsvFile _csvDataProvider;
		private string _importingCsvPath;
		private string _exportingCsvPath;
		private DbHelper _dbQueries;
		private CsvHelper _csvWork;

		public Form1(string importingCsvPath, string exportingCsvPath, string dataBaseConnection, char divider)
		{
			InitializeComponent();

			_sqlConnection = dataBaseConnection;
			_importingCsvPath = importingCsvPath;
			_exportingCsvPath = exportingCsvPath;
			dataGridView1.AutoGenerateColumns = true;

			_csvDataProvider = new CsvFile(_importingCsvPath, _exportingCsvPath, divider);
			_dbQueries = new DbHelper(_sqlConnection);
			_csvWork = new CsvHelper(_csvDataProvider);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var allOrganizations = _dbQueries.GetAllOrganizations();
			if (!allOrganizations.Any())
				MessageBox.Show(FormMessages.NoOrganizations); //Не стал обарачивать в отдельный метод, так как в данном случае напротив усложнит чтение кода.

			dataGridView1.DataSource = allOrganizations;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var selectedOrg = GetIdOfSelectedOrganization();
			var employees = GetEmployeesFromDbByOrg(selectedOrg);
			_csvWork.ExportEmployeesToCsv(employees);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var orgId = GetIdOfSelectedOrganization();
			var allOrgEmployeesFromCsv = GetEmployeesFromCsvForOrganization(orgId);
			var allOrgEmployeesFromDb = _dbQueries.GetOrganizationEmployees(orgId);
			var unicEmployees = FilterOnlyUnicEmployees(allOrgEmployeesFromCsv, allOrgEmployeesFromDb);
			InsertEmployeesToDb(unicEmployees);
			dataGridView2.DataSource = unicEmployees;
		}

		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			var orgId = GetIdOfSelectedOrganization();
			var employeesOfOrganization = GetEmployeesFromDbByOrg(orgId);
			dataGridView2.DataSource = employeesOfOrganization;
		}

		private List<Employee> GetEmployeesFromCsvForOrganization(int idOrg)
		{
			var employees = _csvWork.ImportEmployeesFromCsv(idOrg);
			if (employees.Count == 0)
				throw new Exception(FormMessages.NoEmployees);

			return employees;
		}

		private List<Employee> FilterOnlyUnicEmployees(List<Employee> emplFromCsv, List<Employee> emplFromDb)
		{
			var unicEmployees = emplFromCsv
				.Where(el => !emplFromDb.Select(empl => empl.Id).Contains(el.Id)).ToList();

			if (unicEmployees.Count() == 0)
				throw new Exception(FormMessages.CsvHaventNewEmployees);

			return unicEmployees;
		}

		private int InsertEmployeesToDb(List<Employee> emploees)
		{
			var countOfInsertedRecords = _dbQueries.InsertEmployeesToDb(emploees.ToList());
			if (countOfInsertedRecords == 0)
				throw new Exception(FormMessages.EmployeesNotInserted);

			return countOfInsertedRecords;
		}

		private int GetIdOfSelectedOrganization()
		{
			if (_idOfSelectedRow == null)
				throw new Exception(FormMessages.NoSelectedOrg);

			return (int) _idOfSelectedRow;
		}

		private List<Employee> GetEmployeesFromDbByOrg(int orgId)
		{
			var employees = _dbQueries.GetOrganizationEmployees((int) _idOfSelectedRow);
			if (employees.Count == 0)
				throw new ArgumentException(FormMessages.NoEmployees);

			return employees;
		}
	}
}