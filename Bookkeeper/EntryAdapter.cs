using System;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using Android.App;
using Android.Graphics;

namespace Bookkeeper
{
	public class EntryAdapter : BaseAdapter
	{
		
		Activity context;


		public List<Entry> Entries 
		{ 
			get 
			{ 
				return BookKeeperManager.Instance.getEntries();
			}
		}



		public EntryAdapter(Activity activity)
		{
			this.context = activity;

		}

		public override int Count
		{
			get
			{
				return Entries.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{

			View view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.Book_list_item, parent, false);
			view.FindViewById<TextView>(Resource.Id.date).Text = Entries[position].Date.ToString("yyyy-MM-dd");
			view.FindViewById<TextView>(Resource.Id.description).Text = Entries[position].Description;

			TextView ammount = view.FindViewById<TextView>(Resource.Id.ammount);
			ammount.Text = Entries[position].Ammount + ":-";

			ammount.SetTextColor(Entries[position].isIncome ? Color.Green : Color.Red);

			return view;
		}
	}
}