namespace Machine_Product_Service.MachineProduct;
using Machine_Product_Service.User;
public class Machine
{
    public long MachineGuid
    { get; set; }
    
    public int MachineId 
    { get; set; }
    
    public string MachineName 
    { get; set; }
    
    public string MachineModel 
    { get; set; }
    
    public string MachineDescription 
    { get; set; }
    
    public int  MachineRun 
    { get; set; }
    
    public int MachineYear { get; set; }
    
    public User user 
    { get; set; }
}