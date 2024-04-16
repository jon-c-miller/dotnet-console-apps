public static class ConsoleExtensions
{
    /// <summary> Loop until the user inputs either y or n. </summary>
    public static bool YesOrNoPrompt(string promptText, bool endlineBefore = false)
    {
        // end line when first entering the prompt if desired
        if (endlineBefore)
            Console.Write("\n");

        string continueChoice;
        while (true)
        {
            Console.Write($"{promptText}");
            continueChoice = Console.ReadLine().ToUpper();
            if (continueChoice == "N")
                return false;
            else if (continueChoice == "Y")
                return true;
            else continue;
        }
    }

    /// <summary> Wrap Console.Write() for ease of formatting line spacing before and after the message. </summary>
    public static void PrintToConsole(string message, bool endlineBefore = false, bool endlineAfter = false)
    {
        // print the given message with optional line spacing before and after
        if (endlineBefore)
            Console.Write("\n");

        Console.Write(message);

        if (endlineAfter)
            Console.Write("\n");
    }
}