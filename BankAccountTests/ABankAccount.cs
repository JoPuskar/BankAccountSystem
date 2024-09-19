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

    [Test]
    public void ShouldCalculateAndAddMonthlyInerestToBalance()
    {
        // Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.5;
        var sut = new BankAccount(initialBalance, annualInterestRate);


        decimal monthlyInterest = initialBalance * (decimal)(annualInterestRate/12);
        decimal expectedBalanceAfterInterest = initialBalance + monthlyInterest;

        // Act
        sut.CalculateInterest();

        // Assert
        // Post 3: Balance == Balance@pre + monthlyInterest

        Assert.That(sut.Balance, Is.EqualTo(expectedBalanceAfterInterest));

    }

    [Test]
    public void ShouldSubtractMonthlyServiceChargeAndResetCounters()
    {
        // Arrange
        decimal initialBalance = 100m;
        double annualInterestRate = 0.5;
        decimal depositAmount = 50m;
        decimal withdrawAmount = 20m;

        var sut = new BankAccount(initialBalance, annualInterestRate);

        // Act
        sut.Deposit(depositAmount);
        sut.Withdraw(withdrawAmount);

        decimal monthlyServiceCharge = 10m;
        sut.AddMonthlyServiceCharge(monthlyServiceCharge);
        sut.MonthlyProcess();

        decimal expectedBalanceBeforeInterest = initialBalance - withdrawAmount + depositAmount - monthlyServiceCharge;
        decimal expectedBalanceAfterInterest = expectedBalanceBeforeInterest * (1 + ((decimal)annualInterestRate / 12));
        // Assert
        Assert.That(sut.Balance, Is.EqualTo(expectedBalanceAfterInterest).Within(0.00001));
        Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
        Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));
        Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));

    }
}