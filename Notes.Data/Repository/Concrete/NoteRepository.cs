using Microsoft.EntityFrameworkCore;
using Notes.Data.Context;
using Notes.Data.Models;
using Notes.Data.Repository.Abstract;

namespace Notes.Data.Repository.Concrete
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            var query = Context.Note.Where(x => x.Id.Equals(id));
            return await query.SingleOrDefaultAsync();
        }
    }
}
