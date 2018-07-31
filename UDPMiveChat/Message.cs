using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPMiveChat
{
    class Message
    {
        public string Nickname { get; set; }
        public string Messages { get; set; }

        public static IEnumerable<Message> GetMessage()
        {
            return new List<Message>();
        }
    }
}
