using M1_Classes;

BankCustomer customer1 = new();
BankCustomer customer2 = new("List", "Zoeng");
BankCustomer customer3 = new("Joe", "Shmo");

Console.WriteLine($"BankCustomer 1: {customer1.FirstName} {customer1.LastName} {customer1.CustomerId}");
Console.WriteLine($"BankCustomer 2: {customer2.FirstName} {customer2.LastName} {customer2.CustomerId}");
Console.WriteLine($"BankCustomer 3: {customer3.FirstName} {customer3.LastName} {customer3.CustomerId}");

BankAccount account1 = new(customer1.CustomerId);
BankAccount account2 = new(customer2.CustomerId, 1500, "Checking");
BankAccount account3 = new(customer3.CustomerId, 2500, "Checking");

Console.WriteLine(
  $"Account 1: Account # {account1.AccountNumber}, type {account1.AccountType}, balance {account1.Balance}, rate {BankAccount.InterestRate}, customer ID {account1.CustomerId}"
);
Console.WriteLine(
  $"Account 2: Account # {account2.AccountNumber}, type {account2.AccountType}, balance {account2.Balance}, rate {BankAccount.InterestRate}, customer ID {account2.CustomerId}"
);
Console.WriteLine(
  $"Account 3: Account # {account3.AccountNumber}, type {account3.AccountType}, balance {account3.Balance}, rate {BankAccount.InterestRate}, customer ID {account3.CustomerId}"
);
