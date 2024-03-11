using System;

namespace TheatricalPlayersRefactoringKata;

public class CliInvoicePrinter: IInvoicePrinter
{
    public string Print(InvoiceInfo invoice)
    {
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        foreach (var playInfo in invoice.PlayInfos)
        {
            result += String.Format(invoice.CultureInfo, "  {0}: {1:C} ({2} seats)\n", playInfo.Name, Convert.ToDecimal(playInfo.Price / 100), playInfo.Seats);
        }
        result += String.Format(invoice.CultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(invoice.TotalAmount / 100));
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }
}