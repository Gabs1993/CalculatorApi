using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculator.TestCalculator
{
    public class CalculoIndexadorServiceTests
    {
        private readonly CalculoIndexadorService _service;

        public CalculoIndexadorServiceTests()
        {
            var loggerMock = new Mock<ILogger<CalculoIndexadorService>>();
            _service = new CalculoIndexadorService(loggerMock.Object);
        }

        [Fact]
        public void CalcularFatorDiario()
        {
            // Arrange
            decimal taxaAnual = 12m;

            // Act
            var fator = _service.CalcularFatorDiario(taxaAnual);

            // Assert
            Assert.Equal(1.00044982m, fator);
        }

        [Fact]
        public void CalcularFatorAcumulado()
        {
            // Arrange
            List<decimal> fatores = new()
        {
            1.00044982m,
            1.00046750m
        };

            // Act
            var acumulado = _service.CalcularFatorAcumulado(fatores);

            // Assert
            Assert.Equal(1.00091753_029085m, acumulado);
        }

        [Fact]
        public void CalcularValorAtualizado()
        {
            // Arrange
            decimal valor = 10000;
            decimal fator = 1.00274063296722m;

            // Act
            var atualizado = _service.CalcularValorAtualizado(valor, fator);

            // Assert
            Assert.Equal(10027.40m, atualizado);
        }
    }
}
