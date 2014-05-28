using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace GetOrder
{
    [Cmdlet(VerbsCommon.Get, "Order")]
    public class GetOrder : PSCmdlet
    {
        int _cups;
        string _product = "Lemonade";

        [Parameter(
            Mandatory=true, 
            Position=1, 
            HelpMessage="How many cups would you like to purchase?"
        )]        
        public int Cups
        {
            set { _cups = value; }
        }

        [Parameter(
            Mandatory=false, 
            Position=2, 
            HelpMessage="What would you like to purchase?"
        )]
        [ValidateSet("Lemonade","Water","Tea","Coffee")]
        public string Product
        {
            set { _product = value; }
        }

        protected override void ProcessRecord()
        {
            Collection<String> order = new Collection<string>();
            for (int cup = 1; cup <= _cups; cup++) {
                order.Add(cup.ToString() + ": A cup of " + _product);
            }
            WriteObject(order);
        }
    }
}
