using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Net.Http;
using System.IO;

namespace BusinessAppServer.Sms
{
    public class ViaNettSMS
    {

        // Declarations
        private string username;
        private string password;

        /// <summary>
        /// Constructor with username and password to ViaNett gateway. 
        /// </summary>
        public ViaNettSMS(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        /// <summary>
        /// Send SMS message through the ViaNett HTTP API.
        /// </summary>
        /// <returns>Returns an object with the following parameters: Success, ErrorCode, ErrorMessage</returns>
        /// <param name="msgsender">Message sender address. Mobile number or small text, e.g. company name</param>
        /// <param name="destinationaddr">Message destination address. Mobile number.</param>
        /// <param name="message">Text message</param>
        public OTPResponse sendSMS(string msgsender, string destinationaddr, string message)
        {
            // Declarations
            string url;
           
            long l;
            Result result;
            OTPResponse otpResponse = new OTPResponse(); ;

            // Build the URL request for sending SMS.
            url = "http://smsc.vianett.no/ActiveServer/MT/?"
                + "username=" + username
                + "&password=" +password
                + "&destinationaddr=" + destinationaddr
                + "&message=" + message
                + "&refno=1";

            // Check if the message sender is numeric or alphanumeric.
            if (long.TryParse(msgsender, out l))
            {
                url = url + "&sourceAddr=" + msgsender;
            }
            else
            {
                url = url + "&fromAlpha=" + msgsender;
            }
            // Send the SMS by submitting the URL request to the server. The response is saved as an XML string.
           var serverResult = DownloadString(url);
            // Converts the XML response from the server into a more structured Result object.
            result = ParseServerResult(serverResult);
            otpResponse.Status = result.Success;
            otpResponse.OTP = message;         
            otpResponse.OTPtime = DateTime.Now;
            // Return the Result object.
            return otpResponse;
        }
        /// <summary>
        /// Downloads the URL from the server, and returns the response as string.
        /// </summary>
        /// <param name="URL"></param>
        /// <returns>Returns the http/xml response as string</returns>
        /// <exception cref="WebException">WebException is thrown if there is a connection problem.</exception>
        private Stream DownloadString(string URL)
        {
            // Create WebClient instanse.
                try
                {
                    var client = new HttpClient();

                // Download and return the xml response
                return client.GetStreamAsync(URL).Result;
            }
                catch (WebException ex)
                {
                    // Failed to connect to server. Throw an exception with a customized text.
                    throw new WebException("Error occurred while connecting to server. " + ex.Message, ex);
                }
            //}
        }


        /// <summary>
        /// Parses the XML code and returns a Result object.
        /// </summary>
        /// <param name="ServerResult">XML data from a request through HTTP API.</param>
        /// <returns>Returns a Result object with the parsed data.</returns>
        private Result ParseServerResult(Stream ServerResult)
        {
            var reader = new StreamReader(ServerResult);
            var res = reader.ReadToEnd();
            XDocument xDoc = XDocument.Parse(res);
            var x = XName.Get("errorcode");
            var a = XName.Get("ack");
            //System.Xml.XmlNode ack;
            Result result = new Result();
            //xDoc.LoadXml(ServerResult);
            var nodes =
         
            result.ErrorCode = xDoc.Root.Attribute("errorcode").Value;
            result.ErrorMessage = xDoc.Root.Attribute("refno").Value;
            result.Success = (result.ErrorCode == "0");
            return result;
        }

        /// <summary>
        /// The Result object from the SendSMS function, which returns Success(Boolean), ErrorCode(Integer), ErrorMessage(String).
        /// </summary>
        public class Result
        {
            public bool Success;
            public string ErrorCode;
            public string ErrorMessage;
            
        }
        public class OTPResponse
        {
            public bool Status;
            public string OTP;
            public DateTime OTPtime;

        }
    }
}
