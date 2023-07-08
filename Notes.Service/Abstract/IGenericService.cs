using Notes.Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Service.Abstract
{
    public interface IGenericService<Dto,Entity>
    {
        Task<GenericResponse<Dto>> GetByIdAsync(int id);
        Task<GenericResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<GenericResponse<Dto>> InsertAsync(Dto insertResource);
        Task<GenericResponse<Dto>> UpdateAsync(int id, Dto updateResource);
        Task<GenericResponse<Dto>> RemoveAsync(int id);

    }
}
