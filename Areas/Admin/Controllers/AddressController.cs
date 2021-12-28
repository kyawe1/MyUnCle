using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UncleApp.Areas.Admin.Models;
using UncleApp.Context;


namespace UncleApp.Areas.Admin.Controllers;
[Authorize(Roles ="Admin,Shopkeeper")]
[ApiController]
[Area("Admin")]
[Route("api/v1/[area]/[controller]/[action]/{id?}")]
public class AddressController : ControllerBase
{
    private DataContext _dataContext;
    public AddressController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var k=_dataContext.addresses.FirstOrDefault(p => p.Address_String == id);
        if(k != null)
        {
            _dataContext.Remove(k);
            await _dataContext.SaveChangesAsync();
            return Ok("Your address is deleted");
        }
        return NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AddressViewModel address)
    {
        //address = address.Trim();
        var address1=_dataContext.addresses.FirstOrDefault(p=>p.Address_String==address.oldaddress);
        if (address1 == null)
        {
            return NotFound();
        }
        address1.Address_String = address.newaddress;
        _dataContext.Update(address1);
        await _dataContext.SaveChangesAsync();
        return Ok("updated Successfully");
    }
}