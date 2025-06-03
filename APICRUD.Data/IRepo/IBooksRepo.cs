using APICRUD.Data.Models;

namespace APICRUD.Data.IRepo;

public interface IBooksRepo
{
    public IEnumerable<Book> Books();
    public bool AddBook(Book book);
    public bool UpdateBook(Book book);
    public Book Book(int id);




}
