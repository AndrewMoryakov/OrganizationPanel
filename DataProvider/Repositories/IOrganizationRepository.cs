using System.Collections.Generic;
using OrganizationPanel;

namespace DataProvider.Repositories
{
	public interface IOrganizationRepository
	{
		/// <summary>
		/// Возвращает всех сотрудников.
		/// </summary>
		/// <returns></returns>
		List<Organization> Get();
		/// <summary>
		/// Возвращает организацию по идентификатору.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Organization Get(int id);
		/// <summary>
		/// Обновляет организацию.
		/// </summary>
		/// <param name="entity"></param>
		void Update(Organization entity);
		/// <summary>
		/// Удаляет организацию.
		/// </summary>
		/// <param name="id"></param>
		void Delete(int id);
	}
}
