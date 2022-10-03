using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantesPersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantesPersistence(ProEventosContext context)
        {
            _context = context;

        }
        
        // PALESTRANTE
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            // Incluindo se houver palestrantes
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);
            return await query.ToArrayAsync();

        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            // Incluindo se houver palestrantes
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            // PESQUISA PELO NOME NO BANCO
            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();

        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        { 
             IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            // Incluindo se houver palestrantes
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }


    }
}