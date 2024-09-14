using BankAccountLibrary;
namespace BankAccountTests;

public class ABankAccount
{


    [Test]
    public void ShouldSetBalanceAndAnnualInterestRateWhenConstructed()
    {
        //Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.05;

        //Act
        var sut = new BankAccount(initialBalance, annualInterestRate);

        //Assert
        Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));

    }

    [Test]
    public void ShouldIncreaseBalanceAfterDeposit()
    {
        //Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.05;
        var sut = new BankAccount(initialBalance, annualInterestRate);

        decimal depositAmount = 100m;
        decimal expectedAmount = initialBalance + depositAmount;

        // Act
        sut.Deposit(depositAmount);

        // Post 1: Balance == Balance@pre + depositAmount
        // Assert

        Assert.That(sut.Balance, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void ShouldDecreaseBalanceAfterWithdrawal()
    {
        // Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.5;
        var sut = new BankAccount(initialBalance,annualInterestRate);

        decimal withdrawAmount = 50m;
        decimal expectedAmount = initialBalance - withdrawAmount;

        // Act
        sut.Withdraw(withdrawAmount);

        // Assert
        // Post 2: Balance == Balance@pre - withdrawAmount

        Assert.That(sut.Balance, Is.EqualTo(expectedAmount));
        
    }
}