using System.Collections.Generic;

namespace OrganizationPanel
{
    /// <summary>
    /// Предоставляет основные методы для работы с бизнес сущностями в базе данных
    /// </summary>
    public class DbHelper
    {
        private string _sqlConnection;
        private int _queryTimeOut;
        public DbHelper(string sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Возвращает все организации.
        /// </summary>
        /// <returns>Список организаций.</returns>
        public List<Organization> GetAllOrganizations()
        {
            List<Organization> allOrganizations;
            using (var db = new DbContext(_sqlConnection, _queryTimeOut))
            {
                var uow = new UnitOfWork(db);
                allOrganizations = uow.GetOrganizationRepository().Get();
            }

            return allOrganizations;
        }

        /// <summary>
        /// Добавляет список сотрудников в базу данных.
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public int InsertEmployeesToDb(List<Employee> employees)
        {
            int countOfInsertedEmploees = -1;
            using (var db = new DbContext(_sqlConnection, _queryTimeOut))
            {
                var uow = new UnitOfWork(db);
                countOfInsertedEmploees = uow.GetEmployeeRepository().Insert(employees);
            }

            return countOfInsertedEmploees;
        }

        /// <summary>
        /// Возвращает список сотрудников организации.
        /// </summary>
        /// <param name="idOfOrganization">Идентификатор организации.</param>
        /// <returns>Список сотрудников.</returns>
        public List<Employee> GetOrganizationEmployees(int idOfOrganization)
        {
            List<Employee> employeesOfOrganization = null;
            using (var db = new DbContext(_sqlConnection, _queryTimeOut))
            {
                var uow = new UnitOfWork(db);
                employeesOfOrganization = uow.GetEmployeeRepository().GetByOrganization((int) idOfOrganization);
            }

            return employeesOfOrganization;
        }
    }
}