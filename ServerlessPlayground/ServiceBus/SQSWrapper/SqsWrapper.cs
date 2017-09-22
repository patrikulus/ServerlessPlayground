using System;
using System.Collections.Generic;
using System.Text;

namespace SQSWrapper
{
    class SqsWrapper : ISqsWrapper
    {
        public void CreateQueue(string name)
        {
            throw new NotImplementedException();
        }

        public void PostMessage<T>(T message)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string queue)
        {
            throw new NotImplementedException();
        }
    }
}
