namespace M1_GetStartedWithDatesTimesAndTimeZones;

public interface IYearlyReportGenerator
{
  void GeneratePreviousYearReport();
  void GenerateCurrentYearToDateReport();
  void GenerateLast365DaysReport();
}
