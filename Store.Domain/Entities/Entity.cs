using Flunt.Notifications;

namespace Store.Domain.Entities;

public class Entity : Notifiable<Notification>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
}
