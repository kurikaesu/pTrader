using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace pTrader
{
	public partial class TradeJournalEntryController : AppKit.NSViewController
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournalEntryController(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournalEntryController(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		// Call to load from the XIB/NIB file
		public TradeJournalEntryController() : base("TradeJournalEntry", NSBundle.MainBundle)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed view accessor
		public new TradeJournalEntry View
		{
			get
			{
				return (TradeJournalEntry)base.View;
			}
		}
	}
}
