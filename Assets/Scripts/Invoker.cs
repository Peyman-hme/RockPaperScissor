using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Invoker
{
    private static Queue<ICommand> _commands = new Queue<ICommand>();

    public static void AddNewCommand(ICommand command)
    {
        _commands.Enqueue(command);
    }

    public static void ExecuteCommand()
    {
        _commands.Dequeue().Execute();
    }
}
