using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageHandler
{
    private static Client _clientRefrence;
    

    public static Client ClientRefrence
    {
        get => _clientRefrence;
        set => _clientRefrence = value;
    }

    public static string CreateMessage(MessageType messageType,Dictionary<string,string> attributes)
    {
        string attrs = "";
        foreach (KeyValuePair<string, string> pair in attributes)
        {
            attrs += pair.Value.ToString() + "@@";
        }
        
        return messageType.ToString() + "@@" + attrs;
    }

    public static void HandleMessage(string message)
    {
        string[] separators = { "@@" };

        string[] substrings = message.Split(separators, StringSplitOptions.None);
        switch (substrings[0])
        {
            case "SetClientID":
                Invoker.AddNewCommand(new SetClientIDCommand(Convert.ToInt32(substrings[1])));
                Invoker.ExecuteCommand();
                break;
            case "DrawCard":
                // Dictionary<string, object> attributes = JsonUtility.FromJson<Dictionary<string, object>>(substrings[1]);
                // Invoker.AddNewCommand(new SetRivalIDCommand((int)attributes["playerID"]));
                Invoker.AddNewCommand(new SetRivalIDCommand(Convert.ToInt32(substrings[2])));
                Invoker.ExecuteCommand();
                // Invoker.AddNewCommand(new DrawCardCommand((int)attributes["cardID"],(int)attributes["playerID"]));
                Invoker.AddNewCommand(new DrawCardCommand(Convert.ToInt32(substrings[1]),Convert.ToInt32(substrings[2])));
                Invoker.ExecuteCommand();
                break;
        }
    }

    
}
