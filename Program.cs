// See https://aka.ms/new-console-template for more information
using DebuggingMaximus;
Debugger debugger = new();
debugger.SetDir(string.Empty, "TestDebugg");
debugger.SetMaxLogsLimit(5);
Debugging.SetMaxLogsLimit(5);
Test();

void Test()
{
    Debugging.Log("Test 1");
    debugger.Log("Test a");
    if (Console.ReadKey().Key == ConsoleKey.Q) Test();
}