using LibraryManagement;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

class Program
{
	static void Main(string[] args)
	{
		MainMethod();
	}

	static void MainMethod()
	{
		List<Book> bookList = new List<Book>
			{
				new Book("python", "Alex"),
				new Book("advance cpp", "Ryan"),
				new Book("dsa", "John Doe"),
				new Book("java", "Jane Smith"),
				new Book("the great gatsby", "Robert Johnson"),
				new Book("to kill a mockingbird", "Emily Davis"),
				new Book("complete history of the world", "Michael Anderson"),
				new Book("the da vinci code", "Dan Brown"),
				new Book("the count of monte cristo", "David Wilson"),
				new Book("how to win friends and influence people", "Laura Brown"),
				new Book("the catcher in the rye", "Daniel Taylor"),
				new Book("a tale of two cities", "Sophia Martinez")
			};

		List<User> names = new List<User>();
		bool IDin = false;
		int ID = 0;

		Library lib = new Library(bookList, names);
		bool running = true;
		while(running){
			Console.WriteLine("------------------------------");
			Console.WriteLine("Welcome to Eastbrook library");
			Console.WriteLine("------------------------------");
			Console.WriteLine("1. Login");
			Console.WriteLine("2. Register As New User");
			Console.WriteLine("3. Quit");
			Console.WriteLine("Enter your choice:  ");
			string choice = Console.ReadLine();

			if (choice == "1")
			{
				Console.WriteLine("Ok, please enter your ID:  ");
				string stringID = Console.ReadLine();
				ID = Int32.Parse(stringID);
				foreach (var i in lib.GetUsers())
				{
					if (i.GetID() == ID)
					{
						IDin = true;
						break;
					}
				}

				if (IDin)
					main_menu(lib, ID);
				else
				{
					Console.WriteLine("Sorry, that user ID was not found in our system. You can register as a new user or exit.\n");
					Console.WriteLine("------------------------------");
					Console.WriteLine("1. Register As New User");
					Console.WriteLine("2. Exit\n");
					Console.WriteLine("Please enter your choice:  ");
					string menuChoice = Console.ReadLine();

					if (menuChoice == "1")
					{
						User new_user = RegisterNewUser(lib);
						ID = new_user.GetID();
						Console.WriteLine($"Thank you. Here is your ID: {ID}.");
						main_menu(lib, ID);
					}
					else
						break;
				}
			}
			else if (choice == "2")
			{
				User new_user = RegisterNewUser(lib);
				ID = new_user.GetID();
				Console.WriteLine($"Thank you. Here is your ID: {ID}.");
				main_menu(lib, ID);
			}
			else if (choice == "3")
				break;
			else
			{
				Console.WriteLine("Invalid selection, please try again!!!\n");
				continue;
			}
		}

		Console.WriteLine("Thank you!! Bye-bye!!");
	}

	public static void main_menu(Library lib, int ID) {

		User user = lib.GetUsers().FirstOrDefault(u => u.GetID() == ID);

		/*foreach (var i in lib.GetUsers()) {
			if (i.GetID() == ID) {
				user = i;
				break;
			}
		}*/

		if (user != null) {
			Console.WriteLine($"\nWelcome {user.GetName()}, what would you like to do today?\n");
		} else {
			Console.WriteLine("\nUser not found. Please register or enter a valid user ID.");
		}

		while (true) {
			Console.WriteLine("------------------------------");
			Console.WriteLine("1. View current list of all books");
			Console.WriteLine("2. View current list of available books");
			Console.WriteLine("3. View a book's information");
			Console.WriteLine("4. Checkout a book");
			Console.WriteLine("5. Return a book");
			Console.WriteLine("6. Donate a book");
			Console.WriteLine("7. Logout");


			Console.WriteLine("\nEnter your choice:  ");
			string choice = Console.ReadLine();


			if (choice == "1")
			{
				lib.DisplayAllBooks();
			}
			else if (choice == "2")
			{
				lib.DisplayAvailableBooks();
			}
			else if (choice == "3")
			{
				Console.WriteLine("Ok, great, what book would you like more information on?\n");
				string book_name = Console.ReadLine();
				Book book = null;

				foreach (var available_book in lib.GetBooks())
				{
					if (available_book.GetTitle() == book_name)
					{
						book = available_book;
						break;
					}
				}
				if (book != null)
				{
					Console.WriteLine("Great, here you go: \n");
					book.DisplayInfo();
				}
				else
				{
					Console.WriteLine("No book exists with the given name \n");
				}
			}
			else if (choice == "4")
			{
				if (user is Student)
				{
					if (user.GetBorrowedBooks().Count >= 6)
					{
						Console.WriteLine("Sorry, students can only check out up to 6 books at a time. Please return 1 book before you check out another.");
						continue;
					}
				}

				else if (user is User)
				{
					if (user.GetBorrowedBooks().Count >= 3)
					{
						Console.WriteLine("Sorry, regular users can only check out up to 3 books at a time. Please return 1 book before you check out another.");
						continue;
					}
				}

				Console.WriteLine("Ok, great, what book would you like to check out?\n");
				string book_name = Console.ReadLine();
				Book available_book = null;

				foreach (Book book_search in lib.GetBooks())
				{
					if (book_search.GetTitle() == book_name)
					{
						available_book = book_search;
						break;
					}
				}
				if (available_book.GetAvailability() == "Unavailable")
				{
					Console.WriteLine($"{available_book.GetTitle()} is currently being borrowed by someone else.  Sorry.\n");
					continue;
				}
				if (available_book != null)
				{
					user.borrow_book(available_book);
					lib.CheckoutBook(available_book);
				}
				else
				{
					Console.WriteLine("Sorry, we don't currently have that book in our inventory.\n");
				}
			}
			else if (choice == "5")
			{
				Console.WriteLine("Ok, great, what book would you like to return?\n");
				string book_name = Console.ReadLine();
				Book available_book = null;

				foreach (Book book_search in lib.GetBooks())
				{
					if (book_search.GetTitle() == book_name)
					{
						available_book = book_search;
						break;
					}
				}
				if (available_book != null)
				{
					user.return_book(available_book);
					lib.ReturnBook(available_book);
				}
				else
				{
					Console.WriteLine("Sorry, we don't currently have that book in our inventory!!  Perhaps it's from a different library?\n");
				}
			}
			else if (choice == "6")
			{
				Console.WriteLine("Ok, great, what is the title of the book you would like to donate?\n");
				string title = Console.ReadLine();
				Console.WriteLine("And the author?\n");
				string author = Console.ReadLine();
				Book book = new Book(title, author);
				lib.AddBook(book);
			}
			else if (choice == "7")
			{
				break;
			}
		}
	}

	public static User RegisterNewUser(Library lib) {
		Console.WriteLine("OK, great, you want to register as a new user.  Are you a student or regular user?\n");
		Console.WriteLine("------------------------------");
		Console.WriteLine("1. Student");
		Console.WriteLine("2. Regular User");
		Console.WriteLine("Enter your choice:  ");
		string choice = Console.ReadLine();

		if (choice == "1") {
			Console.WriteLine("Ok, you are a student.  Students can check out up to 6 books at a time.\n");
			Console.WriteLine("What's your name?\n");
			string name = Console.ReadLine();
			Console.WriteLine("What school do you go to?\n");
			string school = Console.ReadLine();
			Console.WriteLine("What grade are you in?\n");
			string gradeString = Console.ReadLine();
			Student student = new Student(name, Int32.Parse(gradeString), school);
			lib.AddUser(student);
			return student;
		} else {
			Console.WriteLine("Ok, you are a regular user.  Regular users can check out up to 3 books at a time.\n");
			Console.WriteLine("What's your name?  ");
			string name = Console.ReadLine();
			User user = new User(name);
			lib.AddUser(user);
			return user;
		}
	}
}
