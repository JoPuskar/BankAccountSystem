using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountLibrary
{
    public class SavingsAccount : BankAccount
    {
        public enum AccountStatus
        { 
            Active,
            Inactive
        }
        public AccountStatus Status { get; private set; }

        public SavingsAccount(decimal initialBalance, double annualInterestRate) 
            : base(initialBalance, annualInterestRate)
        {
            Status = initialBalance <= 25m ? AccountStatus.Inactive : AccountStatus.Active;
        }

        public override void Withdraw(decimal withdrawAmount)
        {
            if (Status == AccountStatus.Inactive)
            {
                Console.WriteLine("Withdrawal denied! Cannot withdraw from an inactive class");
                return;
            }

            base.Withdraw(withdrawAmount);

            if (Balance <= 25m)
            {
                Status = AccountStatus.Inactive;
            }
        }
        public override void Deposit(decimal depositAmount)
        {
            if (Status == AccountStatus.Inactive && (Balance + depositAmount > 25m))
            {
                Status = AccountStatus.Active;
            }

            base.Deposit(depositAmount);
        }
        public override void MonthlyProcess()
        {
            if (NumberOfWithdrawals > 4)
            {
                decimal extraWithdrawals = NumberOfWithdrawals - 4;
                MonthlyServiceCharge += extraWithdrawals;
            }

            base.MonthlyProcess();

            if (Balance <= 25m)
            {
                Status = AccountStatus.Inactive;
            }
        }
    }
}
