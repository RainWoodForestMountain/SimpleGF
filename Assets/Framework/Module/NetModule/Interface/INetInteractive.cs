namespace GameFramework
{
    public interface INetInteractive
    {
        string host { get; }
        int port { get; }
        void Send(ByteBuffer buffer);
        void OnGetted(System.Action<ByteBuffer> _ac);
        void OnMessage(System.Action<Message> _ac);
        void Close();
        void Connect();
    }
}