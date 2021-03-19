using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Lab 5
 * Variant 1
 * 
 * Розробити структуру даних для зберігання інформації про книги в бібліотеці. 
 * Книга характеризується: 
 *       назвою 
 *      ,прізвищем автора 
 *      ,вартістю 
 *      ,датою видання 
 *      ,видавництвом 
 *      ,списком інвентарних номерів (книга в кількох примірниках). 
 * У одного автора може бути декілька книг.
 */

// LINQ to Objects

namespace lab5
{
    class Book
    {
        public int ID { get; }
        public int AuthorID { get; }
        public int PublisherID { get; }
        public string Title { get; }
        public DateTime DatePublished { get; }
        public float Price { get; }
        public List<int> InventoryID { get; }

        public Book(int id, int authorID, int publisherID, string title, DateTime datePublished, float price, List<int> inventoryID)
        {
            ID = id;
            AuthorID = authorID;
            PublisherID = publisherID;
            Title = title;
            DatePublished = datePublished;
            Price = price;
            InventoryID = inventoryID;
        }

    }

    class Author
    {
        public int ID { get; }
        public string Name { get; }
        public List<int> Books { get; }

        public Author(int id, string name, List<int> books)
        {
            ID = id;
            Name = name;
            Books = books;
        }
    }

    class Coauthor
    {
        public int bookID;
        public int coauthorID;
        public Coauthor(int b, int c)
        {
            bookID = b;
            coauthorID = c;
        }
    }


    class Publisher
    {
        public int ID { get; }
        public string Name { get; }
        public string Address { get; }

        public Publisher(int id, string name, string address)
        {
            ID = id;
            Name = name;
            Address = address;
        }
    }

    class Program
    {
        static List<Book> bookGenre1 = new List<Book>()
        {
             new Book(1,1,1,"book1",new DateTime(2021, 3, 20),15,new List<int>() {3,4,10,22})
            ,new Book(2,1,2,"book2",DateTime.Now,10,new List<int>() {1,2})
            ,new Book(3,2,1,"book3",DateTime.Now,12,new List<int>() {5,6,7})
        };

        static List<Book> bookGenre2 = new List<Book>()
        {
             new Book(3,2,1,"book3",DateTime.Now,12,new List<int>() {5,6,7})
            ,new Book(4,3,2,"book4",DateTime.Now,20,new List<int>() {12,14})
            ,new Book(5,1,2,"book5",DateTime.Now,6,new List<int>() {8,9,13})
        };

        static List<Author> authors = new List<Author>()
        {
             new Author(1,"author1",new List<int>() {1,2})
            ,new Author(2,"author2",new List<int>() {3})
            ,new Author(3,"author3",new List<int>() {3})
        };

        static List<Coauthor> coauthors = new List<Coauthor>()
        {
             new Coauthor(1, 1)
            ,new Coauthor(1, 2)
            ,new Coauthor(1, 3)
            ,new Coauthor(2, 3)
            ,new Coauthor(2, 2)
            ,new Coauthor(2, 1)
        };
        

        static List<Publisher> publishers = new List<Publisher>()
        {
             new Publisher(1,"publisher1","address1")
            ,new Publisher(2,"publisher2","address2")
        };

        static void Main(string[] args)
        {
            Console.WriteLine("\nProjection - select");
            var std1 = from b in bookGenre1
                       select b.Title;
            foreach (var title in std1)
                Console.WriteLine(title);

            var ext1 = bookGenre1.Select(b => b.Price);
            foreach (var price in ext1)
                Console.WriteLine($"${price}");


            Console.WriteLine("\nCondition - where");
            var std2 = from b in bookGenre1
                       where b.PublisherID % 2 == 0
                       select new { Title = b.Title, Publisher = b.PublisherID };
            foreach(var book in std2)
                Console.WriteLine($"{book.Title} - {book.Publisher}");

            var ext2 = bookGenre1.Where(b => b.DatePublished != DateTime.Parse("2021, 3, 20"))
                                .Select(b => new { Title = b.Title, Date = b.DatePublished });
            foreach (var book in ext2)
                Console.WriteLine($"{book.Title} - {book.Date}");


            Console.WriteLine("\nGrouping - group by");
            var std3 = from b in bookGenre1
                       group b by b.PublisherID;
            foreach(var b in std3)
            {
                Console.WriteLine("\nPublisherID:" + b.Key);
                foreach(var p in b)
                    Console.WriteLine(p.Title);
            }

            var ext3 = bookGenre1.GroupBy(b => b.AuthorID);
            foreach (var b in ext3)
            {
                Console.WriteLine("\nAuthorID:" + b.Key);
                foreach (var a in b)
                    Console.WriteLine(a.Title);
            }


            Console.WriteLine("\nSorting - orderby");
            var std4 = from b in bookGenre1
                       orderby b.Title descending
                       select b.Title;
            foreach(var title in std4)
                Console.WriteLine(title);

            var ext4 = bookGenre1.OrderBy(b => b.Price).Select(b=>b.Price);
            foreach (var price in ext4)
                Console.WriteLine(price);


            Console.WriteLine("\nAggregation - Count");
            var count = bookGenre1.Count();
                Console.WriteLine(count);


            Console.WriteLine("\nAggregation - Sum");
            var sum = bookGenre1.Sum(b => b.Price);
                Console.WriteLine(sum);


            Console.WriteLine("\nAggregation - Min");
            var min = bookGenre1.Min(b => b.Price);
                Console.WriteLine(min);


            Console.WriteLine("\nAggregation - Max");
            var max = bookGenre1.Max(b => b.Price);
                Console.WriteLine(max);


            Console.WriteLine("\nAggregation - Average");
            var avg = bookGenre1.Average(b => b.Price);
                Console.WriteLine(avg);


            Console.WriteLine("\nOne to many - SelectMany"); 
            var oneToMany = bookGenre1.SelectMany
                (b => b.InventoryID,
                (b, i) => new { BookTitle = b.Title, InventoryID = i });
            foreach(var book in oneToMany)
                Console.WriteLine($"{book.BookTitle} - {book.InventoryID}");


            Console.WriteLine("\nMany to many - join");
            var manyToMany = from b in bookGenre1
                       join c in coauthors on b.ID equals c.bookID into temp1
                       from t1 in temp1
                       join a in authors on t1.coauthorID equals a.ID into temp2
                       from t2 in temp2
                       select new { Title = b.Title, Author = t2.Name };
            foreach (var book in manyToMany)
                Console.WriteLine($"{book.Title} - {book.Author}");
                
        }
    }
}
