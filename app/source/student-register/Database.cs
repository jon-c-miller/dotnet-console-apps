public static class Database
{
    static Dictionary<int, string> Courses = new()
    {
        { 1, "IT 145" },
        { 2, "IT 200" },
        { 3, "IT 201" },
        { 4, "IT 270" },
        { 5, "IT 315" },
        { 6, "IT 328" },
        { 7, "IT 330" }
    };

    public static string GetCourseInfo(params int[] courseIDs)
    {
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < courseIDs.Length; i++)
        {
            sb.Append($"{Courses[courseIDs[i]]}");
        }
        // return the corresponding course id or "" for invalid entries
        return sb.ToString();//Courses.ContainsKey(courseID) ? Courses[courseID] : "";
    }

    public static string GetAllCourses()
    {
        var sb = new System.Text.StringBuilder();
        foreach (var course in Courses)
        {
            sb.Append($"\n[{course.Key}] {course.Value}");
        }

        return sb.ToString();
    }
}