using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBS_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly EBSContext _context;
        Customer cus = new Customer();
        private readonly IMapper _autoMapper;

        public CustomersController(EBSContext context, IMapper mapper)
        {
            _context = context;
            _autoMapper = mapper;

        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(decimal id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(decimal id, CustomerDto customer)
        {
            var userProfile = await _context.Customers.FindAsync(id);
/*            if (id != cus.CustomerId)
            {
                return BadRequest();
            }
            else
            {
*/
                try
                {
                   
                    if (userProfile == null)
                    {
                        return NotFound();
                    }


                    _autoMapper.Map(customer, userProfile);
                    _context.Entry(userProfile).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customer)
        {
            var user = _autoMapper.Map<Customer>(customer);
            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = user.CustomerId }, user);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(decimal id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(decimal id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
