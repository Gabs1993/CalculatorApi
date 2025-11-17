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
        public Task UpdateAsync(Cotacao cotacao) => _cotacaoRepository.UpdateAsync(cotacao);
        public Task DeleteAsync(Guid id) => _cotacaoRepository.DeleteAsync(id);
    }
}
