using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class ExchangeRate : AuditableEntity
	{
        public Guid Id { get; set; }
        public string ImeValute { get; set; }
		public decimal KupovniTecaj { get; set; }
		public decimal SrednjiTecaj { get; set; }
		public decimal ProdajniTecaj { get; set; }
        public DateTime DatumUnosa { get; set; }
    }
}
