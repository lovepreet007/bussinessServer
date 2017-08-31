using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAppServer.Sms;
using static BusinessAppServer.Sms.ViaNettSMS;

namespace BusinessAppServer.Sms
{
    public class SmsVerfication
    {

     
        public OTPResponse SendSMS()
        {

            //Set parameters
            string username = "preeet1000@gmail.com";
            string password = "cjrdd";
            string msgsender = "919980913308";
            string destinationaddr = "919663304502";
           


            // Create ViaNettSMS object with username and password
            ViaNettSMS s = new ViaNettSMS(username, password);
            // Declare Result object returned by the SendSMS function
            OTPResponse otpResponse = new OTPResponse();
            try
            {
                Random generator = new Random();
                String message = generator.Next(0, 1000000).ToString("D6");

                // Send SMS through HTTP API
                otpResponse = s.sendSMS(msgsender, destinationaddr, message);
                return otpResponse;
              
            }
            catch (System.Net.WebException ex)
            {                
                return otpResponse;
            }
        }
     

 
    }
}
