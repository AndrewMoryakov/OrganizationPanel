using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OrganizationPanel;

namespace DataProvider.Repositories
{
	public class OrganizationRepository : IOrganizationRepository
	{
		private SqlConnection _connection;
		private int _timeOut;
		public OrganizationRepository(SqlConnection connection, int queryTimeOut)
		{
			_connection = connection;
			_timeOut = queryTimeOut;
		}
		
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Organization> Get()
		{
			var organizations = new List<Organization>();
			using (var cmd = new SqlCommand())
			{
				if (_connection.State == ConnectionState.Open)
				{
					cmd.Connection = _connection;
					cmd.CommandType = CommandType.Text;
					cmd.CommandTimeout = _timeOut;
					cmd.CommandText = "SELECT * FROM Organization";		
					
					using (var selectReader = cmd.ExecuteReader())
					{
						while (selectReader.Read())
						{
							organizations.Add(new Organization
							{
								Id = selectReader.GetInt32("Id"),
								Name = selectReader.GetString("Name"),
								Inn = selectReader.GetString("Inn"),
								PhysicalAddress = selectReader.GetString("PhysicalAdress"),
								LegalAddress = selectReader.GetString("LegalAddress"),
								Note = selectReader.GetString("Note")
							});
						}
					}
				}
				
				return organizations;
			}
		}

		public Organization Get(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Organization organization)
		{
			throw new NotImplementedException();
		}
	}
}
