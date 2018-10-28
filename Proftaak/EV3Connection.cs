using EV3WifiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proftaak
{
    public class EV3Connection
    {
        // myEV3 is used to communicate with the LEGO EV3.
        private EV3Wifi myEV3;
        readonly string IpAddress = "192.168.202.55";

        public EV3Connection()
        {
            // EV3: Create an EV3Wifi object which you can use to talk to the EV3.
            myEV3 = new EV3Wifi();

            if (myEV3.Connect("1234", IpAddress) == false)
            {
                Disconnect();
            }
        }
        
        public void Disconnect()
        {
            myEV3.Disconnect();
        }

        public bool CheckMessage()
        {
            if (myEV3.isConnected)
            {
                string message = myEV3.ReceiveMessage("EV3_OUTBOX0");

                if (string.IsNullOrEmpty(message))
                {
                    return false;
                }

                return message == "true";
            }

            return false;
        }

        public void SendDrinkDistance(int distance1, int distance2)
        {
            if (myEV3.isConnected)
            {
                myEV3.SendMessage(distance1 + " " + distance2, "0");
            }
        }
    }
}
