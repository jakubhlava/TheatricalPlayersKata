using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter : PrintingInterface
    {

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var volumeCredits = 0;

            var infos = new List<PlayInfo>();
            var invoiceInfo = new InvoiceInfo { PlayInfos = infos, VolumeCredits = volumeCredits };
            
            invoiceInfo.Customer = invoice.Customer;
            invoiceInfo.CultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                var thisAmount = 0;
                switch (play.Type) 
                {
                    case "tragedy":
                        thisAmount = 40_000;
                        if (perf.Audience > 30) {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        thisAmount = 30_000;
                        if (perf.Audience > 20) {
                            thisAmount += 10_000 + 500 * (perf.Audience - 20);
                        }
                        thisAmount += 300 * perf.Audience;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                invoiceInfo.TotalAmount += thisAmount;
                
                infos.Add(new PlayInfo { Name = play.Name, Price = thisAmount, Seats = perf.Audience });
            }

            var currentPrinter = new CliInvoicePrinter();
            invoiceInfo.VolumeCredits = volumeCredits;

            var res1 = currentPrinter.Print(invoiceInfo);

            return res1;
        }
    }
}
