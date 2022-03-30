using Bogus;
using DomainEntity = luxclusif.order.domain.Entity;


namespace luxclusif.order.tests;
public class BaseFixture
{
    public BaseFixture()
    => Faker = new Faker();

    public Faker Faker { get; set; }

    public string GetStringRigthSize(int minLength, int maxlength)
    {
        var userName = "";
        while (userName.Length < minLength)
        {
            userName = Faker.Person.FullName;
        }

        if (userName.Length > maxlength)
        {
            userName = userName[..maxlength];
        }

        return userName;
    }

    public string GetValidUserName()
    {
        return GetStringRigthSize(3,100);
    }

    public virtual DomainEntity.Order GetValidUser()
    {
        return new DomainEntity.Order(
            GetValidUserName(),
            System.Guid.NewGuid(),
            120
            );
    }
}
