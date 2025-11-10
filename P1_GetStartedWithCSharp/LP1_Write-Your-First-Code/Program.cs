Console.Clear();

(string courseName, int courseCredit)[] courses =
[
  ("English 101", 3),
  ("Algebra 101", 3),
  ("Biology 101", 4),
  ("Computer Science I", 4),
  ("Psychology 101", 3),
];

int totalCreditHours = courses.Sum(c => c.courseCredit);

/* Calculates and displays details on a class's students performance given a predefined set of student names and assignment grades. Displays the students name, final numeric grade (decimal), and final letter grade. */
(string name, int[] grades)[] students = [("Sophia", [4, 3, 3, 3, 4])];
Console.WriteLine("Student\t\tGrade\n");
for (int i = 0; i < students.Length; i++)
{
  var (name, grades) = students[i];
  Console.WriteLine($"Student: {name}\n");
  Console.WriteLine($"{"Course", -20}{"Grade", -10}{"Credit Hours", -15}");
  int totalGradePoints = 0;
  for (int j = 0; j < grades.Length; j++)
  {
    int grade = grades[j];
    var (courseName, courseCredit) = courses[j];
    totalGradePoints += grade * courseCredit;
    Console.WriteLine($"{courseName, -20}{grade, -10}{courseCredit, -15}");
  }

  decimal gradePointAverage = (decimal)totalGradePoints / totalCreditHours;
  int leadingDigit = (int)gradePointAverage;
  int firstDigit = (int)(gradePointAverage * 10) % 10;
  int secondDigit = (int)(gradePointAverage * 100) % 10;
  Console.WriteLine($"\nFinal GPA:{leadingDigit, 11}.{firstDigit}{secondDigit}");
}
Console.WriteLine("Press the Enter key to continue");
Console.ReadLine();
