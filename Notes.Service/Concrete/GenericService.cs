using AutoMapper;
using Notes.Data.Repository.Abstract;
using Notes.Data.Response;
using Notes.Data.Unitofwork.Abstract;
using Notes.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Service.Concrete
{
    public class GenericService<Dto, Entity> : IGenericService<Dto, Entity> where Entity : class
    {
        private readonly IGenericRepository<Entity> baseRepository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;

        public GenericService(IGenericRepository<Entity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork) : base()
        {
            this.baseRepository = baseRepository;
            this.Mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            var tempEntity = await baseRepository.GetAllAsync();

            var result = Mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);

            return new GenericResponse<IEnumerable<Dto>>(result);
        }

        public async Task<GenericResponse<Dto>> GetByIdAsync(int id)
        {
            var tempEntity = await baseRepository.GetByIdAsync(id);

            var result = Mapper.Map<Entity, Dto>(tempEntity);

            return new GenericResponse<Dto>(result);
        }

        public async Task<GenericResponse<Dto>> InsertAsync(Dto insertResource)
        {
            try
            {
                var tempEntity = Mapper.Map<Dto, Entity>(insertResource);

                await baseRepository.InsertAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new GenericResponse<Dto>(Mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<GenericResponse<Dto>> RemoveAsync(int id)
        {
            try
            {
                var tempEntity = await baseRepository.GetByIdAsync(id);

                if(tempEntity is null)
                {
                    return new GenericResponse<Dto>("NoData");
                }

                baseRepository.RemoveAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new GenericResponse<Dto>(Mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<GenericResponse<Dto>> UpdateAsync(int id, Dto updateResource)
        {
            try
            {
                var tempEntity = await baseRepository.GetByIdAsync(id);

                if (tempEntity is null)
                {
                    return new GenericResponse<Dto>("NoData");
                }

                Mapper.Map(updateResource, tempEntity);

                await UnitOfWork.CompleteAsync();


                return new GenericResponse<Dto>(Mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
