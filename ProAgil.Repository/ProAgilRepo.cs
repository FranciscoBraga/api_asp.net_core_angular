using System;
using System.Threading.Tasks;
using ProAgil.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProAgil.Repository
{
    public class ProAgilRepo : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepo(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior =  QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
 
        public  async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }

        public  async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            if(includePalestrante)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento);

            return  await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncTema(string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            if(includePalestrante)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                        .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            
            return  await query.ToArrayAsync();
        }

        public  async Task<Evento> GetAllEventoAsyncByID(int EventoId, bool includePalestrante)
        {
           IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include(e => e.RedesSociais);

            if(includePalestrante)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                        .Where(e => e.Id == EventoId);
            
            return  await query.FirstOrDefaultAsync();
        }

        public  async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if(includeEvento)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
            }

            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return  await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteByID(int PalestranteId, bool includeEvento)
        {
           IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if(includeEvento)
            {
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Nome)
                         .Where(p => p.Id == PalestranteId);

            return  await query.FirstOrDefaultAsync();
        }

    }
}