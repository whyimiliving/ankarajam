using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class UdpReceiver : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;
    public TextMeshProUGUI uiText;

    public int port = 5005; // Dinlenecek port (Python kodundaki gibi)

    void Start()
    {
        // Sadece localhost'u dinle
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Loopback, port);
        udpClient = new UdpClient(localEndPoint);

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        uiText.text =($"Listening on 127.0.0.1:{port}...");
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Loopback, port);

        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string receivedText = Encoding.UTF8.GetString(data);
                uiText.text =($"Received from {remoteEndPoint}: {receivedText}");
              
            }
            catch (SocketException ex)
            {
                uiText.text =($"Socket closed: {ex.Message}");
                break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        if (udpClient != null)
            udpClient.Close();
    }
}