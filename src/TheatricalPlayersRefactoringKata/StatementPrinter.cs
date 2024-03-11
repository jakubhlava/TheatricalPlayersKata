﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter : PrintingInterface
    {

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);

            var infos = new List<PlayInfo>();
            var invoiceInfo = new InvoiceInfo { PlayInfos = infos, VolumeCredits = volumeCredits };
            
            invoiceInfo.Customer = invoice.Customer;
            invoiceInfo.CultureInfo = new CultureInfo("en-US");

            var  cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                var thisAmount = 0;
                switch (play.Type) 
                {
                    case "tragedy":
                        thisAmount = 40000;
                        if (perf.Audience > 30) {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        thisAmount = 30000;
                        if (perf.Audience > 20) {
                            thisAmount += 10000 + 500 * (perf.Audience - 20);
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
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
                invoiceInfo.TotalAmount += thisAmount;
                
                infos.Add(new PlayInfo { Name = play.Name, Price = thisAmount, Seats = perf.Audience });
            }

            var currentPrinter = new CliInvoicePrinter();
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            
            invoiceInfo.VolumeCredits = volumeCredits;

            var res1 = currentPrinter.Print(invoiceInfo);
            var res2 = result;
            
            return res1;
        }
    }
}
