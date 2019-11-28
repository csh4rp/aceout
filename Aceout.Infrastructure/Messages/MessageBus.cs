using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Hangfire;

namespace Aceout.Infrastructure.Messages
{
    public class MessageBus : IMessageBus
    {
        public void Send<TMessage>(TMessage message)
        {
            BackgroundJob.Enqueue<HandlerActivator>(x => x.ActivateAsync(message));
        }

    }
}
