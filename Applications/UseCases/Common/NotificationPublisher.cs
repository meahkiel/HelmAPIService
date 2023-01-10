namespace Applications.UseCases.Common
{
    public interface INotificationPublisher {
        void Add(INotification notification);
        Task Publish();
    }
    
    public class NotificationPublisher : INotificationPublisher
    {
        private readonly IPublisher _publisher;
        private readonly List<INotification> _notifications;
        public NotificationPublisher(IPublisher publisher)
        {
            _publisher = publisher;
            _notifications = new List<INotification>();
        }

        public void Add(INotification notification)
        {
            _notifications.Add(notification);
        }

        public async Task Publish()
        {
            if (_notifications.Count > 0)
            {
                foreach (var notification in _notifications)
                {
                    //publish only if dispense log
                    await _publisher.Publish(notification);
                }
            }
        }


    }
}