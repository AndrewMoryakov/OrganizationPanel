using System.Collections.Generic;
using OrganizationPanel;

namespace DataProvider.Repositories
{
	public interface IEmployeeRepository
	{
		/// <summary>
		/// Возвращает всех сотрудников.
		/// </summary>
		/// <returns></returns>
		List<Employee> Get();
		//Возвращает сотрудников организации.
		List<Employee> GetByOrganization(int organizationId);
		/// <summary>
		/// Вставляет сотрудников организации в базу данных.
		/// </summary>
		/// <param name="emploees"></param>
		/// <returns></returns>
		int Insert(List<Employee> emploees);
		/// <summary>
		/// Получает сотрузника по идентификатору организации.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Employee Get(int id);
		/// <summary>
		/// Обновляет сотрудника.
		/// </summary>
		/// <param name="employee"></param>
		void Update(Employee employee);
		/// <summary>
		/// Удаляет сотрудника.
		/// </summary>
		/// <param name="id"></param>
		void Delete(int id);
	}
}
