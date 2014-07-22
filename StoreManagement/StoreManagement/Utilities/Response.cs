using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMarket.ResponceClass
{
    public class Response
    {
        /// <summary>
        /// 5555 for success.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User friendly string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Generally this will be a Stack Trace
        /// </summary>
        public string ExtendedMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strMessage"></param>
        public Response(int intId, string strMessage)
        {
            this.Id = intId;
            this.Message = strMessage;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strMessage"></param>
        /// <param name="strExtendedMessage"></param>
        public Response(int intId, string strMessage, string strExtendedMessage)
        {
            this.Id = intId;
            this.Message = strMessage;
            this.ExtendedMessage = strExtendedMessage;
        }
    }
}