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

    /// <summary>
    /// Get a single voucher by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// creates a voucher
    /// </summary>
    /// <param name="voucher"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Voucher>> PostVoucher(Voucher voucher)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newVoucher = await _voucherRepository.AddVoucherAsync(voucher);
        return CreatedAtAction(nameof(GetVoucher), new { id = newVoucher.Id }, newVoucher);
    }

    /// <summary>
    /// Updates a voucher by id 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="voucher"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Deletes a voucher by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoucher(int id)
    {
        var result = await _voucherRepository.DeleteVoucherAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
