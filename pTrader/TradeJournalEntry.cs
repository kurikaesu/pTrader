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
	}
}
