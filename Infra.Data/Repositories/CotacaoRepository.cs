using Domain.Entities;
using Domain.Ports;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class CotacaoRepository : ICotacaoRepository
    {
        private readonly AppDbContext _context;

        public CotacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal?> ObterTaxaAnualPorDataAsync(DateTime data)
        {
            var item = await _context.Cotacoes
            .FirstOrDefaultAsync(c => c.Data == data);


            return item?.Valor;
        }

        public async Task AddAsync(Cotacao cotacao)
        {
            _context.Cotacoes.Add(cotacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cotacao = await _context.Cotacoes.FindAsync(id);
            if (cotacao != null)
            {
                _context.Cotacoes.Remove(cotacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cotacao>> GetAllAsync()
        {
            return await _context.Cotacoes.ToListAsync();
        }

        public async Task<Cotacao> GetByIdAsync(Guid id)
        {
            return await _context.Cotacoes.FindAsync(id);
        }

        public async Task UpdateAsync(Cotacao cotacao)
        {
            _context.Cotacoes.Update(cotacao);
            await _context.SaveChangesAsync();
        }
    }
}
