
namespace BankAccountLibrary
{
    public class BankAccount
    {
        
        public decimal Balance { get; private set; }
        public double AnnualInterestRate { get; }

        public BankAccount(decimal initialBalance, double annualInterestRate)
        {
            Balance = initialBalance;
            AnnualInterestRate = annualInterestRate;
        }

        public void Deposit(decimal depositAmount)
        {
            Balance += depositAmount;
        }

        public void Withdraw(decimal withdrawAmount)
        {
            Balance -= withdrawAmount;
        }

        public void CalculateInterest()
        {
            double monthlyInterestRate = AnnualInterestRate / 12;
            decimal monthlyInterest = Balance * (decimal)monthlyInterestRate;
            Balance += monthlyInterest;

        }
    }

}
