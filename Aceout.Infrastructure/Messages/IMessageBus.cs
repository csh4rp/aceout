using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Messages
{
    public interface IMessageBus
    {
        void Send<TMessage>(TMessage message);
    }
}
