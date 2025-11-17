using Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CalculoIndexadorService : ICalculoIndexadorService
    {
        private readonly ILogger<CalculoIndexadorService> _logger;

        public CalculoIndexadorService(ILogger<CalculoIndexadorService> logger)
        {
            _logger = logger;
        }


        public decimal CalcularFatorDiario(decimal taxaAnual)
        {
            decimal baseValor = 1 + (taxaAnual / 100);
            double power = 1.0 / 252.0;


            decimal fator = (decimal)Math.Pow((double)baseValor, power);

            _logger.LogDebug("Fator diário para taxa {TaxaAnual}%: {Fator}", taxaAnual, fator);
            return Math.Round(fator, 8, MidpointRounding.AwayFromZero);
        }


        public decimal CalcularFatorAcumulado(List<decimal> fatoresDiarios)
        {
            decimal acumulado = 1;


            foreach (var f in fatoresDiarios)
                acumulado *= f;


            return Truncar(acumulado, 16);
        }


        public decimal CalcularValorAtualizado(decimal valor, decimal fator)
        {
            decimal bruto = valor * fator;

            // Truncar 2 casas decimais
            decimal truncado = Math.Floor(bruto * 100) / 100;

            return truncado;
        }


        private decimal Truncar(decimal valor, int casas)
        {
            decimal fator = (decimal)Math.Pow(10, casas);
            return Math.Truncate(valor * fator) / fator;
        }
    }
}
