using Microsoft.AspNetCore.Mvc;

namespace Machine_Product_Service.Controllers;
using Machine_Product_Service.MachineProduct;
using Machine_Product_Service.DbContext;
using Machine_Product_Service.DescriptionAiIntergratedService;
using Machine_Product_Service.DTOS;
[ApiController]
[Route("[controller]")]
public class AddMachineController : ControllerBase
{ 
    private readonly ILogger<AddMachineController> _logger;
    private readonly DBcontext  _context;
    private readonly AiDescriptionService _aiDescriptionService;

    public AddMachineController(ILogger<AddMachineController> logger, DBcontext dbcontext, AiDescriptionService aiDescriptionService)
    {
        _logger = logger;
        _context = dbcontext;
        _aiDescriptionService = aiDescriptionService;
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
        var machineDto = new CreateMachineDto
        {
            Description =  dto.Description,
            Brand = dto.Brand,
            Model = dto.Model,
            
       };
        await _aiDescriptionService.GenerateDescriptionAsync(machineDto);
        _context.Set<Machine>().Add(machine);
        await _context.SaveChangesAsync();
        
        return Ok();
        
        
    }
    
}