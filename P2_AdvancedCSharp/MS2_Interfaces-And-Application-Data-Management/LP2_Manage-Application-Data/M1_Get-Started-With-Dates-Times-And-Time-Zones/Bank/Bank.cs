using M1_GetStartedWithDatesTimesAndTimeZones;

#region> Task 1: Create and Manipulate Date and Time Values
Console.WriteLine("Task 1");

// Step 1 - Get the current date and time
DateTime currentDateTime = DateTime.Now;
Console.WriteLine($"Current Date and Time: {currentDateTime}");

// Step 2 - Get the current date only
DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
Console.WriteLine($"Current Date: {currentDate}");

// Step 3 - Get the current time only
TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
Console.WriteLine($"Current Time: {currentTime}");

// Step 4 - Get the current day of the week
DayOfWeek currentDayOfWeek = DateTime.Now.DayOfWeek;
Console.WriteLine($"Current Day of the Week:{currentDayOfWeek}");

// Step 5 - Get the current month and year
int currentMonth = DateTime.Now.Month;
int currentYear = DateTime.Now.Year;
Console.WriteLine($"CurrentMonth: {currentMonth}, Current Year: {currentYear}");

// Step 6 - Add days to current date
DateTime datePlusDays = DateTime.Now.AddDays(10);
Console.WriteLine($"Date Plus 10 Days: {datePlusDays}");

// Step 7 - Parse a date string
DateTime parsedDate = DateTime.Parse("2025-03-13");
Console.WriteLine($"Parsed Date: {parsedDate}");

// Step 8 - Format a date using .ToString() and "yyyy-MM-dd" format
string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
Console.WriteLine($"Formatted Date: {formattedDate}");

// Step 9 - Get the current timezone and offset from UTC
TimeZoneInfo currentTimeZone = TimeZoneInfo.Local;
TimeSpan offsetFromUtc = currentTimeZone.GetUtcOffset(DateTime.Now);
Console.WriteLine($"Current Time Zone: {currentTimeZone.DisplayName}, Offset from UTC: {offsetFromUtc}");

// Step 10 - Convert the current time to UTC
DateTime utcTime = DateTime.UtcNow;
Console.WriteLine($"UTC Time: {utcTime}");
Console.WriteLine();
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
DateTime yesterday = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 15, 0));
account1.ProcessTransaction(amount: +100m, type: "reimbursement", date: yesterday);
Console.WriteLine($"Yesterday at 1:15PM: {account1.Transactions.Last()}");

// Step 3 - First three days of December 2024 at 1:15 PM
for (int day = 1; day <= 3; day++)
{
  DateTime transactionDate = new(2024, 12, day, 13, 15, 0);
  account1.ProcessTransaction(amount: +100m, type: "reimbursement", date: transactionDate);
  Console.WriteLine($"December {day}, 2024: {account1.Transactions.Last()}");
}

// Step 4 - Check the time span between accounts in a transfer transaction
account1.Transfer(account2, 5, true, 1000);
Console.WriteLine(
  TimeSpan.FromMilliseconds(
    account1.Transactions.Last().Date.Millisecond,
    account2.Transactions.Last().Date.Millisecond
  )
);
Console.WriteLine();
#endregion
