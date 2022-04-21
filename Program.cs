// See https://aka.ms/new-console-template for more information
using DebuggingMaximus;
Debugger debugger = new();
debugger.SetDir(string.Empty, "TestDebugg");
debugger.SetMaxLogsLimit(5);
Test();

void Test()
{
    debugger.Log("Test a");
    if (Console.ReadKey().Key == ConsoleKey.Q) Test();
}