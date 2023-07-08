using AutoMapper;
using Notes.Data.Models;
using Notes.Data.Repository.Abstract;
using Notes.Data.Unitofwork.Abstract;
using Notes.Dto;
using Notes.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Service.Concrete
{
    public class NoteService : GenericService<NoteDto, Note>, INoteService
    {
        private readonly INoteRepository noteRepository;
        public NoteService(INoteRepository noteRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(noteRepository, mapper, unitOfWork)
        {
            this.noteRepository = noteRepository;
        }
    }
}
