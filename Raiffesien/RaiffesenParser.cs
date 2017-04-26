using System.Collections.Generic;
using HtmlAgilityPack;

namespace Raiffesien
{
    internal class RaiffesenParser
    {
        private List<RaiffeisenCurrency> currencies = new List<RaiffeisenCurrency>();

        public RaiffesenParser()
        {
          
        }

        public  List<RaiffeisenCurrency> RefreshRates()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load("https://www.raiffeisen.hu/hasznos/arfolyamok/vallalati/devizaarfolyamok");

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {

                foreach (HtmlNode tbody in table.SelectNodes("tbody"))
                {
                    foreach (HtmlNode row in tbody.SelectNodes("tr"))
                    {
                        var res = row.SelectNodes("th|td");
                        currencies.Add(new RaiffeisenCurrency() { CurrencyCode = res[0].InnerText, ExchangeRate = decimal.Parse(res[4].InnerText) });

                    }

                }
            }
            return currencies;
        }
    }
}