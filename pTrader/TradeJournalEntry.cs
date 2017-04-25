using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace pTrader
{
	public partial class TradeJournalEntry : AppKit.NSView
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournalEntry(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournalEntry(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		public void SetupView()
		{

		}

		public void textViewDidChange(NSNotification notification)
		{
			Console.WriteLine("Changed");
		}

		[Action("CancelButtonPressed:")]
		public void Cancel(NSObject sender)
		{
			this.Window.Close();
		}

		[Action("SaveButtonPressed:")]
		public void Save(NSObject sender)
		{
			string typeValue = EntryType.StringValue;
			Console.WriteLine(typeValue);
			this.Window.Close();
		}
	}
}
