using APICRUD.Data.Models;

namespace APICRUD.Data.IRepo;

public interface IUserRepo
{
    public User User(string email);
}
