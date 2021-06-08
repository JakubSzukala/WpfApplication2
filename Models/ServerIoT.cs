using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Models
{
    class ServerIoT
    {
        private readonly string _ip;
        private string requestBody;
        static readonly HttpClient client = new HttpClient();
        public ServerIoT(string ip_address)
        {
            _ip = ip_address;
            requestBody = null;
        }

        public async Task<string> makeHttpRequest()
        {
            requestBody = await client.GetStringAsync(_ip);
            return requestBody;
        }

        
    }
}
