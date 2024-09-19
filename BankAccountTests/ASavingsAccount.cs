using System;
using BankAccountLibrary;

namespace BankAccountTests
{
    class ASavingsAccount
    {
        [Test]
        public void ShouldSetBalanceAndAnnualInterestRateWhenConstructed() {

            // Arrange
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            // Act
            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            // Assert
            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
            Assert.That(sut.AnnualInterestRate, Is.EqualTo(annualInterestRate));
        }

        [Test]
        public void ShouldSetStatusBasedOnBalance()
        {
            // Test when Balance > 25
            // Arrange
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            // Act 
            var sut = new SavingsAccount (initialBalance, annualInterestRate);

            // Assert
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Active));

            // Test when Balance is <= $25

            // Arrange
            initialBalance = 20m;

            // Act
            sut = new SavingsAccount(initialBalance, annualInterestRate);

            // Assert
            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void WithdrawWhenAccountIsActiveAndShouldDecreaseBalance()
        {
            // Arrange 
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            decimal withDrawAmount = 50m;

            // Act
            sut.Withdraw(withDrawAmount);

            // Assert
            decimal expectedBalance = initialBalance - withDrawAmount;
            Assert.That(sut.Balance, Is.EqualTo(expectedBalance));
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Active));


        }

        [Test]
        public void WithdrawInactiveAccountShouldNotChangeBalance()
        {
            // Arrange
            decimal initialBalance = 20m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            decimal withDrawAmount = 10m;
            sut.Withdraw(withDrawAmount);


            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Inactive));
        }

        [Test]
        public void DepositingInactiveAccountShouldActivateAccount()
        {
            decimal initialBalance = 20m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            decimal depositAmount = 50m;
            sut.Deposit(depositAmount);


            decimal expectedBalance = initialBalance + depositAmount;
            Assert.That(sut.Balance, Is.EqualTo(expectedBalance));
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Active));
        }

        [Test]
        public void ShouldNotChangeStatusWhenDepositingInactiveAccountBelowThreshold()
        {
            // Arrange 
            decimal initialBalance = 10m;
            double annulInterestRate = 0.05;

            // Act
            var sut = new SavingsAccount (initialBalance, annulInterestRate);
            sut.Deposit(10m);

            // Assert
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Inactive));
        }

        [Test]
        public void MonthlyProcessShouldAddServiceChargeForExcessWithdrawals()
        {
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            for (int i = 0; i <= 5; i++)
            {
                sut.Withdraw(10m);
            }

            sut.MonthlyProcess();

            decimal expectedServiceCharge = 2m;
            decimal expectedBalanceBeforeInterest = initialBalance - (6 * 10m) - expectedServiceCharge;

            decimal monthlyInterestRate = (decimal)(annualInterestRate / 12);
            decimal expectedBalanceAfterInterest = expectedBalanceBeforeInterest * (1 + monthlyInterestRate);

            Assert.That(sut.Balance, Is.EqualTo(expectedBalanceAfterInterest));
            Assert.That(sut.MonthlyServiceCharge, Is.EqualTo(0));
            Assert.That(sut.NumberOfDeposits, Is.EqualTo(0));
            Assert.That(sut.NumberOfWithdrawals, Is.EqualTo(0));

            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Active));
        }

        [Test]
        public void MonthlyProcessShouldUpdateStatus()
        {
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            decimal withdrawAmount = 90m;

            sut.Withdraw(withdrawAmount);
            sut.MonthlyProcess();

            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Inactive));
        }

        //  Tests for Edge cases

        [Test]
        public void DepositShouldNotAllowZeroOrNegativeAmount()
        {
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            sut.Deposit(0m);
            sut.Deposit(-65m);

            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void WithdrawShouldNotAllowZeroOrNegativeAmount()
        {
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            sut.Withdraw(0m);
            sut.Withdraw(-95m);

            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void CalculateInterestWithVariousBalancesAndRates()
        {
            var testCases = new[]
            {
                new { initialBalance = 100m, annualInterestRate = 0.05, expectedBalanceAfterInterest = 100.416666666666667m },
                new { initialBalance = 200m, annualInterestRate = 0.1, expectedBalanceAfterInterest = 201.666666666666667m },
                new { initialBalance = 50m, annualInterestRate = 0.02, expectedBalanceAfterInterest = 50.083333333333333m },
                new { initialBalance = 0m, annualInterestRate = 0.05, expectedBalanceAfterInterest = 0m }
            };

            foreach (var testCase in testCases) 
            {
                var sut = new SavingsAccount(testCase.initialBalance, testCase.annualInterestRate);
                sut.CalculateInterest();

                Assert.That(sut.Balance, Is.EqualTo(testCase.expectedBalanceAfterInterest).Within(0.0001m));

            }
        }

        [Test]
        public void MonthlyProcessWithDifferentNumberOfTransactions()
        {
            decimal initialBalance = 100m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            //sut.Deposit(50m);
            sut.Withdraw(10m);
            sut.Withdraw(10m);
            sut.Withdraw(10m);
            sut.Withdraw(10m);
            sut.Withdraw(10m); // 5th withdrawal

            decimal expectedServiceCharge = 1m;

            //sut.AddMonthlyServiceCharge(expectedServiceCharge);
            sut.MonthlyProcess();

            decimal expectedBalanceBeforeInterest = initialBalance - (5 * 10m) - expectedServiceCharge;
            decimal monthlyInterestRate = (decimal)(annualInterestRate / 12);
            decimal expectedBalanceAfterInterest = expectedBalanceBeforeInterest * (1 + monthlyInterestRate);

            Assert.That(sut.Balance, Is.EqualTo(expectedBalanceAfterInterest));
        }

        [Test]
        public void SavingAccountShouldNotAllowWithdrawalsWhenInactive()
        {
            decimal initialBalance = 20m;
            double annualInterestRate = 0.05;

            var sut = new SavingsAccount(initialBalance, annualInterestRate);

            sut.Withdraw(10m);

            Assert.That(sut.Balance, Is.EqualTo(initialBalance));
            Assert.That(sut.Status, Is.EqualTo(SavingsAccount.AccountStatus.Inactive));
        }
    }
}
