using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cotacao
    {
        public Guid id { get; set; }

        public DateTime Data { get; set; }

        public string Indexador { get; set; } = string.Empty;

        public decimal Valor { get; set; }
    }
}
