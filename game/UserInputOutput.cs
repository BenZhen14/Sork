namespace Sork;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ClientConnectedEventArgs : EventArgs
{
    public TcpClient Client { get; }

    public ClientConnectedEventArgs(TcpClient client)
    {
        Client = client;
    }
}

public class NetworkGame
{
    public event EventHandler<ClientConnectedEventArgs>? ClientConnected;
    private TcpListener? listener;
    private const int Port = 1701;

    public async Task StartListening()
    {
        listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();

        try
        {
            var client = await listener.AcceptTcpClientAsync();
            OnClientConnected(new ClientConnectedEventArgs(client));
        }
        catch (Exception)
        {
            // Handle exceptions
        }
    }

    protected virtual void OnClientConnected(ClientConnectedEventArgs e)
    {
        ClientConnected?.Invoke(this, e);
    }

    public void StopListening()
    {
        listener?.Stop();
    }
}

public class NetworkUserInputOutput : IUserInputOutput
{
    private readonly TcpClient client;
    private readonly NetworkStream stream;
    private readonly StreamReader reader;
    private readonly StreamWriter writer;

    public NetworkUserInputOutput(TcpClient client)
    {
        this.client = client;
        this.stream = client.GetStream();
        this.reader = new StreamReader(stream, Encoding.UTF8);
        this.writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
    }

    public void WritePrompt(string prompt)
    {
        WriteMessage(prompt);
    }

    public void WriteMessage(string message)
    {
        writer.Write(message);
    }

    public void WriteNoun(string noun)
    {
        writer.Write(noun);
    }

    public void WriteMessageLine(string message)
    {
        writer.WriteLine(message);
    }

    public string ReadInput()
    {
        return reader.ReadLine().Trim();
    }

    public string ReadKey()
    {
        return ((char)reader.Read()).ToString();
    }
}


public interface IUserInputOutput
{
    void WritePrompt(string prompt);
    void WriteMessage(string message);
    void WriteNoun(string noun);
    void WriteMessageLine(string message);
    string ReadInput();
    string ReadKey();
}

public class UserInputOutput : IUserInputOutput
{
    public void WritePrompt(string prompt) 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(prompt);
        Console.ResetColor();
    }
    public void WriteMessage(string message) 
    {
        Console.Write(message);
    }
    public void WriteNoun(string noun) 
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(noun);
        Console.ResetColor();
    }
    public void WriteMessageLine(string message) 
    {
        Console.WriteLine(message);
    }
    public string ReadInput()
    {
        return Console.ReadLine().Trim();
    }
    public string ReadKey() 
    {
        return Console.ReadKey().KeyChar.ToString();
    }
}