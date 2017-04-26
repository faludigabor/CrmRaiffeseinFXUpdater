using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using kingsolcrm.entities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;

namespace Raiffesien
{
    class Program
    {
        static void Main(string[] args)
        {

            var raiffeisenParser = new RaiffesenParser();
            var raiffeisenCurrencies = raiffeisenParser.RefreshRates();
          
            CrmConnection crmConnectionDev = new CrmConnection("CRMDEV");
            CrmConnection crmConnectionProd = new CrmConnection("CRMPROD");

            var crmFxUpdater = new CrmFxUpdater(crmConnectionDev, raiffeisenCurrencies);
            crmFxUpdater.UpdateCurrencies();
            crmFxUpdater = new CrmFxUpdater(crmConnectionProd,raiffeisenCurrencies);
            crmFxUpdater.UpdateCurrencies();

        }
    }
}
