using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationPanel
{
	public struct Employee
	{
		public Employee(string[] fields)
		{
			Id = Convert.ToInt32(fields[0]);
			FirstName = fields[1];
			SecondName = fields[2];
			MiddleName = fields[3];
			DateOfBirth = DateTime.Parse(fields[4]);
			PassportId = fields[5];
			PassportSeries = fields[6];
			Note = fields[7];
			OrganizationId = Convert.ToInt32(fields[8]);
		}
		
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string MiddleName { get; set; }
		public DateTime DateOfBirth { get; set; }
		[DisplayName("Номер паспорта")]
		public string PassportId { get; set; }
		[DisplayName("Серия паспорта")]
		public string PassportSeries { get; set; }
		public string Note { get; set; }
		public int OrganizationId { get; set; }
	}
}
