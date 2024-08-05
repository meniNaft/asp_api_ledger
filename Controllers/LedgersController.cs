using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_api_ledger.DAL;
using asp_api_ledger.Models;

namespace asp_api_ledger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly DataContext _context;

        public LedgersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Ledgers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ledger>>> GetLedgers()
        {
            return await _context.Ledgers.Include(l => l.Users).ToListAsync();
        }

        // GET: api/Ledgers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ledger>> GetLedger(int id)
        {
            var ledger = await _context.Ledgers.FindAsync(id);

            if (ledger == null)
            {
                return NotFound();
            }

            return ledger;
        }

        // PUT: api/Ledgers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLedger(int id, Ledger ledger)
        {
            if (id != ledger.Id)
            {
                return BadRequest();
            }

            _context.Entry(ledger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LedgerExists(id))
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

        // POST: api/Ledgers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ledger>> PostLedger(Ledger ledger)
        {
            _context.Ledgers.Add(ledger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLedger", new { id = ledger.Id }, ledger);
        }

        // DELETE: api/Ledgers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLedger(int id)
        {
            var ledger = await _context.Ledgers.FindAsync(id);
            if (ledger == null)
            {
                return NotFound();
            }

            _context.Ledgers.Remove(ledger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut, Route("add-user")]
        public async Task<IActionResult> AddUserToLegder(int legderId, int UserId)
        {
            User user = await _context.Users.FirstAsync(u => u.Id == UserId);
            Ledger ledger = await _context.Ledgers.Include(l => l.Users).FirstAsync(l => l.Id == legderId);
            if(user != null && ledger != null)
            {
                ledger.Users.Add(user);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut, Route("remove-user")]
        public async Task<IActionResult> RemoveUserToLegder(int legderId, int UserId) 
        {
            User user = await _context.Users.FirstAsync(u => u.Id == UserId);
            Ledger ledger = await _context.Ledgers.Include(l => l.Users).FirstAsync(l => l.Id == legderId);
            if (user != null && ledger != null)
            {
                ledger.Users.Remove(user);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool LedgerExists(int id)
        {
            return _context.Ledgers.Any(e => e.Id == id);
        }
    }
}
