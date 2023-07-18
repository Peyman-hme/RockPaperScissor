using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour,IClientIDSubscriber,IRivalIDSubscriber,IDrawnCardSubscriber
{
    private string[] _cardList={"Rock","Paper","Scissor"};
    private Dictionary<int,Text> _playersDrawnCard = new Dictionary<int, Text>();
    public Text[] DrawnCardTexts;
    private void Start()
    {
        Receiver.Subscribe((IClientIDSubscriber)this);
        Receiver.Subscribe((IRivalIDSubscriber)this);
        Receiver.Subscribe((IDrawnCardSubscriber)this);
    }
    
    
    public void SetDrawCardText(int playerID,Text text)
    {
        Debug.Log(playerID);
        _playersDrawnCard.Add(playerID,text);
    }

    public void ShowDrawCard(int cardID,int playerID)
    {
        Debug.Log(playerID);
        // _playersDrawnCard[playerID].text = _cardList[cardID];
        DrawnCardTexts[0].text = _cardList[cardID];
    }

    public void OnClickRock()
    {
        Invoker.AddNewCommand(new SendDrawCardCommand(0,Receiver.ClientID));
        Invoker.ExecuteCommand();
        Invoker.AddNewCommand(new DrawCardCommand(0,Receiver.ClientID));
        Invoker.ExecuteCommand();
    }
    
    public void OnClickPaper()
    {
        Invoker.AddNewCommand(new SendDrawCardCommand(1,Receiver.ClientID));
        Invoker.ExecuteCommand();
        Invoker.AddNewCommand(new DrawCardCommand(1,Receiver.ClientID));
        Invoker.ExecuteCommand();
    }
    
    public void OnClickScissor()
    {
        Invoker.AddNewCommand(new SendDrawCardCommand(2,Receiver.ClientID));
        Invoker.ExecuteCommand();
        Invoker.AddNewCommand(new DrawCardCommand(2,Receiver.ClientID));
        Invoker.ExecuteCommand();
    }


    void IClientIDSubscriber.Update(int id)
    {
        SetDrawCardText(id,DrawnCardTexts[0]);
    }

    void IRivalIDSubscriber.Update(int id)
    {
        Debug.Log("saggg "+id.ToString());
        SetDrawCardText(id,DrawnCardTexts[1]);
    }

    void IDrawnCardSubscriber.Update(int cardID, int playerID)
    {
        ShowDrawCard(cardID,playerID);
    }
}
