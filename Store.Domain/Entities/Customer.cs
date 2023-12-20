namespace Store.Domain.Entities;

public class Customer(string name, string email)
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
}
