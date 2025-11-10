using System.Globalization;
using M1_Inheritance;

class Program
{
  static void Main()
  {
    // Create BankCustomer objects
    string firstName = "Tim";
    string lastName = "Shao";

    Console.WriteLine($"Creating a BankCustomer object for customer {firstName} {lastName}...");
    BankCustomer customer1 = new(firstName, lastName);

    // Task 1: Review the code files in the Starter project
    Console.WriteLine($"\nUsing derived classes to create bank account objects for {customer1.ReturnFullName()}...");
    // BankAccount bankAccount1 = new(customer1.CustomerId, 1000, "Checking");
    CheckingAccount checkingAccount1 = new(customer1.CustomerId, 500);
    SavingsAccount savingsAccount1 = new(customer1.CustomerId, 1000);
    MoneyMarketAccount moneyMarketAccount1 = new(customer1.CustomerId, 2000);

    // Task 2: Create derived classes (CheckingAccount, SavingsAccount, and MoneyMarketAccount)
    Console.WriteLine($"\nUsing inherited properties to display {customer1.ReturnFullName()}'s account information...");
    //Console.WriteLine($" - {bankAccount1.AccountType} account #{bankAccount1.AccountNumber} has a balance of {bankAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
    Console.WriteLine(
      $" - {checkingAccount1.AccountType} account #{checkingAccount1.AccountNumber} has a balance of {checkingAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );
    Console.WriteLine(
      $" - {savingsAccount1.AccountType} account #{savingsAccount1.AccountNumber} has a balance of {savingsAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );
    Console.WriteLine(
      $" - {moneyMarketAccount1.AccountType} account #{moneyMarketAccount1.AccountNumber} has a balance of {moneyMarketAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );

    // Step 3 - Demonstrate using inherited methods to withdraw, transfer, and deposit funds
    Console.WriteLine(
      "\nDemonstrating inheritance of the Withdraw, Transfer, and Deposit methods from the base class..."
    );

    // define a transaction amount
    decimal transactionAmount = 200;

    // Withdraw from checking account, transfer from savings account to checking account, and deposit into money market account
    Console.WriteLine($" - Withdraw {transactionAmount} from {checkingAccount1.AccountType} account");
    checkingAccount1.Withdraw(transactionAmount);

    Console.WriteLine(
      $" - Transfer {transactionAmount.ToString("C", CultureInfo.CurrentCulture)} from {savingsAccount1.AccountType} account into {checkingAccount1.AccountType} account"
    );
    savingsAccount1.Transfer(checkingAccount1, transactionAmount);

    Console.WriteLine(
      $" - Deposit {transactionAmount.ToString("C", CultureInfo.CurrentCulture)} into {moneyMarketAccount1.AccountType} account"
    );
    moneyMarketAccount1.Deposit(transactionAmount);

    Console.WriteLine(
      $" - {checkingAccount1.AccountType} account #{checkingAccount1.AccountNumber} has a balance of {checkingAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );
    Console.WriteLine(
      $" - {savingsAccount1.AccountType} account #{savingsAccount1.AccountNumber} has a balance of {savingsAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );
    Console.WriteLine(
      $" - {moneyMarketAccount1.AccountType} account #{moneyMarketAccount1.AccountNumber} has a balance of {moneyMarketAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}"
    );
    // Step 4 - Demonstrate using the 'new' keyword to override a base class method
    Console.WriteLine("\nDemonstrating the use of the 'new' keyword to override a base class method...");

    // Display account information for each account
    Console.WriteLine($" - {checkingAccount1.ReturnAccountInfo()}");
    Console.WriteLine($" - {savingsAccount1.ReturnAccountInfo()}");
    Console.WriteLine($" - {moneyMarketAccount1.ReturnAccountInfo()}");
    //Step 5: Demonstrate the overridden properties and methods in the derived classes
    Console.WriteLine("\nDemonstrating specialized Withdraw behavior:");

    // CheckingAccount: Withdraw within overdraft limit
    Console.WriteLine("\nCheckingAccount: Attempting to withdraw $600 (within overdraft limit)...");
    checkingAccount1.Withdraw(600);
    Console.WriteLine(checkingAccount1.ReturnAccountInfo());

    // CheckingAccount: Withdraw exceeding overdraft limit
    Console.WriteLine("\nCheckingAccount: Attempting to withdraw $1000 (exceeding overdraft limit)...");
    checkingAccount1.Withdraw(1000);
    Console.WriteLine(checkingAccount1.ReturnAccountInfo());

    // SavingsAccount: Withdraw within limit
    Console.WriteLine("\nSavingsAccount: Attempting to withdraw $200 (within withdrawal limit)...");
    savingsAccount1.Withdraw(200);
    Console.WriteLine(savingsAccount1.ReturnAccountInfo());

    // SavingsAccount: Withdraw exceeding limit
    Console.WriteLine("\nSavingsAccount: Attempting to withdraw $900 (exceeding withdrawal limit)...");
    savingsAccount1.Withdraw(900);
    Console.WriteLine(savingsAccount1.ReturnAccountInfo());

    // SavingsAccount: Exceeding maximum number of withdrawals per month
    Console.WriteLine("\nSavingsAccount: Exceeding maximum number of withdrawals per month...");

    savingsAccount1.ResetWithdrawalLimit();

    for (int i = 0; i < 7; i++)
    {
      Console.WriteLine($"Attempting to withdraw $50 (withdrawal {i + 1})...");
      savingsAccount1.Withdraw(50);
      Console.WriteLine(savingsAccount1.ReturnAccountInfo());
    }

    // MoneyMarketAccount: Withdraw within minimum balance
    Console.WriteLine("\nMoneyMarketAccount: Attempting to withdraw $1000 (maintaining minimum balance)...");
    moneyMarketAccount1.Withdraw(1000);
    Console.WriteLine(moneyMarketAccount1.ReturnAccountInfo());

    // MoneyMarketAccount: Withdraw exceeding minimum balance
    Console.WriteLine("\nMoneyMarketAccount: Attempting to withdraw $1900 (exceeding minimum balance)...");
    moneyMarketAccount1.Withdraw(1900);
    Console.WriteLine(moneyMarketAccount1.ReturnAccountInfo());
  }
}
