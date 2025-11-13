using System;
// TASK 6: Step 1 - Add using directive for collections - System.Collections.Generic
using System.Collections.Generic;

namespace Data_M2;

// TASK 6: Create SimulateDepositWithdrawTransfer Class
// Purpose: Simulate and log transactions.

public class SimulateDepositWithdrawTransfer
{
  // TASK 6: Step 2 - Add methods to simulate deposits
  public static void SimulateDeposit(BankAccount account, double amount) =>
    account.AddTransaction(new Transaction(Guid.NewGuid().ToString(), DateTime.Now, "Deposit", amount));

  // TASK 6: Step 3 - Add methods to simulate withdrawals
  public static void SimulateWithdrawal(BankAccount account, double amount) =>
    account.AddTransaction(new Transaction(Guid.NewGuid().ToString(), DateTime.Now, "Withdrawal", amount));

  // TASK 6: Step 4 - Add methods to simulate transfers
  public static void SimulateTransfer(BankAccount fromAccount, BankAccount toAccount, double amount)
  {
    Transaction withdrawal = new(Guid.NewGuid().ToString(), DateTime.Now, "Transfer Out", amount);
    fromAccount.AddTransaction(withdrawal);
    Transaction deposit = new(Guid.NewGuid().ToString(), DateTime.Now, "Transfer In", amount);
    toAccount.AddTransaction(deposit);
  }
}
