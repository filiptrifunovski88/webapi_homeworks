using homework02.Models;

namespace homework02
{
    public static class BooksDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book {Author = "Charlotte Bronte", Title = "Jane Eyre"},
            new Book {Author = "J.M. Barrie", Title = "Peter Pan"},
            new Book {Author = "Charles Dickens", Title = "Oliver Twist"},
            new Book {Author = "L.M. Montgomery", Title = "Anne of Green Gables"},
            new Book {Author = "Johanna Spyri", Title = "Heidi"},
            new Book {Author = "Stephen King", Title = "Carrie"}
        };
    }
}
