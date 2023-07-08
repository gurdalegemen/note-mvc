using Notes;
using Notes.Data.Models;
using Notes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Service.Abstract
{
    public interface INoteService : IGenericService<NoteDto,Note>
    {

    }
}
