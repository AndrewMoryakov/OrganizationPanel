using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
			List<Organization> allOrganizations;
			try
			{
				allOrganizations = _dbQueries.GetAllOrganizations();
				if (!allOrganizations.Any())
					MessageBox.Show(FormMessages.NoOrganizations, FormMessages.HaventRecords);

				dataGridView1.DataSource = allOrganizations;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, FormMessages.Error);
			}
		}

		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (_idOfSelectedRow != null)
			{
				try
				{
					var employeesOfOrganization = _dbQueries.GetOrganizationEmployees((int) _idOfSelectedRow);
					if (!employeesOfOrganization.Any())
						MessageBox.Show(FormMessages.NoEmployees, FormMessages.HaventRecords);

					dataGridView2.DataSource = employeesOfOrganization;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, FormMessages.Error);
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (_idOfSelectedRow != null)
			{
				var insertedEmployees = _csvWork.ImportEmployeesFromCsv((int) _idOfSelectedRow);
				if (insertedEmployees.Count == 0)
				{
					MessageBox.Show(FormMessages.NoEmployees);
					return;
				}

				try
				{
					var countOfInsertedRecords = _dbQueries.InsertEmployeesToDb(insertedEmployees);
					if (countOfInsertedRecords == 0)
					{
						MessageBox.Show(FormMessages.EmployeesNotInserted, FormMessages.CantInsertRecords);
						return;
					}

					dataGridView2.DataSource = _dbQueries.GetOrganizationEmployees((int) _idOfSelectedRow);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, FormMessages.Error);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (_idOfSelectedRow != null)
			{
				var employees = _dbQueries.GetOrganizationEmployees((int) _idOfSelectedRow);
				_csvWork.ExportEmployeesToCsv(employees);
			}
			else
			{
				MessageBox.Show(FormMessages.NoSelectedOrg, FormMessages.CantExport);
			}
		}
	}
}
