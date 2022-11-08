using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBS_API.Models;

namespace EBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly EBSContext _context;

        public BillsController(EBSContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            return await _context.Bills.ToListAsync();
        }

        // GET: api/Bills/5
       
 /*       public async Task<ActionResult<Bill>> GetBill(decimal id)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }*/
        [HttpGet("{id}")]
        public  List<Bill> BillDetails(decimal id)
        {

            var bill = (from o in _context.Bills where o.CustomerId == id
                        
                        select new Bill()
                        {
                            CustomerId = o.CustomerId,
                            CustomerName = o.CustomerName,
                            BillId = o.BillId,
                            BillGenDate = o.BillGenDate,
                            BillDueDate = o.BillDueDate,
                            CustomerMobile = o.CustomerMobile,
                            CustomerAddress = o.CustomerAddress,
                            PerUnitCost = o.PerUnitCost,
                            TotalUnits = o.TotalUnits,
                            BillAmount = o.BillAmount
                        });

            List<Bill> bills = bill.ToList();
            return bills;
            /* var bill = (from a in _context.Customers
                         where a.CustomerId == id
                         select new Bill()
                         {
                             CustomerId = a.CustomerId
                         });
             List<Bill> bills = bill.ToList();
             decimal cno;
             foreach (var b in bills)
             {
                 cno = b.CustomerId;

                 var res = from o in _context.Bills
                           where o.CustomerId == cno
                           select new Bill()
                           {
                               CustomerId = o.CustomerId,
                               CustomerName = o.CustomerName,
                               BillId = o.BillId,
                               BillGenDate = o.BillGenDate,
                               BillDueDate = o.BillDueDate,
                               CustomerMobile = o.CustomerMobile,
                               CustomerAddress = o.CustomerAddress,
                               PerUnitCost = o.PerUnitCost,
                               TotalUnits = o.TotalUnits,
                               BillAmount = o.BillAmount
                           };
                 List<Bill> query = res.ToList();
                 return query;
             }
             return bills;*/

        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(decimal id, Bill bill)
        {
            if (id != bill.BillId)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.BillId }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(decimal id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillExists(decimal id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
