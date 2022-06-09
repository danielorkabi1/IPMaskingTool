using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingToll.Logic.Classes
{
    internal class MaskIP
    {
        internal string? MaskNetworkAddress { get; set; }
        internal Dictionary<string,string>  MaskHosts { get; set; }
        public MaskIP()
        {
            MaskHosts = new Dictionary<string,string>(255);  
        }
    }
}
