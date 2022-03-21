using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTICSharpAutoFramework.Utils
{
    class MDCOutboundMessage
    {
        public string MDCTranRefNo;
        public string AckDate;
        public string AckTime;
        public string Status;
        public string KDSRefNo;
        public string ErrorCode = string.Empty;
        public string ErrorText = string.Empty;

        private string rawOutboundMessage;

        public MDCOutboundMessage( string outboundMessage, int indexOfRefId)
        {
            rawOutboundMessage = outboundMessage;
            ParseOutboundMessage(indexOfRefId);
        }
        private void ParseOutboundMessage(int indexOfRefId)
        {
            MDCTranRefNo = rawOutboundMessage.Substring(indexOfRefId, 30);
            AckDate = rawOutboundMessage.Substring(indexOfRefId + 30, 8);
            AckTime = rawOutboundMessage.Substring(indexOfRefId + 38, 8);
            Status = rawOutboundMessage.Substring(indexOfRefId + 46, 1);
            if (Status.Equals("A"))
                KDSRefNo = rawOutboundMessage.Substring(indexOfRefId + 47, 30);
            if(Status.Equals("R"))
            {
                ErrorCode = rawOutboundMessage.Substring(indexOfRefId + 77, 6);
                ErrorText = rawOutboundMessage.Substring(indexOfRefId + 83, 80);
            }
        }
    }
}
