using Microsoft.AspNetCore.Mvc;
using UncleApp.Context;
using UncleApp.Areas.Admin.Models;
using UncleApp.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace UncleApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]/{id?}")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DataContext _dataContext { get; }
        public ProductController(DataContext datacontext)
        {
            _dataContext = datacontext;
        }
        [Route("api/v1/[area]/[controller]/")]
        [HttpGet]
        public IActionResult Index()
        {
            var q = _dataContext.dumblingTypes.Select(p => new ProductViewModel() { Name = p.Name, Created = p.Created, price = p.Price, Updated = p.Updated }).ToList();
            return Ok(new { message = "All Itmes", count = q.Count, data = q });
        }
        [HttpGet]
        public IActionResult Detail(long id)
        {
            var q = _dataContext.dumblingTypes.FirstOrDefault(p => p.Id == id);
            if (q == null)
            {
                return NotFound();
            }
            var temp=new ProductViewModel() { Name = q.Name, Description = q.Description, Created=q.Created, price = q.Price };
            return Ok(new { data=temp });
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(new String[] {
            "Description",
            "price",
            "Name"
})]ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var p = new DumblingType()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.price
            };
            _dataContext.Add(p);
            await _dataContext.SaveChangesAsync();
            return Created("created", new { message = "This product is created" });
        }
        [HttpPut]
        public async Task<IActionResult> Update(long id, [Bind(new String[] {
            "Description",
            "price",
            "Name"
})]ProductCreateViewModel model)
        {
            var temp = _dataContext.dumblingTypes.FirstOrDefault(p => p.Id == id);
            if (temp == null)
            {
                return BadRequest("No Found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            temp.Name = model.Name;
            temp.Price = model.price;
            temp.Description = model.Description;
            temp.Updated = DateTime.UtcNow;

            _dataContext.Update(temp);
            await _dataContext.SaveChangesAsync();
            return Ok(new { messgae = "This product is updated" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            DumblingType b = _dataContext.dumblingTypes.FirstOrDefault(p => p.Id == id);
            if (b == null)
            {
                return NotFound();
            }
            _dataContext.Remove(b);
            await _dataContext.SaveChangesAsync();
            return Ok("That product is not suspended. Force Deleted");
        }
    }
}

