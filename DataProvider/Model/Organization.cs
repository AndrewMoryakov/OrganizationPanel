using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationPanel
{
	public class Organization
	{
		public int Id { get; internal set; }
		[DisplayName("Название")]
		public string Name { get; internal set; }
		[DisplayName("Инн")]
		public string Inn { get; internal set; }
		[DisplayName("Фактический адрес")]
		public string PhysicalAddress { get; internal set; }
		[DisplayName("Юридический адрес")]
		public string LegalAddress { get; internal set; }
		[DisplayName("Примечание")]
		public string Note { get; internal set; }
	}
}
