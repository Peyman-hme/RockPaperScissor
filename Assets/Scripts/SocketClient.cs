using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketClient : MonoBehaviour,Client
{
    private Socket clientSocket;
    private Thread receiveThread;
    private bool isRunning;
    

    private void Start()
    {
        MessageHandler.ClientRefrence = this;
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        // Create a TCP/IP socket
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Connect to the server
        IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        IPEndPoint serverEndPoint = new IPEndPoint(serverIP, 5555);
        clientSocket.Connect(serverEndPoint);

        Debug.Log("Connected to server");

        // Start receiving messages from the server
        isRunning = true;
        // receiveThread = new Thread(new ThreadStart(ReceiveMessage));
        // receiveThread.Start();
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        clientSocket.Send(data);
    }

    public void ReceiveMessage()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        while (isRunning)
        {
            try
            {
                // Receive data from the server
                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                Debug.Log($"Message received from server: {message}");
                MessageHandler.HandleMessage(message);
            }
            catch (SocketException ex)
            {
                Debug.LogError($"Socket exception: {ex}");
                isRunning = false;
            }
        }
    }

    private void OnDestroy()
    {
        // Stop receiving messages and close the socket
        isRunning = false;
        if (clientSocket != null)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}