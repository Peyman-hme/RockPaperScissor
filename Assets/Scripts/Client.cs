using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Client
{
    void SendMessage(string message);
    void ReceiveMessage();
}
