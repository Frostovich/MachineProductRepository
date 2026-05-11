using Microsoft.AspNetCore.Mvc;

namespace Machine_Product_Service.Controllers;
using Machine_Product_Service.MachineProduct;
using Machine_Product_Service.DbContext;
[ApiController]
[Route("[controller]")]
public class AddMachineController : ControllerBase
{ 
    private readonly ILogger<AddMachineController> _logger;
    private readonly DBcontext  _context;

    public AddMachineController(ILogger<AddMachineController> logger, DBcontext dbcontext)
    {
        _logger = logger;
        _context = dbcontext;
    }

    [HttpPost("/AddMachine")]
    [Consumes("multipart/form-data")]
    public async  Task<IActionResult> AddMachine([FromBody] CreateMachineDto dto )
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        // Генерируем уникальное имя на основе тиков + случайное число
        var uniqueNumber = DateTime.UtcNow.Ticks % int.MaxValue;
        var fileName = $"{uniqueNumber}{Path.GetExtension(dto.Image.FileName)}";
        var filePath = Path.Combine("wwwroot/images", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
            await dto.Image.CopyToAsync(stream);

        var machine = new Machine
        {
            MachineRun = dto.MachineRun,
            MachineDescription =  dto.Description,
            MachineName = dto.Brand,
            MachineModel = dto.Model,
            MachineYear = dto.Year,
            MachineGuid = uniqueNumber   // сохраняем int в БД
        };

        _context.Set<Machine>().Add(machine);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
}
public class CreateMachineDto
{
    public int MachineRun { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public IFormFile Image { get; set; }
}