namespace Machine_Product_Service.MessagesEntities;
using Machine_Product_Service.UserEntity;
public class Message
{
    public string message { get; set; }
    public string ReceiverName { get; set; }
    public string MessageId { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedOn { get; set; }
    public User user { get; set; }
}