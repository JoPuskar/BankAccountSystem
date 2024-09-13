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
    public void ShouldInceaseBalanceAfterDeposit()
    {
        //Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.05;
        var sut = new BankAccount(initialBalance, annualInterestRate);

        decimal depositAmount = 100m;
        // Actp
        sut.Deposit(depositAmount);

        // Post 1: Balance == Balance@pre + depositAmount
        // Assert

        Assert.That(sut.Balance, Is.EqualTo(200m));
    }

    [Test]
    public void ShouldDecreaseBalanceAfterWithdrawal()
    { 
        //Arrange
        
    }
}