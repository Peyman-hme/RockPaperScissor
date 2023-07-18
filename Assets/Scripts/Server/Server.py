import socket
import threading

HOST = '127.0.0.1'  # Host IP address
PORT = 5555        # Port number

# Create a socket object
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to a specific IP address and port
server_socket.bind((HOST, PORT))

# Listen for incoming connections
server_socket.listen(2)  # Allow up to 2 simultaneous connections

print(f"Server up and running on {HOST}:{PORT}")

# List to hold connected clients
clients = []

# Function to handle each client connection
def handle_client(client_socket, client_address):
    print(f"Client connected from {client_address}")

    # Add the client to the list of connected clients
    clients.append(client_socket)

    # Receive data from the client
    while True:
        try:
            data = client_socket.recv(1024)

            if data:
                # Process the data
                print(f"Data received from {client_address}: {data.decode()}")

                # Forward the message to all other connected clients
                for c in clients:
                    if c != client_socket:
                        c.send(data)
            else:
                # If no data is received, remove the client from the list of connected clients
                clients.remove(client_socket)
                client_socket.close()
                print(f"Connection with {client_address} closed")
                break

        except:
            # If an error occurs, remove the client from the list of connected clients
            clients.remove(client_socket)
            client_socket.close()
            print(f"Connection with {client_address} closed")
            break

# Accept incoming connections
while True:
    # Wait for a client to connect
    client_socket, client_address = server_socket.accept()
    clientID = client_address[1]
    setClientIDStr = "SetClientID@@"+str(clientID)
    client_socket.send(setClientIDStr.encode())
    # Start a new thread to handle the client connection
    client_thread = threading.Thread(target=handle_client, args=(client_socket, client_address))
    client_thread.start()

    print(f"Started new thread for {client_address}")
