using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Data.Models;
using Notes.Dto;
using Notes.Service.Abstract;

namespace Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : BaseController<NoteDto, Note>
    {

        private readonly INoteService _noteService;
        public NoteController(INoteService noteService, IMapper mapper) : base(noteService, mapper)
        {
            this._noteService = noteService;
        }

        [HttpGet]
        public IActionResult ViewNotes()
        {
            var notes = _noteService.GetAllAsync();
            return View(notes.Result.Response.Where(x => x.isDeleted == false).ToList());
        }

        [HttpGet("GetNoteById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _noteService.GetByIdAsync(id);

            if (result.Success)
            {
                if (result.Response.isDeleted)
                {
                    return StatusCode(201, result);
                }
                
            }

            return BadRequest(result.Message);
        }

        [HttpPost("CreateNote")]
        public async Task<IActionResult> CreateAsync([FromForm] NoteDto entity)
        {
            var result = await _noteService.InsertAsync(entity);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return BadRequest(result.Message);
        }

        [HttpPut("UpdateNote")]
        [Route("UpdateNote/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] NoteDto req)
        {
            var future = await _noteService.GetByIdAsync(id);
            var updatedEntity = future.Response; 

            updatedEntity.Title = req.Title;
            updatedEntity.Content = req.Content;

            var result = await _noteService.UpdateAsync(id, updatedEntity);

            if (result.Success)
            {
                return StatusCode(201, result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("DeleteNote")]
        [Route("DeleteNote/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var future = await _noteService.GetByIdAsync(id);
            var updatedEntity = future.Response;

            updatedEntity.isDeleted = true;

            var result = await _noteService.UpdateAsync(id, updatedEntity);

            if (result.Success)
            {
                return RedirectToAction("ViewNotes");
            }

            return BadRequest(result.Message);
        }
    }
}
