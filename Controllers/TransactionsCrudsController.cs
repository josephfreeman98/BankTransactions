using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankTransactions.Models;

namespace BankTransactions.Controllers {
    public class TransactionsCrudsController : Controller {
        private readonly TransactionDbContext _context;

        public TransactionsCrudsController(TransactionDbContext context) {
            _context = context;
        }

        // GET: TransactionsCruds
        public async Task<IActionResult> Index() {
            return _context.Transactions != null ?
                        View(await _context.Transactions.ToListAsync()) :
                        Problem("Entity set 'TransactionDbContext.Transactions'  is null.");
        }



        // GET: TransactionsCruds/AddOrEdit
        public IActionResult AddOrEdit(int id = 0) {

            if (id == 0)
                return View(new TransactionCrud());
            else
                return View(_context.Transactions.Find(id));

        }

        // POST: TransactionsCruds/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionCrud transactionCrud) {
            if (ModelState.IsValid) {
                if (transactionCrud.TransactionId == 0) {
                    transactionCrud.Date = DateTime.Now;
                    _context.Add(transactionCrud);

                } else {
                    _context.Update(transactionCrud);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionCrud);
        }





        // POST: TransactionsCruds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Transactions == null) {
                return Problem("Entity set 'TransactionDbContext.Transactions'  is null.");
            }
            var transactionCrud = await _context.Transactions.FindAsync(id);
            if (transactionCrud != null) {
                _context.Transactions.Remove(transactionCrud);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
