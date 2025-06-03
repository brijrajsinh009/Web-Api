using APICRUD.Data.CustomModels;

namespace APICRUD.Services.IServices;

public interface IAPICRUDService
{
    public IEnumerable<BookViewModel> GetAllBooks();

    public (bool, int, string) AddBook(BookViewModel book);
    public (bool, string) DeleteBook(int id);
    public (bool, int, string) UpdateBook(BookViewModel book);
    public (bool, string) Authenticate(LoginDetails model);




}
