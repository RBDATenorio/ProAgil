using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.Entities;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly DataContext _context;
        public ProAgilRepository(DataContext context)
        {
            _context = context;
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
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if(includePalestrante)
            {
                query = query.Include(c => c.PalestrantesEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderByDescending( c => c.DataEvento );
            
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include( l => l.Lotes)
            .Include( r => r.RedesSociais );

            if(includePalestrante)
            {
                query = query.Include( p => p.PalestrantesEventos)
                .ThenInclude( p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderByDescending( d => d.DataEvento)
                                        .Where( t => t.Tema.ToLower().Contains(tema.ToLower()) );
                                        
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                                .Include(l => l.Lotes)
                                                .Include(r => r.RedesSociais);
            if(includePalestrante)                                            
            {
                query = query.Include( p => p.PalestrantesEventos)
                            .ThenInclude( p => p.Palestrante);
            }

            query = query.AsNoTracking()
                        .OrderByDescending(d => d.DataEvento)
                            .Where(e => e.EventoId == eventoId );

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(e => e.RedesSociais );
            if(includeEventos)
            {
                query = query.Include( e => e.PalestrantesEventos)
                            .ThenInclude( e => e.Evento);
            }

            query = query.AsNoTracking()
                        .Where( p => p.Nome.ToLower().Contains(name.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                .Include( r => r.RedesSociais);
            if(includeEventos)
            {
                query = query.Include( e => e.PalestrantesEventos)
                                .ThenInclude( e => e.Evento );
            }

            query = query.AsNoTracking()
                        .Where( p => p.PalestranteId == palestranteId);
            return await query.FirstOrDefaultAsync();
        }

    }
}