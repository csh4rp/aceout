using Autofac;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Messages
{

    public class HandlerActivator
    {
        private readonly IContainer _container;

        public HandlerActivator(IContainer container)
        {
            _container = container;
        }

        [Queue("messages")]
        public async Task ActivateAsync<TMessage>(TMessage message)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var handlers = _container.Resolve<IEnumerable<IMessageHandler<TMessage>>>();

                if (!handlers.Any())
                {
                    return;
                }

                var tasks = new List<Task>();

                foreach (var handler in handlers)
                {
                    tasks.Add(handler.HandleAsync(message));
                }

                await Task.WhenAll(tasks.ToArray());
            }
        }
    }
}
