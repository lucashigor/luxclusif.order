using FluentAssertions;
using luxclusif.order.domain.Conts;
using System;
using Xunit;

using DomainExceptions = luxclusif.order.domain.Exceptions;

namespace luxclusif.order.tests.UnitTest.domain.Exceptions;
public class EntityGenericExceptionTests
{
    [Fact(DisplayName = nameof(InstatiateWithOnlyMessage))]
    [Trait("Domain", "Exceptions - EntityGenericException")]
    public void InstatiateWithOnlyMessage()
    {
        //Arrange
        var msg = "Nova Exceptions";

        //Act
        var ex = new DomainExceptions.EntityGenericException(msg);

        //Assert
        ex.Message.Should().Be(msg);
        ex.Code.Should().Be(ErrorsCodes.ErrorCode);
    }

    [Fact(DisplayName = nameof(InstatiateWithMessageAndCode))]
    [Trait("Domain", "Exceptions - EntityGenericException")]
    public void InstatiateWithMessageAndCode()
    {
        //Arrange
        var msg = "Nova Exceptions";
        var ErrorCode = "0002";

        //Act
        var ex = new DomainExceptions.EntityGenericException(msg, ErrorCode);

        //Assert
        ex.Message.Should().Be(msg);
        ex.Code.Should().Be(ErrorCode);
    }

    [Fact(DisplayName = nameof(ThrowWithMessageAndCode))]
    [Trait("Domain", "Exceptions - EntityGenericException")]
    public void ThrowWithMessageAndCode()
    {
        //Arrange
        var msg = "Nova Exceptions";

        //Act
        Action action = () => throw new DomainExceptions.EntityGenericException(msg);

        //Assert
        action.Should().Throw<DomainExceptions.EntityGenericException>()
                .WithMessage(msg);
    }
}
