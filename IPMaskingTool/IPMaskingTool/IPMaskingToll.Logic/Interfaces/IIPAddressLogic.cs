using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPMaskingToll.Logic.Interfaces
{
    internal interface IIPAddressLogic
    {
        string ChangeLine(string line);
    }
}
