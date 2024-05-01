### Student Course Register
<i>A refactored version of a student course registration application that I designed as coursework at university.</i>

<details>
    <summary>Overview</summary>
    <ul><br>
        The original application displays a short menu of courses, then prompts the user to enter the id of the course they wish to register for. Once the maximum amount of courses that can be registered for is registered, the program thanks the user and exits. The program can be exited early if the user wishes to handle registration later. Courses cannot be registered for twice, and there is basic input checking to avoid some parsing errors.
        <br><br>
        Approaching the project again after years of working with C# and building on the techniques I learned at university, the main focus was on improving code readability, separating the code into smaller classes, improving performance by removing boilerplate and transitioning to a more data-based approach, and adding basic quality of life features.
        <br><br>
        The refactored application incorporates the following:
        <ul><br>
            · A notice beside registered courses
            <br>
            · The ability to unregister for a course
            <br>
            · Stringent input checking to avoid errors
            <br>
            · Improved phrasing in user prompt messages
            <br>
            · Some static helper methods to standardize console output and remove boilerplate
            <br>
            · The ability to continue running the application after the max amount of courses has been registered
            <br>
            · Additional prompts for more intuitive flow-control
            <br>
            · A dictionary to store course names by id, which moves to a more maintainable data-based approach
            <br>
            · Improvements to the main loop, greatly simplifying readability
            <br>
            · Reasonable separation of concerns into additional classes that handle specific aspects of the program
        </ul>
    </ul><br>
</details>

<details>
    <summary>Core Goals</summary>
    <ul><br>
        · Demonstrate general competency with C# in a .NET console environment
        <br>
        · Demonstrate knowledge of practices for successful integration with version control and docker
        <br>
        · Demonstrate abilities regarding refactoring and iteration
        <br>
        · Improve methods of application organization, structure, and user-facing flow control
        <br>
    </ul><br>
</details>

<details open>
    <summary>How to Use the Project</summary>
    <ul>
        <br><b>Method 1 (run in code editor)</b><br>
        <ul>· Download project and run in desired code editor as usual (Program.cs)</ul>
    </ul>
    <ul>
        <b>Method 2 (run with Docker)</b><br>
        <ul>
			· Build the docker image: <code>docker build -t student-course-register .</code>
			<br>· Run the image: <code>docker run --rm -it student-course-register</code>
			<br>
			<br><b>-t</b> tags the resulting image as 'student-course-register'
			<br><b>--rm</b> removes the image container when done
			<br><b>-it</b> shows console output
			<br>
		</ul>
        <br><i>Created in VSCode 1.88 on Linux using .Net (C# Dev Kit)</i>
    </ul>
</details>