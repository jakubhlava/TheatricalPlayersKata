using System;

namespace TheatricalPlayersRefactoringKata;

public class CliInvoicePrinter: IInvoicePrinter
{
    public string Print(InvoiceInfo invoice)
    {
        var result = $"Statement for {invoice.Customer}\n";
        
        foreach (var playInfo in invoice.PlayInfos)
        {
            result += String.Format(invoice.CultureInfo, "  {0}: {1:C} ({2} seats)\n", playInfo.Name, Convert.ToDecimal(playInfo.Price / 100), playInfo.Seats);
        }
        
        result += string.Format(invoice.CultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(invoice.TotalAmount / 100));
        result += $"You earned {invoice.VolumeCredits} credits\n";
        return result;
    }
}