using System.ComponentModel.DataAnnotations;

namespace Machine_Product_Service.User;
using Machine_Product_Service.MachineProduct;
public class User
{
    public int  UserId  { get; set; }
    [Required]
    public string UserName  { get; set; }
    public string Password { get; set; }
    public ICollection<Machine> Products { get; set; }
}