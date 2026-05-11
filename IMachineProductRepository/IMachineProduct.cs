
using Microsoft.EntityFrameworkCore;

namespace Machine_Product_Service.IMachineProductRepository;
using Machine_Product_Service.MachineProduct;
using Machine_Product_Service.DbContext;
public interface IMachineProduct
{
    Task<Machine> AddMachine(string machineName, string machineDescription);
    Task<Machine> GetMachine(string machineName);
    void Delete(Machine machineId);
}

public class MachineRepository : IMachineProduct
{
    private readonly DBcontext _dbcontext;
    public MachineRepository(DBcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Machine> AddMachine(string machineName, string machineDescription)
    {
        var machine = new Machine
        {
            MachineName = machineName,
            MachineDescription = machineDescription,
        };
        
        await _dbcontext.Set<Machine>().AddAsync(machine);
        await _dbcontext.SaveChangesAsync();
        return machine;
    }

    public async Task<Machine> GetMachine(string machineName)
    {
        var machine = await _dbcontext.Set<Machine>().FirstOrDefaultAsync(u=>u.MachineName == machineName);
        await  _dbcontext.SaveChangesAsync();
        return machine;
    }

    public void Delete(Machine machine)
    {
        _dbcontext.Set<Machine>().Remove(machine);
        _dbcontext.SaveChanges();
    }

}