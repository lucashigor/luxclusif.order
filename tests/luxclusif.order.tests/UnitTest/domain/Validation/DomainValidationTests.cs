using Bogus;
using FluentAssertions;
using luxclusif.order.domain.Conts;
using luxclusif.order.domain.Exceptions;
using luxclusif.order.domain.Validation;
using System;
using Xunit;

namespace luxclusif.order.tests.UnitTest.domain.Validation;
public class DomainValidationTests
{
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(DateTimeNotDefault))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void DateTimeNotDefault()
        {
            var value = DateTime.UtcNow;

            Action action = () => value.NotDefaultDateTime();

            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(DateTimeNUllNotDefaultSuccess))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void DateTimeNUllNotDefaultSuccess()
        {
            DateTime? value = null!;

            Action action = () => value.NotDefaultDateTime();

            action.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(DateTimeDefaultError))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void DateTimeDefaultError()
        {
            var value = new DateTime();

            Action action = () => value.NotDefaultDateTime();

            //Assert
            var msg = ErrorsMessages.NotDefaultDateTime.GetMessage(nameof(value));

            action.Should().Throw<EntityGenericException>()
                    .WithMessage(msg);
        }

        [Fact(DisplayName = "NotNullOk")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();

            Action action = () => value.NotNullOrEmptyOrWhiteSpace();

            action.Should().NotThrow();
        }

        [Fact(DisplayName = "NotNullThrowWhenNull")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            //
            string value = null!;

            //Act
            Action action = () => value.NotNull();

            //Assert
            var msg = ErrorsMessages.NotNull.GetMessage(nameof(value));

            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }

        [Fact(DisplayName = "NotNullOrEmptyOrWhiteSpaceThrowWhenIsNull")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOrWhiteSpaceThrowWhenIsNull()
        {
            //
            string valueNull = null!;

            //Act
            Action action = () => valueNull.NotNullOrEmptyOrWhiteSpace();

            //Assert
            var msg = ErrorsMessages.NotNull.GetMessage(nameof(valueNull));

            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }

        [Fact(DisplayName = "NotNullOrEmptyOrWhiteSpaceThrowWhenIsEmpty")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOrWhiteSpaceThrowWhenIsEmpty()
        {
            //
            string valueEmpty = string.Empty!;

            //Act
            Action action = () => valueEmpty.NotNullOrEmptyOrWhiteSpace();

            //Assert
            var msg = ErrorsMessages.NotNull.GetMessage(nameof(valueEmpty));

            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }

        [Fact(DisplayName = "NotNullOrEmptyOrWhiteSpaceThrowWhenIsWhiteSpace")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOrWhiteSpaceThrowWhenIsWhiteSpace()
        {
            //
            string valueWhiteSpace = "   ";

            //Act
            Action action = () => valueWhiteSpace.NotNullOrEmptyOrWhiteSpace();

            //Assert
            var msg = ErrorsMessages.NotNull.GetMessage(nameof(valueWhiteSpace));

            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }

        [Fact(DisplayName = "NotNullOrEmptyOrWhiteSpaceSuccess")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOrWhiteSpaceSuccess()
        {
            //
            string value = "Hello";

            //Act
            Action action = () => value.NotNullOrEmptyOrWhiteSpace();

            //Assert
            action.Should().NotThrow();
        }


        //between
        [Fact(DisplayName = "BetweenLengthSuccess")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthSuccessExactMinLength()
        {
            //
            var minLength = 5;
            var maxLength = 10;
            string value = "".PadLeft(minLength);

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "BetweenLengthSuccess")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthSuccessExactMaxLength()
        {
            //
            var minLength = 5;
            var maxLength = 10;
            string value = "".PadLeft(maxLength);

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "BetweenLengthSuccess")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthSuccess()
        {
            //
            var minLength = 5;
            var maxLength = 10;
            string value = "".PadLeft(maxLength - 2);

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "BetweenLengthNotValidNull")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthNotValidNull()
        {
            //
            var minLength = 5;
            var maxLength = 10;
            string value = null!;

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            action.Should().NotThrow();
        }

        [Fact(DisplayName = "BetweenLengthNotValidMinValue")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthNotValidMinValue()
        {
            //
            var minLength = 50;
            var maxLength = 100;
            string value = "".PadLeft(minLength - 1, 'a');

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            var msg = ErrorsMessages.BetweenLength.GetMessage(nameof(value), minLength, maxLength);
            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }

        [Fact(DisplayName = "BetweenLengthNotValidMinValue")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void BetweenLengthNotValidMaxValue()
        {
            //
            var minLength = 50;
            var maxLength = 100;
            string value = "".PadLeft(maxLength + 1, 'a');

            //Act
            Action action = () => value.BetweenLength(minLength, maxLength);

            //Assert
            var msg = ErrorsMessages.BetweenLength.GetMessage(nameof(value), minLength, maxLength);
            action.Should().Throw<EntityGenericException>()
                .WithMessage(msg);
        }
    }
}
