using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelNotesV2.Models;

namespace TravelNotesV2.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class model_listController : ControllerBase
    {
        private readonly TravelContext _context;

        public model_listController(TravelContext context)
        {
            _context = context;
        }

        // GET: api/model_list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<model_list>>> Getmodel_list()
        {
            return await _context.model_list.ToListAsync();
        }

        // GET: api/model_list/5
        [HttpGet("{id}")]
        public async Task<ActionResult<model_list>> Getmodel_list(int id)
        {
            var model_list = await _context.model_list.FindAsync(id);

            if (model_list == null)
            {
                return NotFound();
            }

            return model_list;
        }

        // PUT: api/model_list/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmodel_list(int id, model_list model_list)
        {
            if (id != model_list.modelId)
            {
                return BadRequest();
            }

            _context.Entry(model_list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!model_listExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/model_list
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<model_list>> Postmodel_list(model_list model_list)
        {
            _context.model_list.Add(model_list);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (model_listExists(model_list.modelId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getmodel_list", new { id = model_list.modelId }, model_list);
        }

        // DELETE: api/model_list/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemodel_list(int id)
        {
            var model_list = await _context.model_list.FindAsync(id);
            if (model_list == null)
            {
                return NotFound();
            }

            _context.model_list.Remove(model_list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool model_listExists(int id)
        {
            return _context.model_list.Any(e => e.modelId == id);
        }
    }
}
