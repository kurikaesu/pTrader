// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace pTrader
{
	[Register ("TradeJournal")]
	partial class TradeJournal
	{
		[Outlet]
		AppKit.NSTableView TradeJournalEntryList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TradeJournalEntryList != null) {
				TradeJournalEntryList.Dispose ();
				TradeJournalEntryList = null;
			}
		}
	}
}
