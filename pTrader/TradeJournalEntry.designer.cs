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
	[Register ("TradeJournalEntry")]
	partial class TradeJournalEntry
	{
		[Outlet]
		AppKit.NSDatePicker EntryDate { get; set; }

		[Outlet]
		AppKit.NSTextField EntryPrice { get; set; }

		[Outlet]
		AppKit.NSComboBox EntrySetup { get; set; }

		[Outlet]
		AppKit.NSTextField EntrySize { get; set; }

		[Outlet]
		AppKit.NSComboBox EntryType { get; set; }

		[Outlet]
		AppKit.NSDatePicker ExitDate { get; set; }

		[Outlet]
		AppKit.NSTextField ExitPrice { get; set; }

		[Outlet]
		AppKit.NSTextField Fees { get; set; }

		[Outlet]
		AppKit.NSTextField ProfitLoss { get; set; }

		[Outlet]
		AppKit.NSTextField StopLoss { get; set; }

		[Outlet]
		AppKit.NSTextField TakeProfit { get; set; }

		[Action ("CancelButtonPressed:")]
		partial void CancelButtonPressed (Foundation.NSObject sender);

		[Action ("SaveButtonPressed:")]
		partial void SaveButtonPressed (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (EntryDate != null) {
				EntryDate.Dispose ();
				EntryDate = null;
			}

			if (EntryPrice != null) {
				EntryPrice.Dispose ();
				EntryPrice = null;
			}

			if (EntrySetup != null) {
				EntrySetup.Dispose ();
				EntrySetup = null;
			}

			if (EntrySize != null) {
				EntrySize.Dispose ();
				EntrySize = null;
			}

			if (EntryType != null) {
				EntryType.Dispose ();
				EntryType = null;
			}

			if (ExitDate != null) {
				ExitDate.Dispose ();
				ExitDate = null;
			}

			if (ExitPrice != null) {
				ExitPrice.Dispose ();
				ExitPrice = null;
			}

			if (Fees != null) {
				Fees.Dispose ();
				Fees = null;
			}

			if (ProfitLoss != null) {
				ProfitLoss.Dispose ();
				ProfitLoss = null;
			}

			if (StopLoss != null) {
				StopLoss.Dispose ();
				StopLoss = null;
			}

			if (TakeProfit != null) {
				TakeProfit.Dispose ();
				TakeProfit = null;
			}
		}
	}
}
