namespace ConsoleRegisterStudent
{
    public static class CourseDatabase
    {
        static Dictionary<int, string> courses = new()
        {
            { 1, "IT 145" },
            { 2, "IT 200" },
            { 3, "IT 201" },
            { 4, "IT 270" },
            { 5, "IT 315" },
            { 6, "IT 328" },
            { 7, "IT 330" }
        };

        public static int CourseCount => courses.Count;

        public static string GetCourseInfo(int courseID) => courses.ContainsKey(courseID) ? courses[courseID] : "";

        public static string GetCourseInfo(params int[] courseIDs)
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < courseIDs.Length; i++)
            {
                sb.Append($"{courses[courseIDs[i]]}");
            }

            return sb.ToString();
        }

        public static string GetAllCourses()
        {
            var sb = new System.Text.StringBuilder();
            foreach (var course in courses)
            {
                sb.Append($"\n[{course.Key}] {course.Value}");
            }

            return sb.ToString();
        }

        public static string GetAllCourses(int[] currentlyRegistered)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var course in courses)
            {
                // parse the registered courses at each iteration to see if this course is registered
                bool courseRegistered = false;
                for (int i = 0; i < currentlyRegistered.Length; i++)
                {
                    if (course.Key == currentlyRegistered[i])
                    {
                        courseRegistered = true;
                        break;
                    }
                }

                // print a notice beside registered courses
                if (!courseRegistered)
                    sb.Append($"\n[{course.Key}] {course.Value}");
                else sb.Append($"\n[{course.Key}] {course.Value} <Registered>");
            }

            return sb.ToString();
        }
    }
}