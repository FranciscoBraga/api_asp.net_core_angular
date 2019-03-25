using System.Threading.Tasks;
using ProAgil.Domain;
namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //geral
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();

         //eventos
         Task<Evento[]> GetAllEventoAsyncTema(string tema, bool includePalestrante);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
         Task<Evento> GetAllEventoAsyncByID(int EventoId, bool includePalestrante);

         //palestrante
         Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento);
         Task<Palestrante> GetAllPalestranteByID(int PalestranteId, bool includeEvento);




       
    }
}