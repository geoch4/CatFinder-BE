using InfrastructureLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        //// GET: api/<EmailController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<EmailController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<EmailController>
        [HttpPost("verification")]
        public async Task<IActionResult> SendEmail([FromServices] IEmailService emailService)
        {
            await emailService.SendAsync(
                "lilnz4@hotmail.com",
                "Hello from CatFinder!",
                "<p>Hello! Resend is working 😸</p>"
                );

            return Ok("Email sent");
        }

        // PUT api/<EmailController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmailController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
