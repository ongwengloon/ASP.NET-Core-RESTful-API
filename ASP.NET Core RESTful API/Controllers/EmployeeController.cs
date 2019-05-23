﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_RESTful_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace ASP.NET_Core_RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;

            if (_context.EmployeeItems.Count() == 0)
            {
                // Create a new EmployeeItem if collection is empty, which means you can't delete all Employee.
                _context.EmployeeItems.Add(new Employee { FirstName = "Sky" });
                _context.SaveChanges();
            }
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeItems()
        {
            return await _context.EmployeeItems.ToListAsync();
        }

        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeItem(long id)
        {
            var item = await _context.EmployeeItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            
            return item;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployeeItem(Employee item)
        {
            _context.EmployeeItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeItem), new { id = item.Id }, item);
        }

        // PUT: api/Employee/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> PutEmployeeItem(long id, Employee item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeItem(long id)
        {
            var item = await _context.EmployeeItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.EmployeeItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Sample PATCH JSON
        /// [
	    ///     {"op" : "test", "path": "id", "value": "1"}, //To validate is the id is '1', else request will fail
	    ///     {"op" : "move", "from": "firstName", "path": "lastName" }, //move value from 1 property to another property (@from become null)
	    ///     {"op" : "copy", "from": "lastName", "path": "firstName" }, //copy value from 1 property to another property (@from value remain)
	    ///     {"op" : "remove", "path": "lastName"}, //set the property's value to null
	    ///     {"op" : "add", "path" : "lastName", "value" : "Ong"}, //Add if empty or update if contain value
	    ///     {"op" : "replace", "path" : "lastName", "value" : "Ong Weng Loon"} //Replaces a value. Equivalent to a “remove” followed by an “add”.
        /// ]
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemPatch"></param>
        /// <returns></returns>
        // PATCH: api/Employee/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Employee>> PatchEmployeeItem(long id, [FromBody]JsonPatchDocument<Employee> itemPatch)
        {
            var item = await _context.EmployeeItems.FindAsync(id);

            itemPatch.ApplyTo(item);
            _context.Update(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }
    }
}
