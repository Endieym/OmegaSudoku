using OmegaSudoku.UI;

namespace OmegaSudoku;

class Program
{
    static void Main(string[] args) // Main Function, calls for UI to be initialized
    {
        InitiateUI();
        Console.ReadKey();
    }

    static void InitiateUI()  // calls for UserInterface class to work
    {
        UserInterface.InitiateUI();
    }






}