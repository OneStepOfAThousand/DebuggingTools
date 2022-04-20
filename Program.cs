// See https://aka.ms/new-console-template for more information
using DebuggingMaximus;
Debugger debugger = new();
debugger.SetDir(string.Empty, "TestDebugg");
Test();

void Test()
{
    Debugging.Log("Test 1");
    debugger.Log("Test a");
    if (Console.ReadKey().Key == ConsoleKey.Q) Test();
}