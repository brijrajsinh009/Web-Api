using APICRUD.Data.CustomModels;
using APICRUD.Services.Attributes;
using APICRUD.Services.IServices;
using APICRUD.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class APICRUDController : ControllerBase
{

    private readonly IAPICRUDService _apiCRUDService;
    private readonly ITokenService _tokenService;

    public APICRUDController(IAPICRUDService apiCRUDService, ITokenService tokenService)
    {
        _apiCRUDService = apiCRUDService;
        _tokenService = tokenService;
    }


    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Login([FromBody] LoginDetails model)
    {
        bool isAuth;
        string message;
        (isAuth, message) = _apiCRUDService.Authenticate(model);
        if (!isAuth)
        {
            return BadRequest(new { Message = message });
        }
        _tokenService.SaveJWTToken(Response, message);
        _tokenService.SaveRefreshJWTToken(Response, _tokenService.GenerateRefreshToken(model.UserEmail));
        return Ok(new { Message = "Logged in" });
    }



    [HttpGet("books", Name = "GetBooks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [CutomAuth]
    public IActionResult GetBooks()
    {
        IEnumerable<BookViewModel> books = _apiCRUDService.GetAllBooks();
        if (books.Count() == 0)
        {
            return BadRequest();
        }
        Console.WriteLine(_tokenService.GenerateJwtToken("Jadeja", "abc@gmail.com", 15));
        return Ok(books);
    }


    [HttpPost("addBook", Name = "AddBook")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [CutomAuth]
    public IActionResult AddBook([FromBody] BookViewModel newBook)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (newBook.Id > 0)
        {
            return BadRequest();
        }
        bool isAdded;
        int id;
        string message;
        (isAdded, id, message) = _apiCRUDService.AddBook(newBook);
        return Ok(new { Id = id, Message = message });
    }


    [HttpDelete("deleteBook", Name = "DeleteBook")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBook(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        bool isDeleted;
        string message;
        (isDeleted, message) = _apiCRUDService.DeleteBook(id);
        if (!isDeleted)
        {
            return BadRequest(new { Id = id, Message = message });
        }
        return Ok(new { Id = id, Message = message });
    }


    [HttpPost("updateBook", Name = "UpdateBook")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateBook([FromBody] BookViewModel book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (book.Id == 0)
        {
            return BadRequest();
        }
        bool isUpdate;
        int id;
        string message;
        (isUpdate, id, message) = _apiCRUDService.UpdateBook(book);
        if (!isUpdate)
        {
            return BadRequest(new { Id = id, Message = message });
        }
        return Ok(new { Id = id, Message = message });
    }




}
