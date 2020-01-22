using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services;

namespace StudentsAPI.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class StudentsV2Controller : ControllerBase
    {
        private readonly IStudentsV2Service studentsService;

        public StudentsV2Controller(IStudentsV2Service studentsService)
        {
            this.studentsService = studentsService;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<StudentV2> Get([FromQuery] Filter filter)
        {
            return studentsService.Get(filter).ToList();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<StudentV2> Get(long id)
        {
            try
            {
                return studentsService.Get().Single(s => s.Id == id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult Post([FromBody] Models.StudentV2 student)
        {
            if (studentsService.Get().Any(s => s.Id == student.Id))
            {
                studentsService.Update(student);
                return Ok(student);
            }

            studentsService.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] StudentV2 student)
        {
            if (student.Id != id)
            {
                return BadRequest();
            }
            if (!studentsService.Get().Any(s => s.Id == student.Id))
            {
                return NotFound();
            }

            studentsService.Update(student);
            return Ok(student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            if (!studentsService.Get().Any(s => s.Id == id))
            {
                return NotFound();
            }

            studentsService.Delete(id);
            return Ok();
        }
    }
}