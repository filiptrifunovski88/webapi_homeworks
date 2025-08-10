using homework02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homework02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet] // https://localhost:[port]/api/books
        public ActionResult<List<Book>> GetAllBooks()
        {
            return StatusCode(StatusCodes.Status200OK, BooksDb.Books);
        }

        [HttpGet("withindex")] // https://localhost:[port]/api/books/withindex?index=1
        public ActionResult<Book> GetBookByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("Index must be bigger than 0");
                }
                if (index >= BooksDb.Books.Count)
                {
                    return NotFound($"There is no book with index: {index}");
                }
                return Ok(BooksDb.Books[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("filter")] // https://localhost:[port]/api/books/filter?
        public ActionResult<Book> FilterByAuthorAndTitle(string? author, string? title)
        {
            try
            {
                if (author is null && title is null)
                {
                    return BadRequest("There is no such book");
                }

                if (author is null)
                {
                    // https://localhost:[port]/api/books/filter?title=heidi
                    List<Book> book = BooksDb.Books.Where(b => b.Title.ToLower() == title.ToLower()).ToList();
                    return Ok(book);
                }

                if (title is null)
                {
                    //https://localhost:[port]/api/books/filter?author=charlotte+bronte
                    List<Book> book = BooksDb.Books.Where(b => b.Author.ToLower() == author.ToLower()).ToList();
                    return Ok(book);
                }

                if (BooksDb.Books.Any(b => b.Author.ToLower() == author.ToLower()) && !BooksDb.Books.Any(b => b.Title.ToLower() == title.ToLower()))
                {
                    //https://localhost:[port]/api/books/filter?author=charles+dickens&title=heidi
                    return NotFound("Author exists but title does not match.");
                }

                if (BooksDb.Books.Any(b => b.Title.ToLower() == title.ToLower()) && !BooksDb.Books.Any(b => b.Author.ToLower() == author.ToLower()))
                {
                    //https://localhost:[port]/api/books/filter?author=filip&title=heidi
                    return NotFound("Title exists but Author does not match.");
                }
                //https://localhost:[port]/api/books/filter?author=stephen+king&title=carrie
                List<Book> fiilterBook = BooksDb.Books.Where(b => b.Title.ToLower() == title.ToLower() && b.Author.ToLower() == author.ToLower()).ToList();
                return Ok(fiilterBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost] //https://localhost:[port]/api/books
        // {
        // "author": "Filip Trifunovski",
        // "title": "Knigata od Filip"
        // }
        public ActionResult<Book> AddNewBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("You must have author and title");
                }
                BooksDb.Books.Add(book);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // BONUS
        [HttpPost("addlist")] //https://localhost:[port]/api/books/addlist
        //    [
        //    {
        //        "author": "Filip",
        //        "title": "Kniga Eden"
        //    },
        //    {
        //        "author": "Stefan",
        //        "title": "Kniga Dva"
        //    },
        //    {
        //    "author": "Matej",
        //    "title": "Kniga Tri"
        //    }
        //    ]
        public IActionResult AddListOfBooks([FromBody] List<Book> books)
        {
            try
            {
                if(books is null || books.Count == 0)
                {
                    return BadRequest("You must enter minimum one book");
                }
                foreach(Book book in books)
                {
                    if(string.IsNullOrEmpty(book.Author) || (string.IsNullOrEmpty(book.Title)))
                    {
                        BadRequest("Book must have author and title");
                    }
                    BooksDb.Books.Add(book);
                }
                return Created();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
