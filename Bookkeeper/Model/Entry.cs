using System;
namespace Bookkeeper
{
	public class Entry
	{

		public bool income;
		private int ammount;
		private int date;
		private int totalAmmount;
		private string description;

		public bool Income 
		{ 
			set { income = value;}
		}

		public int Ammount 
		{
			get { return ammount; }
			set { ammount = value; }
		}


		public Entry(int ammount, int date, int totalAmmount, string description)
		{
			this.ammount = ammount;
			this.date = date;
			this.totalAmmount = totalAmmount;
			this.description = description;
		
		}
	}
}
