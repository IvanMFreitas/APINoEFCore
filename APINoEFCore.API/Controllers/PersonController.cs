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
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            try
            {
                var person = await _personService.GetByIdAsync(id);

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
    }
}