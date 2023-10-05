using Microsoft.AspNetCore.Mvc;
using PatatzaakSoftwareMVC.DataAccessLayer;
using PatatzaakSoftwareMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class VoucherController : ControllerBase
{
    private readonly VoucherRepository _voucherRepository;

    public VoucherController(VoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Voucher>>> GetVouchers()
    {
        var vouchers = await _voucherRepository.GetVouchersAsync();
        return Ok(vouchers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Voucher>> GetVoucher(int id)
    {
        var voucher = await _voucherRepository.GetVoucherByIdAsync(id);

        if (voucher == null)
        {
            return NotFound();
        }

        return Ok(voucher);
    }

    //POST, PUT and DELETE have been disabled as this API is not intended to be used for CRUD operations just for reading data.

   /* [HttpPost]
    public async Task<ActionResult<Voucher>> PostVoucher(Voucher voucher)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newVoucher = await _voucherRepository.AddVoucherAsync(voucher);
        return CreatedAtAction(nameof(GetVoucher), new { id = newVoucher.Id }, newVoucher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVoucher(int id, Voucher voucher)
    {
        if (id != voucher.Id)
        {
            return BadRequest();
        }

        var existingVoucher = await _voucherRepository.GetVoucherByIdAsync(id);

        if (existingVoucher == null)
        {
            return NotFound();
        }

        var updatedVoucher = await _voucherRepository.UpdateVoucherAsync(voucher);

        if (updatedVoucher == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoucher(int id)
    {
        var result = await _voucherRepository.DeleteVoucherAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }*/
}
