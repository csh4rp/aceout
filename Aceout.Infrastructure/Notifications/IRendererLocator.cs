using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public interface IRendererLocator
    {
        IEnumerable<NotificationRenderer<TModel>> GetAll<TModel>();
        IEnumerable<NotificationInfo> GetInfoList();
    }
}
