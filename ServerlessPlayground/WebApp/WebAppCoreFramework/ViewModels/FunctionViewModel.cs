using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreFramework.Models;

namespace WebAppCoreFramework.ViewModels
{
    public class FunctionViewModel
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public string Receiver { get; set; }

        public string Response { get; set; }

        public Email ToEmail()
        {
            return new Email
            {
                Body = Message,
                Subject = Subject,
                Receiver = Receiver
            };
        }
    }
}
