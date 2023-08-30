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

        [HttpGet("{id}")]
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

        [HttpGet("generateToken/{email}/{pwd}")]
        public async Task<IActionResult> Login(string email, string pwd){
            try
            {
                var token = _personService.GenerateJwtToken(email, pwd);

                return Ok(token);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }
}