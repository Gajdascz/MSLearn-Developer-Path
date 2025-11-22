namespace M1_GetStartedWithDatesTimesAndTimeZones;

public interface IMonthlyReportGenerator
{
  void GenerateMonthlyReport();
  void GenerateCurrentMonthToDateReport();
  void GeneratePrevious30DayReport();
}
