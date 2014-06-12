using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace GetOrder
{
    [Cmdlet(VerbsCommon.Get, "Order")]
    public class GetOrder : PSCmdlet, IDynamicParameters
    {
        private AgeDynamicParameter agedynparm = null;
        
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
        [ValidateSet("Lemonade","Water","Tea","Coffee","Hard Lemonade")]
        public string Product = "Lemonade";

        public object GetDynamicParameters()
        {
            if (Product == "Hard Lemonade")
            {
                agedynparm = new AgeDynamicParameter();
                return agedynparm;
            }
            else
            {
                return null;
            }
        }

        protected override void BeginProcessing()
        {
            if (agedynparm != null && agedynparm.Age < 21)
            {
                ParameterBindingException pbe = new ParameterBindingException("You are not old enough for Hard Lemonade. How about a nice glass of regular Lemonade instead?");
                ErrorRecord erec = new ErrorRecord(pbe, null, ErrorCategory.PermissionDenied, agedynparm.Age);
                ThrowTerminatingError(erec);                
            }
        }

        protected override void ProcessRecord()
        {
            Collection<String> order = new Collection<string>();
            for (int cup = 1; cup <= Cups; cup++) {
                order.Add(cup.ToString() + ": A cup of " + Product);
            }
            WriteObject(order, true);
        }
    }

    public class AgeDynamicParameter
    {
        private int _age;
        
        [Parameter(
            Mandatory = true,
            Position = 3,
            HelpMessage = "Please enter your age:"
        )]
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }        
    }
}
