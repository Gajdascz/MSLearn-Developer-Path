using System;
using System.Collections.Generic;

namespace Data_M2;

// TASK 2: Create Bank Class
// Purpose: Manage customers and transaction reports.

public class Bank(string name)
{
  // TASK 2: Step 1 - Add Name and List<BankCustomer> properties
  public string Name { get; set; } = name;
  public List<BankCustomer> Customers { get; set; } = [];

  // TASK 2: Step 2 - Add a Dictionary<string, List<Transaction>> for transaction reports
  public Dictionary<string, List<Transaction>> TransactionReports { get; set; } = [];

  // TASK 2: Step 3 - Implement AddCustomer method
  public void AddCustomer(BankCustomer customer) => Customers.Add(customer);
}
