
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Messaging;

namespace LTICSharpAutoFramework.Utils
{
    public class MSMQUtil
    {
        public static bool QueueNameExists(string serverName, string queueName)
        {
            bool result = false;
            MessageQueue[] queues = MessageQueue.GetPrivateQueuesByMachine(serverName);
            string queueNameToSearch = @"private$\" + queueName;
            foreach(MessageQueue queue in queues)
            {
                if (queue.QueueName == queueNameToSearch)
                    result = true;
            }
            return result;
        }

        public static void SendMessage(string serverName, string queueName, string message, bool isTransactional)
        {
            string messageQueueConnectionString = string.Empty;
            if (string.IsNullOrEmpty(serverName))
                messageQueueConnectionString = string.Format(@".\Private$\{0}", queueName);
            else
                messageQueueConnectionString = string.Format(@"FormatName:Direct=OS:{0}\Private$\{1}", serverName, queueName);

            MessageQueue msgQueue = new MessageQueue(messageQueueConnectionString, QueueAccessMode.Send);

            Message msg = new Message();
            msg.Formatter = new BinaryMessageFormatter();
            msg.Body = message;

            if (isTransactional)
                msgQueue.Send(msg, MessageQueueTransactionType.Single);
            else
                msgQueue.Send(msg);
        }

        public static List<string> GetAllMessage(string serverName, string queueName)
        {
            List<string> messages = new List<string>();

            string messageQueueConnectionString = string.Empty;
            if (string.IsNullOrEmpty(serverName))
                messageQueueConnectionString = string.Format(@".\Private$\{0}", queueName);
            else
                messageQueueConnectionString = string.Format(@"FormatName:Direct=OS:{0}\Private$\{1}", serverName, queueName);


            MessageQueue msgQueue = new MessageQueue(messageQueueConnectionString, QueueAccessMode.Receive);

            Message[] msgs = msgQueue.GetAllMessages();

            foreach( Message msg in msgs)
            {
                msg.Formatter = new BinaryMessageFormatter();

                var reader = new StreamReader(msg.BodyStream, Encoding.UTF8);
                var msgBody = reader.ReadToEnd();
            }

            return messages;
        }
    }
}


