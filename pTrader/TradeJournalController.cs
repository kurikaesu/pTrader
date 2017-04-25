using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace pTrader
{
	public partial class TradeJournalController : AppKit.NSViewController
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournalController(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournalController(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		// Call to load from the XIB/NIB file
		public TradeJournalController() : base("TradeJournal", NSBundle.MainBundle)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		//strongly typed view accessor
		public new TradeJournal View
		{
			get
			{
				return (TradeJournal)base.View;
			}
		}
	}
}
