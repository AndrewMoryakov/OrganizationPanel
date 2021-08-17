using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using OrganizationPanel;

namespace DataProvider.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection _connection;
        private int _timeOut;

        public EmployeeRepository(SqlConnection connection, int queryTimeout)
        {
            _connection = connection;
            _timeOut = queryTimeout;
        }
        
        public List<Employee> Get()
        {
            throw new System.NotImplementedException();
        }

        public List<Employee> GetByOrganization(int organizationId)
        {
            var employees = new List<Employee>();
            using (var cmd = new SqlCommand())
            {
                if (_connection.State == ConnectionState.Open)
                {
                    cmd.Connection = _connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = _timeOut;
                    cmd.CommandText = $"SELECT * FROM Employee WHERE OrganizationId = '{organizationId.ToString()}'";

                    using (var selectReader = cmd.ExecuteReader())
                    {
                        while (selectReader.Read())
                        {
                            employees.Add(new Employee()
                            {
                                Id = selectReader.GetInt32("Id"),
                                FirstName = selectReader.GetString("FirstName"),
                                SecondName = selectReader.GetString("SecondName"),
                                MiddleName = selectReader.GetString("MiddleName"),
                                DateOfBirth = selectReader.GetDateTime("DateOfBirth"),
                                PassportId = selectReader.GetString("PassportNumber"),
                                OrganizationId = selectReader.GetInt32("OrganizationId"),
                                PassportSeries = selectReader.GetString("PassportSeries"),
                                Note = selectReader.GetString("Note")
                            });
                        }
                    }
                }

                return employees;
            }
        }

        public int Insert(List<Employee> emploees)
        {
            int countOfAfferedRows=0;
            using (var cmd = new SqlCommand())
            {
                if (_connection.State == ConnectionState.Open)
                {
                    try
                    {
                        cmd.Transaction = _connection.BeginTransaction();
                        cmd.Connection = _connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = _timeOut;

                        var values = string.Join(",", emploees.Select(el =>
                            $"('{el.FirstName}', '{el.SecondName}', '{el.MiddleName}'," +
                            $"'{el.DateOfBirth.ToString(CultureInfo.InvariantCulture)}'," +
                            $"'{el.PassportId}', '{el.PassportSeries}', '{el.Note}', '{el.OrganizationId}')"));
                        cmd.CommandText =
                            $"INSERT INTO Employee (FirstName, SecondName, MiddleName, DateOfBirth, PassportNumber," +
                            $"PassportSeries, Note, OrganizationId)"
                            + $"VALUES {values}";

                        countOfAfferedRows = cmd.ExecuteNonQuery();
                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                    }
                }
            }

            return countOfAfferedRows;
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}