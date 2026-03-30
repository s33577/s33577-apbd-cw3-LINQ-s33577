using LinqConsoleLab.EN.Data;

namespace LinqConsoleLab.EN.Exercises;

public sealed class LinqExercises
{
    /// <summary>
    /// Task:
    /// Find all students who live in Warsaw.
    /// Return the index number, full name, and city.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName, City
    /// FROM Students
    /// WHERE City = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Task01_StudentsFromWarsaw()


    {
        // method 1
        var query = from s in UniversityData.Students
            where s.City.Equals("Warsaw")
                select new {s.IndexNumber, s.FirstName, s.LastName, s.City};
        // method 2
        var query2 = from s in UniversityData.Students
            where s.City.Equals("Warsaw")
            select $"{s.IndexNumber}, {s.FirstName}, {s.LastName}, {s.City}";
        
        // method 3

        return query2;
        throw NotImplemented(nameof(Task01_StudentsFromWarsaw));
    }

    /// <summary>
    /// Task:
    /// Build a list of all student email addresses.
    /// Use projection so that you do not return whole objects.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Students;
    /// </summary>
    public IEnumerable<string> Task02_StudentEmailAddresses()
    {
        
        //Query Sytax
        var query = from s in UniversityData.Students
            select s.Email;
        
        // Lambda Syntax
        var method = UniversityData.Students.Select(s => s.Email);
        
        return query;
        
        throw NotImplemented(nameof(Task02_StudentEmailAddresses));
    }

    /// <summary>
    /// Task:
    /// Sort students alphabetically by last name and then by first name.
    /// Return the index number and full name.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName
    /// FROM Students
    /// ORDER BY LastName, FirstName;
    /// </summary>
    public IEnumerable<string> Task03_StudentsSortedAlphabetically()
    {

        var method = from s in UniversityData.Students
            orderby s.FirstName ascending, s.LastName ascending
            select $"{s.IndexNumber}, {s.FirstName}, {s.LastName}";
        
        return method;
        
        throw NotImplemented(nameof(Task03_StudentsSortedAlphabetically));
    }

    /// <summary>
    /// Task:
    /// Find the first course from the Analytics category.
    /// If such a course does not exist, return a text message.
    ///
    /// SQL:
    /// SELECT TOP 1 Title, StartDate
    /// FROM Courses
    /// WHERE Category = 'Analytics';
    /// </summary>
    public IEnumerable<string> Task04_FirstAnalyticsCourse()
    {
        var rest = UniversityData.Courses
            .Where(e => e.Category.Equals("Analytics"))
            .Select(e => new {e.Title, e.StartDate})
            .FirstOrDefault();

        if (rest != null)
        {
            return [$"{rest.Title}, {rest.StartDate}"];
        }

        return ["Course dont exists"];
        
        throw NotImplemented(nameof(Task04_FirstAnalyticsCourse));
    }

    /// <summary>
    /// Task:
    /// Check whether there is at least one inactive enrollment in the data set.
    /// Return one line with a True/False or Yes/No answer.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Enrollments
    ///     WHERE IsActive = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Task05_IsThereAnyInactiveEnrollment()
    {
        
        bool res = UniversityData.Enrollments.Any(e => !e.IsActive);
        
        yield return $"Enrollment {res.ToString()}";
        
        
    }

    /// <summary>
    /// Task:
    /// Check whether every lecturer has a department assigned.
    /// Use a method that validates the condition for the whole collection.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Department)
    /// THEN 1 ELSE 0 END
    /// FROM Lecturers;
    /// </summary>
    public IEnumerable<string> Task06_DoAllLecturersHaveDepartment()
    {

        bool res = UniversityData.Lecturers.All(e => !string.IsNullOrEmpty(e.Department));
        yield return res.ToString();
        
    }

    /// <summary>
    /// Task:
    /// Count how many active enrollments exist in the system.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Enrollments
    /// WHERE IsActive = 1;
    /// </summary>
    public IEnumerable<string> Task07_CountActiveEnrollments()
    {
        
        int coujt = UniversityData.Enrollments.Count(e => !e.IsActive);
        yield return $"Enrollment {coujt.ToString()}";
    }

    /// <summary>
    /// Task:
    /// Return a sorted list of distinct student cities.
    ///
    /// SQL:
    /// SELECT DISTINCT City
    /// FROM Students
    /// ORDER BY City;
    /// </summary>
    public IEnumerable<string> Task08_DistinctStudentCities()
    {

        var distinctt = UniversityData.Students
            .Select(s => s.City)
            .Distinct()
            .OrderBy(s => s);
        
        return distinctt;


        
    }

    /// <summary>
    /// Task:
    /// Return the three newest enrollments.
    /// Show the enrollment date, student identifier, and course identifier.
    ///
    /// SQL:
    /// SELECT TOP 3 EnrollmentDate, StudentId, CourseId
    /// FROM Enrollments
    /// ORDER BY EnrollmentDate DESC;
    /// </summary>
    public IEnumerable<string> Task09_ThreeNewestEnrollments()
    {
        var newest = UniversityData.Enrollments
            .OrderByDescending(e => e.EnrollmentDate)
            .Take(3)
            .Select(e => $"{e.EnrollmentDate:yyyy-MM-dd}, Studentid: {e.StudentId}, courseId: {e.CourseId}");
        return newest;

    }

    /// <summary>
    /// Task:
    /// Implement simple pagination for the course list.
    /// Assume a page size of 2 and return the second page of data.
    ///
    /// SQL:
    /// SELECT Title, Category
    /// FROM Courses
    /// ORDER BY Title
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Task10_SecondPageOfCourses()
    {
        var secondPage = UniversityData.Courses
            .OrderBy(c => c.Title)
            .Skip(2)
            .Take(2)
            .Select(c => $"{c.Title}, {c.Category}");
        return secondPage;
        
    }

    /// <summary>
    /// Task:
    /// Join students with enrollments by StudentId.
    /// Return the full student name and the enrollment date.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, e.EnrollmentDate
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId;
    /// </summary>
    public IEnumerable<string> Task11_JoinStudentsWithEnrollments()
    {

        var method = UniversityData.Students.Join(
            UniversityData.Enrollments,
            s => s.Id,
            e => e.StudentId,
            (s,e) => new{s.FirstName, s.LastName, e.EnrollmentDate} 
        );

        return method.Select(e => e.FirstName + " " + e.LastName + " " + e.EnrollmentDate);
        throw NotImplemented(nameof(Task11_JoinStudentsWithEnrollments));
    }

    /// <summary>
    /// Task:
    /// Prepare all student-course pairs based on enrollments.
    /// Use an approach that flattens the data into a single result sequence.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, c.Title
    /// FROM Enrollments e
    /// JOIN Students s ON s.Id = e.StudentId
    /// JOIN Courses c ON c.Id = e.CourseId;
    /// </summary>
    public IEnumerable<string> Task12_StudentCoursePairs()
    {

        var studentCoursePairs = from e in UniversityData.Enrollments
            join s in UniversityData.Students on e.StudentId equals s.Id
            join c in UniversityData.Courses on e.CourseId equals c.Id
            select $"{s.FirstName} {s.LastName} {c.Title}";
        return studentCoursePairs;

    }

    /// <summary>
    /// Task:
    /// Group enrollments by course and return the course title together with the number of enrollments.
    ///
    /// SQL:
    /// SELECT c.Title, COUNT(*)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task13_GroupEnrollmentsByCourse()
    {
        
        var groupEnrollments = from e in UniversityData.Enrollments
            join c in UniversityData.Courses on e.CourseId equals c.Id
            group e by c.Title into g
                select $"{g.Key} : {g.Count()}";
        return groupEnrollments;
    }

    /// <summary>
    /// Task:
    /// Calculate the average final grade for each course.
    /// Ignore records where the final grade is null.
    ///
    /// SQL:
    /// SELECT c.Title, AVG(e.FinalGrade)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task14_AverageGradePerCourse()
    {
        var averageGradePerCourse = from e in UniversityData.Enrollments
            where e.FinalGrade.HasValue
            join c in UniversityData.Courses on e.CourseId equals c.Id
            group e by c.Title
            into g
            select $"{g.Key} : {g.Average(x => x.FinalGrade)}";
        return averageGradePerCourse;


    }

    /// <summary>
    /// Task:
    /// For each lecturer, count how many courses are assigned to that lecturer.
    /// Return the full lecturer name and the course count.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, COUNT(c.Id)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Task15_LecturersAndCourseCounts()
    {

        var lecturesAndCoursesCount = from l in UniversityData.Lecturers
            join c in UniversityData.Courses on l.Id equals c.LecturerId into lc
            from c in lc.DefaultIfEmpty()
            group c by new { l.FirstName, l.LastName }
            into g
            select $"{g.Key.FirstName} {g.Key.LastName} : {g.Count(x => x != null)}";
        return lecturesAndCoursesCount;

    }

    /// <summary>
    /// Task:
    /// For each student, find the highest final grade.
    /// Skip students who do not have any graded enrollment yet.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, MAX(e.FinalGrade)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY s.FirstName, s.LastName;
    /// </summary>
    public IEnumerable<string> Task16_HighestGradePerStudent()
    {
        var highestGradePerStudent = from s in UniversityData.Students
            join e in UniversityData.Enrollments on s.Id equals e.StudentId
            where e.FinalGrade.HasValue
                group e by new {s.FirstName, s.LastName} into g
                select $"{g.Key.FirstName} {g.Key.LastName} : {g.Max(x => x.FinalGrade)}";
        return highestGradePerStudent;
    }

    /// <summary>
    /// Challenge:
    /// Find students who have more than one active enrollment.
    /// Return the full name and the number of active courses.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.FirstName, s.LastName
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Challenge01_StudentsWithMoreThanOneActiveCourse()
    {
        
        var chall1 = from s in UniversityData.Students
            join e in UniversityData.Enrollments on s.Id equals e.StudentId
            where e.IsActive
                group e by new {s.FirstName, s.LastName} into g
                where g.Count() > 1
                select $"{g.Key.FirstName} {g.Key.LastName} : {g.Count()}";
        return chall1;
    }

    /// <summary>
    /// Challenge:
    /// List the courses that start in April 2026 and do not have any final grades assigned yet.
    ///
    /// SQL:
    /// SELECT c.Title
    /// FROM Courses c
    /// JOIN Enrollments e ON c.Id = e.CourseId
    /// WHERE MONTH(c.StartDate) = 4 AND YEAR(c.StartDate) = 2026
    /// GROUP BY c.Title
    /// HAVING SUM(CASE WHEN e.FinalGrade IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Challenge02_AprilCoursesWithoutFinalGrades()
    {
        var chall2 = from c in UniversityData.Courses
            join e in UniversityData.Enrollments on c.Id equals e.CourseId
            where c.StartDate.Month == 4 && c.StartDate.Year == 2026
            group e by c.Title
            into g
            where g.Sum(x => x.FinalGrade.HasValue ? 1 : 0) == 0
            select g.Key;
        return chall2;
    }

    /// <summary>
    /// Challenge:
    /// Calculate the average final grade for every lecturer across all of their courses.
    /// Ignore missing grades but still keep the lecturers in mind as the reporting dimension.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, AVG(e.FinalGrade)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// LEFT JOIN Enrollments e ON e.CourseId = c.Id
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Challenge03_LecturersAndAverageGradeAcrossTheirCourses()
    {

        var chall3 = from l in UniversityData.Lecturers
            join c in UniversityData.Courses on l.Id equals c.LecturerId
            join e in UniversityData.Enrollments on l.Id equals e.CourseId
            where e.FinalGrade.HasValue
            group e by new { l.FirstName, l.LastName }
            into g
            select $"{g.Key.FirstName} {g.Key.LastName} : {g.Average(x => x.FinalGrade)}";
        return chall3;
    }

    /// <summary>
    /// Challenge:
    /// Show student cities and the number of active enrollments created by students from each city.
    /// Sort the result by the active enrollment count in descending order.
    ///
    /// SQL:
    /// SELECT s.City, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.City
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Challenge04_CitiesAndActiveEnrollmentCounts()
    {
        
        var chal4 = from s in UniversityData.Students
            join e in UniversityData.Enrollments on s.Id equals e.StudentId
            where e.IsActive
                group e by s.City into g
                orderby g.Count() descending
                select $"{g.Key} : {g.Count()}";
        return chal4;
    }

    private static NotImplementedException NotImplemented(string methodName)
    {
        return new NotImplementedException(
            $"Complete method {methodName} in Exercises/LinqExercises.cs and run the command again.");
    }
}
