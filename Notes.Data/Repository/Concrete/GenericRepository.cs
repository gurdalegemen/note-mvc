using Microsoft.EntityFrameworkCore;
using Notes.Data.Context;
using Notes.Data.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository.Concrete
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected readonly AppDbContext Context;
        private DbSet<Entity> entities;

        public GenericRepository(AppDbContext context)
        {
            this.Context = context;
            this.entities = Context.Set<Entity>();
        }

        public async Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> expression)
        {
            return await entities.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(int entityId)
        {
            return await entities.FindAsync(entityId);
        }

        public async Task InsertAsync(Entity entity)
        {
            await entities.AddAsync(entity);
        }

        public void RemoveAsync(Entity entity)
        {
            entities.Remove(entity);
        }

        public void Update(Entity entity)
        {
            entities.Update(entity);
        }
    }
}
