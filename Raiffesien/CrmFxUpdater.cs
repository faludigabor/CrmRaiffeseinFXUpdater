using System.Collections.Generic;
using System.Linq;
using kingsolcrm.entities;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;

namespace Raiffesien
{
    internal class CrmFxUpdater
    {
        private CrmConnection crmConnection;
        private List<RaiffeisenCurrency> raiffeisenCurrencies;
        private IOrganizationService service;
        public CrmFxUpdater(CrmConnection crmConnection, List<RaiffeisenCurrency> raiffeisenCurrencies)
        {
            this.crmConnection = crmConnection;
            this.raiffeisenCurrencies = raiffeisenCurrencies;
            service = new OrganizationService(crmConnection);
        }

        public void UpdateCurrencies()
        {
            List<TransactionCurrency> currencyList = new List<TransactionCurrency>();

            using (var ctx = new KingsolDevContext(service))
            {
                foreach (var currency in raiffeisenCurrencies)
                {
                    var crmCurrency = (from c in ctx.TransactionCurrencySet
                                       where c.ISOCurrencyCode == currency.CurrencyCode
                                       select c).FirstOrDefault();
                    if (crmCurrency != null)
                    {
                        crmCurrency.ExchangeRate = currency.ExchangeRate;
                        ctx.UpdateObject(crmCurrency);
                        var res = ctx.SaveChanges();
                    }
                }
            }
        }
    }
}