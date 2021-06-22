using System.Threading.Tasks;
using ProAgil.Domain.Entities;

namespace ProAgil.Repository
{
    public interface IRepository
    {
        //GERAL
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTOS
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrante);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
         Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrante);

         //PALESTRANTE
         Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento);
         Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEvento);



     }
}