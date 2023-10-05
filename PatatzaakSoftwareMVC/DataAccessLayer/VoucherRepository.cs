﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Voucher> GetVoucherByIdAsync(int id)
        {
            return await _context.vouchers.FindAsync(id);
        }

        public async Task<Voucher> AddVoucherAsync(Voucher voucher)
        {
            _context.vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        public async Task<Voucher> UpdateVoucherAsync(Voucher voucher)
        {
            _context.Entry(voucher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return voucher;
        }

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
