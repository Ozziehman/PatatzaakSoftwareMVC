using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatatzaakSoftwareMVC.DataAccessLayer
{
    public class VoucherRepository
    {
        private readonly MainDb _context;

        public VoucherRepository(MainDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voucher>> GetVouchersAsync()
        {
            return await _context.vouchers.ToListAsync();
        }

        /// <summary>
        /// Get voucher by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Voucher> GetVoucherByIdAsync(int id)
        {
            return await _context.vouchers.FindAsync(id);
        }

        /// <summary>
        /// Add voucher to the database
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public async Task<Voucher> AddVoucherAsync(Voucher voucher)
        {
            _context.vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        /// <summary>
        /// Update voucher in the database
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public async Task<Voucher> UpdateVoucherAsync(Voucher voucher)
        {
            var voucherToUpdate = await _context.vouchers.FindAsync(voucher.Id);

            voucherToUpdate.Price = voucher.Price;
            voucherToUpdate.VoucherDiscount = voucher.VoucherDiscount;
            voucherToUpdate.VoucherCode = voucher.VoucherCode;
            voucherToUpdate.ExpiresBy = voucher.ExpiresBy;
            voucherToUpdate.UserId = voucher.UserId;

            await _context.SaveChangesAsync();
            return voucher;
        }

        /// <summary>
        /// Delete voucher by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVoucherAsync(int id)
        {
            var voucher = await _context.vouchers.FindAsync(id);

            if (voucher == null)
                return false;

            _context.vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
