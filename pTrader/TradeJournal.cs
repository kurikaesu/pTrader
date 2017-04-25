using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace pTrader
{
	public partial class TradeJournal : AppKit.NSView
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournal(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournal(NSCoder coder) : base(coder)
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
