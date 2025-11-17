using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ICalculoIndexadorService
    {
        decimal CalcularFatorDiario(decimal taxaAnual);
        decimal CalcularFatorAcumulado(List<decimal> fatoresDiarios);
        decimal CalcularValorAtualizado(decimal valorAplicado, decimal fatorAcumulado);
    }
}
