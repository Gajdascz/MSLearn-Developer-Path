namespace M2_Create_And_Manage_Events;

public class Counter(int Threshold)
{
  public event EventHandler<ThresholdReachedEventArgs>? ThresholdReached;

  public int Total { get; private set; } = 0;
  public int Threshold { get; set; } = Threshold;

  public void Add(int value)
  {
    Total += value;
    Console.WriteLine($"Current Total: {Total}");
    if (Total >= Threshold)
      OnThresholdReached(new ThresholdReachedEventArgs { Threshold = Threshold, TimeReached = DateTime.Now });
  }

  protected virtual void OnThresholdReached(ThresholdReachedEventArgs e) => ThresholdReached?.Invoke(this, e);
}
