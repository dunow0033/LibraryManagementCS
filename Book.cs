using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
	class Book
	{
		private string title;
		private string author;
		private string availability;

		public Book(string title, string author)
		{
			this.title = title;
			this.author = author;
			this.availability = "Available";
		}

		public string GetTitle()
		{
			return title;
		}

		public string GetAvailability()
		{
			return availability;
		}

		public void SetAvailability(string status)
		{
			this.availability = status;
		}

		public void DisplayInfo()
		{
			Console.WriteLine("Title: " + title + "\n");
			Console.WriteLine("Author: " + author + "\n");
			Console.WriteLine("Availability: " + availability + "\n");
		}
	}
}
