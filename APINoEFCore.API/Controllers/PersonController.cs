using APINoEFCore.Entities.RequestModels;
using APINoEFCore.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINoEFCore.API.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("id/{id}")]
        public IActionResult GetPersonById(Guid id)
        {
            try
            {
                var person = _personService.GetById(id);

                if (person == null)
                {
                    return NotFound();
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public IActionResult GetPersonByEmail(string email)
        {
            try
            {
                var person = _personService.GetByEmail(email);

                if (person == null)
                {
                    return NotFound();
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("login/{email}/{pwd}")]
        public async Task<IActionResult> Login(string email, string pwd){
            try
            {
                var (success, message) =  _personService.Login(email, pwd);

                if (!success && message.Contains("Unauthorized")){
                    return Unauthorized(new { Message = message });
                }else if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public IActionResult CreatePerson(PersonCreateRequestModel request)
        {
            try
            {
                var (success, message) = _personService.CreatePerson(request);

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }

        [HttpPatch("update/{email}")]
        public IActionResult UpdatePerson(PersonUpdateRequestModel request, string email)
        {
            try
            {
                var (success, message) = _personService.UpdatePerson(request, email);

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }

        [HttpDelete("delete/{email}")]
        public IActionResult DeletePerson(string email)
        {
            try
            {
                var (success, message) = _personService.DeletePerson(email);

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }

        [HttpPatch("changePwd")]
        public IActionResult ChangePassword(PersonChangePwdRequestModel request)
        {
            try
            {
                var (success, message) = _personService.ChangePassword(request);

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }
    }
}