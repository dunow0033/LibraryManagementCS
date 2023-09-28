using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryManagement
{
	class Student : User
	{
		private int grade;
		private string school;

		public Student(string name, int grade, string school) : base(name)
		{
			this.grade = grade;
			this.school = school;
		}
	}
}
