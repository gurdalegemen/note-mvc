using Notes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository.Abstract
{
    public interface INoteRepository :IGenericRepository<Note>
    {
        Task<Note> GetByIdAsync(int id);
    }
}
