using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Machine_Product_Service.IUserRepository;
using Machine_Product_Service.User;
using Machine_Product_Service.DbContext;
public interface IUserRepository
{
    public Task<User> AddUser(string username, string password);
    public Task<User> GetUser(string username);
    void Delete(User user);
}


class UserRepository : IUserRepository
{
    private readonly DBcontext _dbcontext;
    public UserRepository(DBcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<User> AddUser(string username, string password)
    {
        var user = new User {Password = password,
            UserName =  username };
        await _dbcontext.AddAsync(user);
        await _dbcontext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUser(string username)
    {
        var user = await _dbcontext.Set<User>().FirstOrDefaultAsync(u=> u.UserName == username);
        await _dbcontext.SaveChangesAsync();
        return user;
    }

    public void Delete(User user)
    {
        _dbcontext.Remove(user);
        _dbcontext.SaveChanges();
    }
    
}