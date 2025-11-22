/*
This application manages transactions at a store check-out line. The
check-out line has a cash register, and the register has a cash till
that is prepared with a number of bills each morning. The till includes
bills of four denominations: $1, $5, $10, and $20. The till is used
to provide the customer with change during the transaction. The item
cost is a randomly generated number between 2 and 49. The customer
offers payment based on an algorithm that determines a number of bills
in each denomination.

Each day, the cash till is loaded at the start of the day. As transactions
occur, the cash till is managed in a method named MakeChange (customer
payments go in and the change returned to the customer comes out). A
separate "safety check" calculation that's used to verify the amount of
money in the till is performed in the "main program". This safety check
is used to ensure that logic in the MakeChange method is working as
expected.
*/

Console.Clear();

const int ONES_INDEX = 0;
const int FIVES_INDEX = 1;
const int TENS_INDEX = 2;
const int TWENTIES_INDEX = 3;

const int BILL_AMOUNT_INDEX = 0;
const int BILL_COUNT_INDEX = 1;

int[] BILL_VALUES = [1, 5, 10, 20];
int[] STARTING_BILLS_COUNT = [50, 20, 10, 5];
int[] cashTill = [0, 0, 0, 0];

// registerDailyStartingCash: $1 x 50, $5 x 20, $10 x 10, $20 x 5 => ($350 total)
int[,] registerDailyStartingCash = new int[,]
{
  { BILL_VALUES[ONES_INDEX], STARTING_BILLS_COUNT[ONES_INDEX] },
  { BILL_VALUES[FIVES_INDEX], STARTING_BILLS_COUNT[FIVES_INDEX] },
  { BILL_VALUES[TENS_INDEX], STARTING_BILLS_COUNT[TENS_INDEX] },
  { BILL_VALUES[TWENTIES_INDEX], STARTING_BILLS_COUNT[TWENTIES_INDEX] },
};

LoadTillEachMorning(registerDailyStartingCash, cashTill);
int SumTill()
{
  int sum = 0;
  for (int i = 0; i < cashTill.Length; i++)
  {
    sum += cashTill[i] * BILL_VALUES[i];
  }
  return sum;
}
int registerCheckTillTotal =
  registerDailyStartingCash[ONES_INDEX, BILL_AMOUNT_INDEX] * registerDailyStartingCash[ONES_INDEX, BILL_COUNT_INDEX]
  + registerDailyStartingCash[FIVES_INDEX, BILL_AMOUNT_INDEX] * registerDailyStartingCash[FIVES_INDEX, BILL_COUNT_INDEX]
  + registerDailyStartingCash[TENS_INDEX, BILL_AMOUNT_INDEX] * registerDailyStartingCash[TENS_INDEX, BILL_COUNT_INDEX]
  + registerDailyStartingCash[TWENTIES_INDEX, BILL_AMOUNT_INDEX]
    * registerDailyStartingCash[TWENTIES_INDEX, BILL_COUNT_INDEX];

// display the number of bills of each denomination currently in the till
LogTillStatus(cashTill);

// display a message showing the amount of cash in the till
Console.WriteLine(TillAmountSummary(cashTill));

// display the expected registerDailyStartingCash total
Console.WriteLine($"Expected till value: {registerCheckTillTotal}");
Console.WriteLine();

var valueGenerator = new Random((int)DateTime.Now.Ticks);

int transactions = 100;

(int itemCost, int ones, int fives, int tens, int twenties) MakePayment()
{
  int itemCost = valueGenerator.Next(2, 50);
  return (
    itemCost,
    itemCost % 2, // [Ones] value is 1 when odd, 0 when even
    (itemCost % 10 > 7) ? 1 : 0, // [Fives] value is when ends with 8 or 9, otherwise 0
    (itemCost % 20 > 13) ? 1 : 0, // [Tens] value is 1 when 13 < itemCost < 20 or 33 < itemCost < 40, otherwise 0
    (itemCost < 20) ? 1 : 2 // [Twenties] value is 1 when itemCost < 20, otherwise 2
  );
}

while (transactions > 0)
{
  transactions -= 1;
  var (itemCost, ones, fives, tens, twenties) = MakePayment();

  Console.WriteLine($"Customer is making a ${itemCost} purchase");
  Console.WriteLine($"\t Using {twenties} twenty dollar bills");
  Console.WriteLine($"\t Using {tens} ten dollar bills");
  Console.WriteLine($"\t Using {fives} five dollar bills");
  Console.WriteLine($"\t Using {ones} one dollar bills");

  try
  {
    // MakeChange manages the transaction and updates the till
    MakeChange(itemCost, cashTill, twenties, tens, fives, ones);

    // Backup Calculation - each transaction adds current "itemCost" to the till
    registerCheckTillTotal += SumTill();
  }
  catch (InvalidOperationException e)
  {
    Console.WriteLine($"Could not complete transaction: {e.Message}");
  }

  Console.WriteLine(TillAmountSummary(cashTill));
  Console.WriteLine($"Expected till value: {SumTill()}");
  Console.WriteLine();
}

Console.WriteLine("Press the Enter key to exit");
Console.ReadLine();

static void LoadTillEachMorning(int[,] registerDailyStartingCash, int[] cashTill)
{
  cashTill[ONES_INDEX] = registerDailyStartingCash[ONES_INDEX, BILL_COUNT_INDEX];
  cashTill[FIVES_INDEX] = registerDailyStartingCash[FIVES_INDEX, BILL_COUNT_INDEX];
  cashTill[TENS_INDEX] = registerDailyStartingCash[TENS_INDEX, BILL_COUNT_INDEX];
  cashTill[TWENTIES_INDEX] = registerDailyStartingCash[TWENTIES_INDEX, BILL_COUNT_INDEX];
}

void MakeChange(int cost, int[] cashTill, int twenties, int tens = 0, int fives = 0, int ones = 0)
{
  cashTill[TWENTIES_INDEX] += twenties;
  cashTill[TENS_INDEX] += tens;
  cashTill[FIVES_INDEX] += fives;
  cashTill[ONES_INDEX] += ones;

  int amountPaid =
    twenties * BILL_VALUES[TWENTIES_INDEX] + tens * BILL_VALUES[TENS_INDEX] + fives * BILL_VALUES[FIVES_INDEX] + ones;
  int changeNeeded = amountPaid - cost;

  if (changeNeeded < 0)
    throw new InvalidOperationException(
      "InvalidOperationException: Not enough money provided to complete the transaction."
    );

  Console.WriteLine("Cashier prepares the following change:");

  while ((changeNeeded >= BILL_VALUES[TWENTIES_INDEX]) && (cashTill[TWENTIES_INDEX] > 0))
  {
    cashTill[TWENTIES_INDEX]--;
    changeNeeded -= BILL_VALUES[TWENTIES_INDEX];
    Console.WriteLine("\t A twenty");
  }

  while ((changeNeeded >= BILL_VALUES[TENS_INDEX]) && (cashTill[TENS_INDEX] > 0))
  {
    cashTill[TENS_INDEX]--;
    changeNeeded -= BILL_VALUES[TENS_INDEX];
    Console.WriteLine("\t A ten");
  }

  while ((changeNeeded >= BILL_VALUES[FIVES_INDEX]) && (cashTill[FIVES_INDEX] > 0))
  {
    cashTill[FIVES_INDEX]--;
    changeNeeded -= BILL_VALUES[FIVES_INDEX];
    Console.WriteLine("\t A five");
  }

  while ((changeNeeded > 0) && (cashTill[ONES_INDEX] > 0))
  {
    cashTill[0]--;
    changeNeeded -= BILL_VALUES[ONES_INDEX];
    Console.WriteLine("\t A one");
  }

  if (changeNeeded > 0)
    throw new InvalidOperationException(
      "InvalidOperationException: The till is unable to make change for the cash provided."
    );
}

void LogTillStatus(int[] cashTill)
{
  Console.WriteLine("The till currently has:");
  Console.WriteLine($"{cashTill[TWENTIES_INDEX] * BILL_VALUES[TWENTIES_INDEX]} in twenties");
  Console.WriteLine($"{cashTill[TENS_INDEX] * BILL_VALUES[TENS_INDEX]} in tens");
  Console.WriteLine($"{cashTill[FIVES_INDEX] * BILL_VALUES[FIVES_INDEX]} in fives");
  Console.WriteLine($"{cashTill[ONES_INDEX]} in ones");
  Console.WriteLine();
}

string TillAmountSummary(int[] cashTill)
{
  return $"The till has {cashTill[TWENTIES_INDEX] * BILL_VALUES[TWENTIES_INDEX] + cashTill[TENS_INDEX] * BILL_VALUES[TENS_INDEX] + cashTill[FIVES_INDEX] * BILL_VALUES[FIVES_INDEX] + cashTill[ONES_INDEX]} dollars";
}
