using IPMaskingToll.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPMaskingToll.Logic.Classes
{
    internal class IPAddressLogic : IIPAddressLogic
    {

        readonly Dictionary<string, MaskIP> _networkAddress;
        readonly Object lockObj = new Object();
        public IPAddressLogic()
        {
            this._networkAddress = new Dictionary<string, MaskIP>();
        }
        private string[] GetNetworkAddressAndHost(string ipAddress)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));
            int lastIndexOfDot = ipAddress.LastIndexOf('.');
            return new string[] { ipAddress.Substring(0, lastIndexOfDot), ipAddress.Substring(lastIndexOfDot + 1) };
        }
        private string MaskIP(string ipAddress)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            string[] ip = GetNetworkAddressAndHost(ipAddress);
            string netWorkAddress = ip[0];
            string host = ip[1];

            if (!this._networkAddress.ContainsKey(netWorkAddress))
            {
                this._networkAddress.Add(netWorkAddress, new MaskIP() { MaskNetworkAddress = CreateMaskNetworkAddress() });
            }

            string maskHost;
            if (this._networkAddress[netWorkAddress].MaskHosts.ContainsKey(host))
                maskHost = this._networkAddress[netWorkAddress].MaskHosts[host];
            else
            {
                maskHost =CreateMaskHost(netWorkAddress);
                this._networkAddress[netWorkAddress].MaskHosts.Add(host, maskHost.ToString());
            }
            return $"{this._networkAddress[netWorkAddress].MaskNetworkAddress}.{maskHost}";
        }
        private string CreateMaskHost(string netWorkAddress)
        {
            string maskHost = RandomMask().ToString();
            while (this._networkAddress[netWorkAddress].MaskHosts.Values.FirstOrDefault((mask) => maskHost == mask) != null)
            {
                maskHost = RandomMask().ToString();
            }
            return maskHost;
        }
        private string CreateMaskNetworkAddress()
        {
            string[] maskIPAddressArray = new string[3];
            string maskIPAddress;
            do
            {
                for (int i = 0; i < 3; i++)
                {
                    int maskNumber = RandomMask();
                    maskIPAddressArray[i] = maskNumber.ToString();
                }
                maskIPAddress = String.Join('.', maskIPAddressArray);
            } while (this._networkAddress.ContainsKey(maskIPAddress));
            return maskIPAddress;
        }
        private int RandomMask()
        {
            Random rnd = new Random();
            return rnd.Next(1, 255);
        }
        public string ChangeLine(string line)
        {
            MatchCollection matches = GetIPAddress(line);
            if (matches == null) return line;
            List<Task> tasks = new List<Task>();
            foreach (var ipAddress in matches)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    string mask = MaskIP(ipAddress.ToString());
                    lock (lockObj)
                    {
                        line = line.Replace(ipAddress.ToString(), mask);
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            return line;
        }
        private MatchCollection GetIPAddress(string data)
        {
            return Regex.Matches(data, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }
    }
}
