using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PilotTest.API.Data;
using PilotTest.API.Model;

namespace PilotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<List<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }
    }
}
