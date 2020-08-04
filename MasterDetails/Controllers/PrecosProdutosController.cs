using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasterDetails.Data;
using MasterDetails.Models;

namespace MasterDetails.Controllers
{
    public class PrecosProdutosController : Controller
    {
        private readonly Context _context;

        public PrecosProdutosController(Context context)
        {
            _context = context;
        }

        // GET: PrecosProdutos
        public async Task<IActionResult> Index()
        {
            var context = _context.PrecosProdutos.Include(p => p.Produto);
            return View(await context.ToListAsync());
        }

        // GET: PrecosProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoProduto = await _context.PrecosProdutos
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.PrecoProdutoId == id);
            if (precoProduto == null)
            {
                return NotFound();
            }

            return View(precoProduto);
        }

        // GET: PrecosProdutos/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: PrecosProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrecoProdutoId,ProdutoId,Preco,InicioValidade,FinalValidade")] PrecoProduto precoProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(precoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", precoProduto.ProdutoId);
            return View(precoProduto);
        }

        // GET: PrecosProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoProduto = await _context.PrecosProdutos.FindAsync(id);
            if (precoProduto == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", precoProduto.ProdutoId);
            return View(precoProduto);
        }

        // POST: PrecosProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrecoProdutoId,ProdutoId,Preco,InicioValidade,FinalValidade")] PrecoProduto precoProduto)
        {
            if (id != precoProduto.PrecoProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precoProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecoProdutoExists(precoProduto.PrecoProdutoId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", precoProduto.ProdutoId);
            return View(precoProduto);
        }

        // GET: PrecosProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precoProduto = await _context.PrecosProdutos
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.PrecoProdutoId == id);
            if (precoProduto == null)
            {
                return NotFound();
            }

            return View(precoProduto);
        }

        // POST: PrecosProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var precoProduto = await _context.PrecosProdutos.FindAsync(id);
            _context.PrecosProdutos.Remove(precoProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecoProdutoExists(int id)
        {
            return _context.PrecosProdutos.Any(e => e.PrecoProdutoId == id);
        }
    }
}
