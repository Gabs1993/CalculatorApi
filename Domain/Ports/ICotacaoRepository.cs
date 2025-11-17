using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ICotacaoRepository
    {
        Task<decimal?> ObterTaxaAnualPorDataAsync(DateTime data);
        Task<Cotacao> GetByIdAsync(Guid id);
        Task<IEnumerable<Cotacao>> GetAllAsync();
        Task AddAsync(Cotacao cotacao);
        Task UpdateAsync(Cotacao cotacao);
        Task DeleteAsync(Guid id);
    }
}
