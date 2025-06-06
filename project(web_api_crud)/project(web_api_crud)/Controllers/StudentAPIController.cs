﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_web_api_crud_.Data;
using project_web_api_crud_.Models;

namespace project_web_api_crud_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly CrudApiDbContext _context;
        public StudentAPIController(CrudApiDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Student>>>  GetStudent()
        {
            var data = await _context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student s)
        {
            
            await _context.Students.AddAsync(s);
            await _context.SaveChangesAsync();
            return Ok(s);
        }


        // GET: api/student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var mystudent = await _context.Students.FindAsync(id);
            if (mystudent == null)
            {
                return NotFound();
            }
            _context.Students.Remove(mystudent);
            await _context.SaveChangesAsync();
            return Ok();


        }
    }
}
