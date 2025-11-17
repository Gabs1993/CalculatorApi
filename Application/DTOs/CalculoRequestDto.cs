using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CalculoRequestDto
    {
        public decimal ValorAplicado { get; set; }
        public DateTime DataAplicacao { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
