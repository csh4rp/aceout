using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Notifications
{
    public interface INotificationManager
    {
        Task SendAsync<TModel>(int userId, TModel model);
    }
}
