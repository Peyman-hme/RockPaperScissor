using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Receiver
{
    private static int _clientID;

   
    public static int ClientID
    {
        get => _clientID;
        set => _clientID = value;
    }
    
    private static int _rivalID;

   
    public static int RivalID
    {
        get => _rivalID;
        set => _rivalID = value;
    }
    private static List<IClientIDSubscriber> _clientIDSubscribers  = new List<IClientIDSubscriber>();
    private static List<IRivalIDSubscriber> _rivalIDSubscribers  = new List<IRivalIDSubscriber>();
    private static List<IDrawnCardSubscriber> _drawnCardSubscribers  = new List<IDrawnCardSubscriber>();

    public static void Subscribe(IClientIDSubscriber subscriber)
    {
        _clientIDSubscribers.Add(subscriber);
    }
    
    public static void Unsubscribe(IClientIDSubscriber subscriber)
    {
        _clientIDSubscribers.Remove(subscriber);
    }
    
    public static void Subscribe(IRivalIDSubscriber subscriber)
    {
        _rivalIDSubscribers.Add(subscriber);
    }
    
    public static void Unsubscribe(IRivalIDSubscriber subscriber)
    {
        _rivalIDSubscribers.Remove(subscriber);
    }
    public static void Subscribe(IDrawnCardSubscriber subscriber)
    {
        _drawnCardSubscribers.Add(subscriber);
    }
    
    public static void Unsubscribe(IDrawnCardSubscriber subscriber)
    {
        _drawnCardSubscribers.Remove(subscriber);
    }
    
    public static void SendMessageToServer(MessageType messageType,Dictionary<string,string> attributes)
    {
        string message = MessageHandler.CreateMessage(messageType, attributes);
        MessageHandler.ClientRefrence.SendMessage(message);
    }

    public static void NotifyClientIDSubscribers(int id)
    {
        foreach (var subscriber in _clientIDSubscribers)
        {
            subscriber.Update(id);
        }
    }
    public static void NotifyRivalIDSubscribers(int id)
    {
        foreach (var subscriber in _rivalIDSubscribers)
        {
            subscriber.Update(id);
        }
    }
    public static void NotifyDrawnCardSubscribers(int cardID,int playerID)
    {
        foreach (var subscriber in _drawnCardSubscribers)
        {
            subscriber.Update(cardID,playerID);
        }
    }

    public static void ShowDrawCard(int cardID,int playerID)
    {
        NotifyDrawnCardSubscribers(cardID,playerID);
    }
    
    public static void SetCLientID(int playerID)
    {
        ClientID = playerID;
        NotifyClientIDSubscribers(playerID);
    }
    public static void SetRivalID(int playerID)
    {
        RivalID = playerID;
        NotifyRivalIDSubscribers(playerID);
    }
    
    
}
