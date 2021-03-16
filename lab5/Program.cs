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
        static List<Book> books = new List<Book>()
        {
             new Book(1,1,1,"book1",new DateTime(2021, 3, 20),10,new List<int>() {3,4,10,22})
            ,new Book(2,1,2,"book2",DateTime.Now,15,new List<int>() {1,2})
            ,new Book(3,2,1,"book3",DateTime.Now,12,new List<int>() {5,6,7})
        };

        static List<Author> authors = new List<Author>()
        {
             new Author(1,"author1",new List<int>() {1,2})
            ,new Author(2,"author2",new List<int>() {3})
        };

        static List<Publisher> publishers = new List<Publisher>()
        {
             new Publisher(1,"publisher1","address1")
            ,new Publisher(2,"publisher2","address2")
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Select all books");
            var q1 = from b in books
                     select b;
            foreach (var b in q1)
                Console.WriteLine(b.Title);


        }
    }
}
