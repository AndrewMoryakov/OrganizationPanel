using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OrganizationPanel
{
	/// <summary>
	/// Реализация бизнес логики с использованием <see cref="CsvFile"/> для сущностей <see cref="Employee"/> и <see cref="Organization"/>
	/// Предоставляет методы для работы с файлами в которые экспортируются сотрудники и из которого импортируются сотрудники
	/// </summary>
	public class CsvHelper
	{
		private CsvFile _csvFile;

		public CsvHelper(CsvFile csvFile)
		{
			_csvFile = csvFile;
		}

		/// <summary>
		/// Выгружает сотрудников организации
		/// </summary>
		/// <param name="idOfOrganization">Идентификатор организации</param>
		/// <returns>Сотрудники, указанной организации</returns>
		public List<Employee> ImportEmployeesFromCsv(int idOfOrganization)
		{
			var importedEmployees = GetAllEmployees(_csvFile.SourceCsvPath)
				.Where(el => el.OrganizationId == idOfOrganization).ToList();
			return importedEmployees;
		}

		private List<Employee> GetAllEmployees(string pathToFile)
		{
			var employeesFromCsv = _csvFile.ReadAllLines(pathToFile);
			return employeesFromCsv.Select(el => new Employee(el)).ToList();
		}

		/// <summary>
		/// Запись сотрудников в CSV
		/// </summary>
		/// <param name="employees"></param>
		public void ExportEmployeesToCsv(List<Employee> employees)
		{
			var employeesFromCsv = GetAllEmployees(_csvFile.TargetCsvPath);
			var unicEmployees = employees.Where(el => !employeesFromCsv.Select(empl => empl.Id).Contains(el.Id));
			
			foreach (var empl in unicEmployees)
			{
				_csvFile.WriteLine(new[]
				{
					empl.Id.ToString(), empl.FirstName, empl.SecondName, empl.MiddleName, empl.DateOfBirth.ToString(CultureInfo.InvariantCulture),
					empl.PassportId, empl.PassportSeries, empl.Note, empl.OrganizationId.ToString()
				}, ';');
			}
		}
	}
}