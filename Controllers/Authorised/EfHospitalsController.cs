using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleTaskApp.Models;
using SampleTaskApp.UnitOfWork;
using SampleTaskApp.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SampleTaskApp.Controllers.Authorised
{
    [Route(ApiUrls.BaseAPI + "/[controller]")]
    [ApiController]
   
    public class EfHospitalsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EfHospitalsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: rk/api/Hospitals
        [HttpGet]
        [CustomAuthorize]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            try
            {
                var data = await _unitOfWork.EfHospitalsRepository.GetAllAsync();
                var rType = new CommonOperation { Type = 6, Data = data,Status = StatusCodes.Status200OK };
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

        // GET: rk/api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospitals(int id)
        {
            try
            {
                var data = await _unitOfWork.EfHospitalsRepository.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
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

        // PUT: rk/api/Hospitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitals(int id, Hospital hospitals)
        {
            try
            {
                var data = await _unitOfWork.EfHospitalsRepository.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                data.HospitalName = hospitals.HospitalName;
                data.HospitalLocation = hospitals.HospitalLocation;

                await _unitOfWork.EfHospitalsRepository.UpdateAsync(data);
                await _unitOfWork.CompleteAsync();
                var rType = new CommonOperation { Type = 2, Data = data, Status = StatusCodes.Status200OK };
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

        // POST: rk/api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospitals(Hospital hospitals)
        {
            try
            {
                await _unitOfWork.EfHospitalsRepository.AddAsync(hospitals);
                await _unitOfWork.CompleteAsync();
                var rType = new CommonOperation { Type = 1,  Status = StatusCodes.Status200OK };
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

        // DELETE: rk/api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitals(int id)
        {
            try
            {
                await _unitOfWork.EfHospitalsRepository.DeleteAsync(id);
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
                    Message = "An error occurred while fetching hospital data."
                });
            }
        }
    }
}
