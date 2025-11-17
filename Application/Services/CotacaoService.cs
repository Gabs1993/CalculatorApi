using Application.DTOs;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CotacaoService
    {
        private readonly ICotacaoRepository _cotacaoRepository;

        public CotacaoService(ICotacaoRepository repository)
        {
            _cotacaoRepository = repository;
        }

        public Task<IEnumerable<Cotacao>> GetAllAsync() => _cotacaoRepository.GetAllAsync();
        public Task<Cotacao> GetByIdAsync(Guid id) => _cotacaoRepository.GetByIdAsync(id);
        public Task AddAsync(Cotacao cotacao) => _cotacaoRepository.AddAsync(cotacao);
        public async Task UpdateAsync(Guid id, UpdateCotacaoDto dto)
        {
            var cotacao = await _cotacaoRepository.GetByIdAsync(id);
            if (cotacao == null)
                throw new Exception("Cotação não encontrada");

            cotacao.Data = dto.Data;
            cotacao.Indexador = dto.Indexador;
            cotacao.Valor = dto.Valor;

            await _cotacaoRepository.UpdateAsync(cotacao);
        }
        public Task DeleteAsync(Guid id) => _cotacaoRepository.DeleteAsync(id);
    }
}
