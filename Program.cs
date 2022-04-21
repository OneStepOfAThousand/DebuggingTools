// See https://aka.ms/new-console-template for more information
using DebuggingMaximus;
Debugger debugger = new();
debugger.SetDir(string.Empty, "TestDebugg");
Debugging.SetMaxLogsLimit(5);
Test();

void Test()
{
    debugger.SetDir(string.Empty, "TestDebugg");
    Debugging.Log("Test 1");
    debugger.Log("Test a");
    Debugging.Log("Test 2");
    debugger.Log("Test b");
    Debugging.Log("Test 3");
    Debugging.Log("Test 4");
    if (Console.ReadKey().Key == ConsoleKey.Q)
    {
        Test();
    }
}