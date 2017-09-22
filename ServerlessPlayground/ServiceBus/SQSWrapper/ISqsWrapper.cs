using System;
using System.Collections.Generic;
using System.Text;

namespace SQSWrapper
{
    interface ISqsWrapper
    {
        void CreateQueue(string name);

        void PostMessage<T>(T message);

        void Subscribe(string queue);
    }
}
