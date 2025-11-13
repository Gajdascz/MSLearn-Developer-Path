using System;

namespace Data_M2;

// TASK 5: Create Transaction Class
// Purpose: Represent deposits, withdrawals, and transfers.

// TASK 5: Step 2 - Add a constructor to initialize transaction details
public class Transaction(string transactionId, DateTime date, string type, double amount)
{
  // TASK 5: Step 1 - Add properties for transaction details
  public string TransactionId { get; set; } = transactionId;
  public DateTime Date { get; set; } = date;
  public string Type { get; set; } = type;
  public double Amount { get; set; } = amount;
}
