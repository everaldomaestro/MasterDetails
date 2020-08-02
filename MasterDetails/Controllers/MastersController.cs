using MasterDetails.Data;
using MasterDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDetails.Controllers
{
    public class MastersController : Controller
    {
        private readonly Context _context;

        public MastersController(Context context)
        {
            _context = context;
        }

        // GET: Masters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Masters.ToListAsync());
        }

        // GET: Masters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterId == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // GET: Masters/Create
        public IActionResult Create()
        {
            ViewData["Produtos"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: Masters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Master master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Produtos"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View(master);
        }

        // GET: Masters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters.Include(x => x.Details).ThenInclude(x => x.Produto).FirstOrDefaultAsync(x => x.MasterId == id);
            if (master == null)
            {
                return NotFound();
            }

            ViewData["Produtos"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View(master);
        }

        // POST: Masters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Master master)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Details.RemoveRange(_context.Details.Where(x => x.MasterId == master.MasterId));
                    _context.Masters.Update(master);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterExists(master.MasterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Produtos"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View(master);
        }

        // GET: Masters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.MasterId == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // POST: Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var master = await _context.Masters.FindAsync(id);
            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterExists(int id)
        {
            return _context.Masters.Any(e => e.MasterId == id);
        }

        public JsonResult AppendProdutos(int produtoId)
        {
            var produto = _context.Produtos.Find(produtoId);
            return Json(produto);
        }
    }
}
