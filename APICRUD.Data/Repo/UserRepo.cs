using APICRUD.Data.Context;
using APICRUD.Data.IRepo;
using APICRUD.Data.Models;

namespace APICRUD.Data.Repo;

public class UserRepo : IUserRepo
{
    private readonly APICRUDContext _context;

    public UserRepo(APICRUDContext context)
    {
        _context = context;
    }


    public User User(string email)
    {
        return _context.Users.FirstOrDefault(b => b.Email == email);
    }
}
