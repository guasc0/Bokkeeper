using System;
using SQLite;
namespace Bookkeeper
{
	public class Account : Java.Lang.Object
	{
		[PrimaryKey]
		public int Number { get; set; }
		public string Name { get; set;}
		public string Type { get; set;}

		public Account()
		{
			
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", Name, Number);
		}

	}
}
