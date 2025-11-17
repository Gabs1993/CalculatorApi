using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CotacaoDto
    {
        public Guid id { get; set; }
        public DateTime Data { get; set; }
        public string Indexador { get; set; }  = string.Empty; 
        public decimal Valor { get; set; }
    }
}
