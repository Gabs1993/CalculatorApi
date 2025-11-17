using Application.DTOs;
using Domain.Ports;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class CalculoApplicationService
    {
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly ICalculoIndexadorService _service;
        private readonly ILogger<CalculoApplicationService> _logger;

        public CalculoApplicationService(ICotacaoRepository cotacaoRepository, ICalculoIndexadorService calculoIndexadorService, ILogger<CalculoApplicationService> logger)
        {
            _cotacaoRepository = cotacaoRepository;
            _service = calculoIndexadorService;
            _logger = logger;

        }


        public async Task<CalculoResponseDto> CalcularAsync(CalculoRequestDto request)
        {
            _logger.LogInformation("Iniciando cálculo: Valor={Valor}, DataInicial={DataInicial}, DataFinal={DataFinal}",
                request.ValorAplicado, request.DataAplicacao, request.DataFinal);

            List<decimal> fatoresDiarios = new();

            for (var data = request.DataAplicacao.AddDays(1); data < request.DataFinal; data = data.AddDays(1))
            {
                if (data.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                    _logger.LogInformation("Ignorando fim de semana: {Data}", data);
                    continue;
                }

                var taxa = await _cotacaoRepository.ObterTaxaAnualPorDataAsync(data.AddDays(-1));

                if (taxa is null)
                {
                    _logger.LogWarning("Nenhuma taxa encontrada para {Data}", data.AddDays(-1));
                    continue;
                }

                var fator = _service.CalcularFatorDiario(taxa.Value);
                _logger.LogDebug("Fator diário calculado para {Data}: {Fator}", data, fator);

                fatoresDiarios.Add(fator);
            }

            var acumulado = _service.CalcularFatorAcumulado(fatoresDiarios);
            var valorAtualizado = _service.CalcularValorAtualizado(request.ValorAplicado, acumulado);

            _logger.LogInformation("Cálculo finalizado: FatorAcumulado={Acumulado}, ValorAtualizado={Valor}", acumulado, valorAtualizado);

            return new CalculoResponseDto
            {
                FatorAcumulado = acumulado,
                ValorAtualizado = valorAtualizado
            };
        }
    }
}
