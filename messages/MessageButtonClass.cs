using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yellowantSDK
{
    public class MessageButtonClass
    {
        public string Value, Name, Text;
        public Command command = new Command();

        public  MessageButtonClass(Command command, string Value = "", string Name = "")
        {
            this.Value = Value;
            this.Name = Name;
            // this.Text = Text;

        }

    }

    public class Command
    {

    }
}
