using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleTaskApp.Models;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;

namespace SampleTaskApp.Controllers.Authorised
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfDoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public EfDoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/EfDoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctors>>> GetDoctors()
        {
            
            try
            {
                var data = await _unitOfWork.EfDoctorsRepository.GetAllAsync();
                var rType = new CommonOperation { Type = 6, Data = data, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching hospital data."
                });
            }
        }

        // GET: api/EfDoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctors>> GetDoctors(int id)
        {
            try
            {
                var data = await _unitOfWork.EfDoctorsRepository.GetByIdAsync(id);
                var rType = new CommonOperation { Type = 6, Data = data, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching hospital data."
                });
            }
            
        }

        // PUT: api/EfDoctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctors(int id, Doctors doctors)
        {
            if (id != doctors.DoctorId)
            {
                return BadRequest();
            }
            try
            {
                var data = await _unitOfWork.EfDoctorsRepository.GetByIdAsync(id);
                var rType = new CommonOperation { Type = 6, Data = data, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching hospital data."
                });
            }
            _context.Entry(doctors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
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

        // POST: api/EfDoctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctors>> PostDoctors(Doctors doctors)
        {
            _context.Doctors.Add(doctors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctors", new { id = doctors.DoctorId }, doctors);
        }

        // DELETE: api/EfDoctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
