using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
	class Library
	{
		private List<Book> books;
		private List<User> users;
		private List<Book> borrowedBooks;

		public Library(List<Book> listOfBooks, List<User> listOfUsers) 
		{
			this.books = listOfBooks;
			this.users = listOfUsers;
			this.borrowedBooks = new List<Book>();
		}

		public void DisplayAllBooks()
		{

			Console.WriteLine("CURRENT LIST OF ALL BOOKS: ");
			foreach(var book in books) { 
				Console.WriteLine(" ♦-- " + book.GetTitle());
			}
			Console.WriteLine();
		}

		public void DisplayAvailableBooks() {

			Console.WriteLine($"\nCURRENT LIST OF ALL AVAILABLE BOOKS: ");
			foreach(Book book in books)
				if(!borrowedBooks.Contains(book))
					Console.WriteLine(" ♦-- " + book.GetTitle());
			Console.WriteLine("\n");
		}

		public void AddBook(Book book)
		{
			this.books.Add(book);
		}

		public void CheckoutBook(Book book)
		{
			this.borrowedBooks.Add(book);
		}

		public void ReturnBook(Book book) {
			if (!borrowedBooks.Contains(book)){
				return;
			}
			borrowedBooks.Remove(book);
		}

		public List<Book> GetBorrowedBooks() {
			return borrowedBooks;
		}

		public List<Book> GetBooks() {
			return books;
		}

		public List<User> GetUsers() {
			return users;
		}

		public void AddUser(User user) {
			users.Add(user);
		}
	}
}
