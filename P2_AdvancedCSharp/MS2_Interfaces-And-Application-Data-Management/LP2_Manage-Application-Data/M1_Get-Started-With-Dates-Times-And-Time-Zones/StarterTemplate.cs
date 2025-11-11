using M1_GetStartedWithDatesTimesAndTimeZones;

#region> Task 1: Create and Manipulate Date and Time Values
Console.WriteLine("Task 1");

// Step 1 - Get the current date and time

// Step 2 - Get the current date only

// Step 3 - Get the current time only

// Step 4 - Get the current day of the week

// Step 5 - Get the current month and year

// Step 6 - Add days to current date

// Step 7 - Parse a date string

// Step 8 - Format a date using .ToString() and "yyyy-MM-dd" format

// Step 9 - Get the current timezone and offset from UTC

// Step 10 - Convert the current time to UTC

#endregion


#region> Task 2 & 3: Calculate Date and Time Values for Bank Customer
Console.WriteLine("Task 2 & 3");

// New customer and accounts
string firstName = "Tim";
string lastName = "Shao";
IBankCustomer customer1 = new BankCustomer(firstName, lastName);

BankAccount account1 = new CheckingAccount(customer1.CustomerId, 500, 400);
BankAccount account2 = new SavingsAccount(customer1.CustomerId, 1000);
BankAccount account3 = new MoneyMarketAccount(customer1.CustomerId, 2000);

BankAccount[] bankAccounts = [account1, account2, account3];

// Step 1 - Create a transaction for the current date and time
account1.Deposit(100);
Console.WriteLine($"Current date and time: {account1.Transactions.Last()}");

// Step 2 - Create a transaction for yesterday at 1:15 PM
DateTime yesterday = DateTime;
account1.ProcessTransaction(amount: +100m, type: "reimbursement", date: yesterday);
Console.WriteLine($"Yesterday at 1:15PM: {account1.Transactions.Last()}");

// Step 3 - First three days of December 2024 at 1:15 PM
for (int day = 1; day <= 3; day++)
{
  DateTime transactionDate = new DateTime();
  account1.ProcessTransaction(amount: +100m, type: "reimbursement", date: transactionDate);
  Console.WriteLine($"December {day}, 2024: {account1.Transactions.Last()}");
}

// Step 4 - Check the time span between accounts in a transfer transaction
account1.Transfer(account2, 5, true, 1000);
int acc1TransferMs = account1.Transactions.Last().Date.Millisecond;
int acc2TransferMs = account2.Transactions.Last().Date.Millisecond;
Console.WriteLine($"Time span between transfers: {""}");
Console.WriteLine();
#endregion
