using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace GetOrder
{
    [Cmdlet(VerbsCommon.Get, "Order")]
    public class GetOrder : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "How many cups would you like to purchase?"
        )]
        public int Cups;

        [Parameter(
            Mandatory = false, 
            Position = 2, 
            HelpMessage = "What would you like to purchase?"
        )]
        [ValidateSet("Lemonade","Water","Tea","Coffee")]
        public string Product = "Lemonade";

        protected override void ProcessRecord()
        {
            Collection<String> order = new Collection<string>();
            for (int cup = 1; cup <= Cups; cup++) {
                order.Add(cup.ToString() + ": A cup of " + Product);
            }
            WriteObject(order, true);
        }
    }
}
