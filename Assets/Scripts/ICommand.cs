using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
}


public class DrawCardCommand : ICommand
{
    private int _cardID;
    private int _playerID;

    public DrawCardCommand( int cardID, int playerID)
    {
        _cardID = cardID;
        _playerID = playerID;
    }

    public void Execute()
    {
        Receiver.ShowDrawCard(_cardID,_playerID);
    }
}



public class SendDrawCardCommand : ICommand
{
    private int _cardID;
    private int _playerID;

    public SendDrawCardCommand(int cardID, int playerID)
    {
        _cardID = cardID;
        _playerID = playerID;
    }

    public void Execute()
    {
        Dictionary<string, string> attributes = new Dictionary<string, string>
        {
            ["cardID"] = _cardID.ToString(),
            ["playerID"] = _playerID.ToString()
        };
        
        Receiver.SendMessageToServer(MessageType.DrawCard,attributes);
    }
}

public class SetClientIDCommand : ICommand
{
    private int _playerID;

    public SetClientIDCommand(int playerID)
    {
        _playerID = playerID;
    }

    public void Execute()
    {
        Receiver.SetCLientID(_playerID);
    }
}

public class SetRivalIDCommand : ICommand
{
    private int _playerID;

    public SetRivalIDCommand(int playerID)
    {
        _playerID = playerID;
    }

    public void Execute()
    {
        Receiver.SetRivalID(_playerID);
    }
}