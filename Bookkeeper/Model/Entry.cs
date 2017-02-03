using System;
using SQLite;

namespace Bookkeeper
{
	public class Entry
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; private set;}
		public bool Income { get; set;}
		public int Ammount { get; set;}
		public DateTime Date { get; set;}
		public string Description { get; set;}
		public int TypeId { get; set;}
		public int AccountId { get; set;}
		public int TaxRateId { get; set;}
		//public bool income { get; set;}



		public Entry()
		{
			

		}

		public override string ToString()
		{
			return string.Format("[Entry: Description = {0}, Ammount = {1}]", Description, Ammount);
		}

	}
}
