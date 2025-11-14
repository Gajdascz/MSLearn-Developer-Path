using M2_Create_And_Manage_Events;

// Counter counter = new(5);
// counter.ThresholdReached += (sender, e) => Console.WriteLine("Threshold reached!");
//
// counter.Add(3);
// counter.Add(2);
// Console.WriteLine("\n");

static void Counter_ThresholdReached(object? sender, ThresholdReachedEventArgs e) =>
  Console.WriteLine($"Threshold of {e.Threshold} reached at {e.TimeReached}");

Counter counter = new(10);
counter.ThresholdReached += Counter_ThresholdReached;

Console.WriteLine("Press 'a' to add 1 to the counter or 'q' to quit.");
while (true)
{
  char key = Console.ReadKey(true).KeyChar;
  if (key is 'a')
    counter.Add(1);
  if (key is 'q')
    counter.ThresholdReached -= Counter_ThresholdReached;
}
