using Backend_tareas.Context;
using Backend_tareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public TareaController( AplicationDbContext context )
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listadetareas =  await _context.tareas.ToListAsync();
                return Ok(listadetareas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task <IActionResult> Post([FromBody]Tarea tarea)
        {
            try
            {
                _context.tareas.Add(tarea);
                await _context.SaveChangesAsync();
                return Ok(new {message ="la tarea fue registrada con exito" });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Tarea tarea)
        {
            try
            {
                if(id != tarea.Id)
                {
                    return NotFound();
                }
                tarea.Estado = !tarea.Estado;
                _context.Entry(tarea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new {message ="actualizado con exito" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarea = await _context.tareas.FindAsync(id);
                if(tarea== null)
                {
                    return NotFound();
                }
                _context.tareas.Remove(tarea);
                await _context.SaveChangesAsync();
                return Ok(new {message="tarea eliminada de lista!" });
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
        }
    }
}
