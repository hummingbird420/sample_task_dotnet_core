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
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
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
        public async Task<ActionResult<Doctor>> GetDoctors(int id)
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
                    Message = "An error occurred while fetching doctor data."
                });
            }
            
        }

        // PUT: api/EfDoctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctors(int id, Doctor doctors)
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
                    Message = "An error occurred while fetching doctor data."
                });
            }
            
        }

        // POST: api/EfDoctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctors(Doctor doctors)
        {
            try
            {
                await _unitOfWork.EfDoctorsRepository.AddAsync(doctors);
                await _unitOfWork.CompleteAsync();
                var rType = new CommonOperation { Type = 1, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching doctor data."
                });
            }
            
        }

        // DELETE: api/EfDoctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            try
            {
                await _unitOfWork.EfDoctorsRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                var rType = new CommonOperation { Type = 3, Status = StatusCodes.Status200OK };
                return Ok(rType);
            }
            catch (Exception)
            {

                return Ok(new CommonOperation
                {
                    Type = 5,
                    Status = StatusCodes.Status500InternalServerError, // Return appropriate status codes for errors
                    Message = "An error occurred while fetching doctor data."
                });
            }
            
        }

        private async Task<bool> DoctorsExists(int id)
        {
            return await _unitOfWork.EfDoctorsRepository.CheckByIdAsync(w=> w.DoctorId==id); 
        }
    }
}
