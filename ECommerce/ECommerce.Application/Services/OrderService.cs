using System;
using ECommerce.Application.Notifications;

namespace ECommerce.Application.Services;

public class OrderService(NotificationService notificationService) : IOrderService
{
   public async Task CreateOrder()
    {
        //farz edin ki db'ye eklendi...
        await notificationService.NotifyAsync("...Müşteri Epostası..","Siparişiniz oluşturuldu");
    }
}
