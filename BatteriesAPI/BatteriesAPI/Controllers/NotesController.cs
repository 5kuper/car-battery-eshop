using BatteriesAPI.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using BattAPI.App.Specific.Notes;
using BattAPI.App.Specific.Notes.Models;
using Microsoft.AspNetCore.Authorization;

namespace BatteriesAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class NotesController(INoteService service)
        : DtoCrudControllerBase<INoteService, NoteInput, NoteDto, NotePatch>(service)
    {
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Create(NoteInput input)
        {
            return base.Create(input);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Update(Guid id, NotePatch patch)
        {
            return base.Update(id, patch);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}
