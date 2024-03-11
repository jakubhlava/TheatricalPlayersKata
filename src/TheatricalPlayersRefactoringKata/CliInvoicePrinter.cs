namespace TheatricalPlayersRefactoringKata;

public class CliInvoicePrinter: IInvoicePrinter
{
    public string Print(InvoiceInfo invoice)
    {
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        foreach (var playInfo in invoice.PlayInfos)
        {
            
        }
        return result;
    }
}