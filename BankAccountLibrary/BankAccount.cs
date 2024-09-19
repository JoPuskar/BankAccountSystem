
namespace BankAccountLibrary
{
    public class BankAccount
    {
        
        public decimal Balance { get; private set; }
        public double AnnualInterestRate { get; }
        public decimal MonthlyServiceCharge { get; protected set; }    
        public int NumberOfWithdrawals { get; protected set; }
        public int NumberOfDeposits { get; protected set; }

        public BankAccount(decimal initialBalance, double annualInterestRate)
        {
            Balance = initialBalance;
            AnnualInterestRate = annualInterestRate;
            NumberOfDeposits = 0;
            NumberOfWithdrawals = 0;
            MonthlyServiceCharge = 0;
        }

        public virtual void Deposit(decimal depositAmount)
        {
            if (depositAmount > 0)
            { 
                Balance += depositAmount;
                NumberOfDeposits++;
             }
        }

        public virtual void Withdraw(decimal withdrawAmount)
        {
            if (withdrawAmount > 0)
            {
                Balance -= withdrawAmount;
                NumberOfWithdrawals++;
            }
        }

        public void CalculateInterest()
        {
            decimal monthlyInterestRate = (decimal)(AnnualInterestRate / 12);
            decimal monthlyInterest = Balance * monthlyInterestRate;
            Balance += monthlyInterest;

        }

        public void AddMonthlyServiceCharge(decimal charge)
        {
            MonthlyServiceCharge = charge;
        }

        public virtual void MonthlyProcess()
        {
            Balance -= MonthlyServiceCharge;
            CalculateInterest();
            MonthlyServiceCharge = 0;
            NumberOfDeposits = 0;
            NumberOfWithdrawals = 0;

        }
    }

}
