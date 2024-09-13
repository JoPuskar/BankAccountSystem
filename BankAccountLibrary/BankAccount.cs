
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
    }

}
