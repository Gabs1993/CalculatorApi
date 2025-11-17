using Application.DTOs;
using Application.Services;
using Domain.Ports;
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
    public class CalculoApplicationServiceTests
    {
        private readonly Mock<ICotacaoRepository> _cotacaoRepositoryMock;
        private readonly Mock<ICalculoIndexadorService> _calculoIndexadorServiceMock;
        private readonly CalculoApplicationService _service;
        private readonly Mock<ILogger<CalculoApplicationService>> _loggerMock;


        public CalculoApplicationServiceTests()
        {
            _cotacaoRepositoryMock = new Mock<ICotacaoRepository>();
            _calculoIndexadorServiceMock = new Mock<ICalculoIndexadorService>();
            _loggerMock = new Mock<ILogger<CalculoApplicationService>>();

            _service = new CalculoApplicationService(
                _cotacaoRepositoryMock.Object,
                _calculoIndexadorServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task CalcularAsyncTest()
        {
            // Arrange
            var request = new CalculoRequestDto
            {
                ValorAplicado = 10000,
                DataAplicacao = new DateTime(2025, 03, 13),
                DataFinal = new DateTime(2025, 03, 21)
            };

            
            var diasUteis = new List<DateTime>
            {
                new DateTime(2025, 03, 14),
                //new DateTime(2025, 03, 16),
                new DateTime(2025, 03, 17),
                new DateTime(2025, 03, 18),
                new DateTime(2025, 03, 19),
                new DateTime(2025, 03, 20)
            };

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 13)))
                .ReturnsAsync(12.50m);

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 14)))
                .ReturnsAsync(11m);

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 16)))
                .ReturnsAsync(11m); 

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 17)))
                .ReturnsAsync(12.2m);

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 18)))
                .ReturnsAsync(13m);

            
            _cotacaoRepositoryMock.Setup(x => x.ObterTaxaAnualPorDataAsync(new DateTime(2025, 03, 19)))
                .ReturnsAsync(12.4m);

            
            _calculoIndexadorServiceMock.Setup(x => x.CalcularFatorDiario(It.IsAny<decimal>()))
                .Returns((decimal taxa) => 1.0004m);

            
            _calculoIndexadorServiceMock.Setup(x => x.CalcularFatorAcumulado(It.IsAny<List<decimal>>()))
                .Returns((List<decimal> fatores) => 1.0027m);

            
            _calculoIndexadorServiceMock.Setup(x => x.CalcularValorAtualizado(10000, 1.0027m))
                .Returns(10027.40m);

            // Act
            var resultado = await _service.CalcularAsync(request);

            // Assert
            Assert.Equal(1.0027m, resultado.FatorAcumulado);
            Assert.Equal(10027.40m, resultado.ValorAtualizado);

            
            _cotacaoRepositoryMock.Verify(x => x.ObterTaxaAnualPorDataAsync(It.IsAny<DateTime>()), Times.Exactly(5));

            
            _calculoIndexadorServiceMock.Verify(x => x.CalcularFatorDiario(It.IsAny<decimal>()), Times.Exactly(5));
            _calculoIndexadorServiceMock.Verify(x => x.CalcularFatorAcumulado(It.IsAny<List<decimal>>()), Times.Once);
            _calculoIndexadorServiceMock.Verify(x => x.CalcularValorAtualizado(10000, 1.0027m), Times.Once);
        }
    }
}
