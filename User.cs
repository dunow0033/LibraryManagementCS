using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
	class User
	{
		private string name;
		private int ID;
		private List<Book> borrowedBooks;

		public User(string name)
		{
			this.name = name;
			Random random = new Random();
			this.ID = random.Next(0, 501);
			this.borrowedBooks = new List<Book>();
		}

		public int GetID() {
			return ID;
		}

		public string GetName(){
			return name;
		}

		public void borrow_book(Book book) {
			borrowedBooks.Add(book);
			book.SetAvailability("Unavailable");
			Console.WriteLine($"{name} has checked out {book.GetTitle()}.\n");
		}

		public void return_book(Book book) {
			if(!borrowedBooks.Contains(book)){
				Console.WriteLine($"You have not borrowed {book.GetTitle()}, so you cannot return this book.");
					return;
			}
			this.borrowedBooks.Remove(book);
			book.SetAvailability("Available");

			Console.WriteLine($"Thank you {name}.  {book.GetTitle()} is now available!!\n");
		}

		public List<Book> GetBorrowedBooks() {
			return this.borrowedBooks;
		}
	}
}
