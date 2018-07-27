using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using LitJson;

namespace GameFramework
{
    public class SocketSolution : INetInteractive
    {
        private const int MAX_READ = 2048;

        private IProtocol protocol = new Protocol_Protobuf_4L_4ID_4Sign();
        private TcpClient client = null;
        private NetworkStream outStream = null;
        private MemoryStream memStream;
        private BinaryReader reader;
        private byte[] byteBuffer = new byte[MAX_READ];
        private bool dealSendQueue = false;
        private long dealSendQueueID = 0;
        
        public string host { get; private set; }
        public int port { get; private set; }
        protected virtual string name { get; set; }

        protected bool isConnected { get { return client != null && client.Connected; } }

        private Action<ByteBuffer> onGetted;
        private Action<Message> onMessage;

        private System.Collections.Generic.Queue<ByteBuffer> sendQueue = new System.Collections.Generic.Queue<ByteBuffer>();


        public SocketSolution(string _host, int _port)
        {
            host = _host;
            port = _port;
            name = "socket";
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        private void ConnectServer()
        {
            if (client != null) client.Close();
            client = null;
            try
            {
                IPAddress[] address = Dns.GetHostAddresses(host);
                if (address.Length == 0)
                {
                    Debug.LogError("host invalid");
                    return;
                }
                IPAddress _one = address[(new System.Random()).Next(0, address.Length)];
                if (_one.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    client = new TcpClient(AddressFamily.InterNetworkV6);
                }
                else
                {
                    client = new TcpClient(AddressFamily.InterNetwork);
                }
                client.SendTimeout = 5000;
                client.ReceiveTimeout = 5000;
                client.NoDelay = true;
                client.BeginConnect(_one, port, new AsyncCallback(OnConnect), null);
            }
            catch (Exception e)
            {
                Close();
                Utility.LogError(e.Message);
            }
        }
        /// <summary>
        /// 连接上服务器
        /// </summary>
        private void OnConnect(IAsyncResult asr)
        {
            outStream = client.GetStream();
            client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
            onMessage(MessageFactory.ProduceMessage("", MessageType.ServerConnected, "On Socket Connected"));
        }
        /// <summary>
        /// 写数据
        /// </summary>
        private void WriteMessage(byte[] _message)
        {
            if (client != null && client.Connected)
            {
                outStream.BeginWrite(_message, 0, _message.Length, new AsyncCallback(OnWrite), null);
            }
            else
            {
                Utility.LogError("client.connected----->>false  client != null && client.Connected");
            }
        }
        /// <summary>
        /// 读取消息
        /// </summary>
        private void OnRead(IAsyncResult _asr)
        {
            int _bytesRead = 0;
            try
            {
                lock (client.GetStream())
                {         
                    //读取字节流到缓冲区
                    _bytesRead = client.GetStream().EndRead(_asr);
                }
                if (_bytesRead < 1)
                {                
                    //包尺寸有问题，断线处理
                    OnDisconnected("OnDisconnected");
                    return;
                }
                OnReceive(byteBuffer, _bytesRead);   
                //分析数据包内容，抛给逻辑层
                lock (client.GetStream())
                {         
                    //分析完，再次监听服务器发过来的新消息
                    Array.Clear(byteBuffer, 0, byteBuffer.Length);
                    //清空数组
                    client.GetStream().BeginRead(byteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
                }
            }
            catch (Exception ex)
            {
                OnDisconnected(ex.Message);
            }
        }
        /// <summary>
        /// 丢失链接
        /// </summary>
        private void OnDisconnected(string msg)
        {
            //关掉客户端链接
            onMessage(MessageFactory.ProduceMessage("", MessageType.ServerDisconnect, msg));
            Close();
        }
        /// <summary>
        /// 向链接写入数据流
        /// </summary>
        private void OnWrite(IAsyncResult r)
        {
            try
            {
                outStream.EndWrite(r);
            }
            catch (Exception ex)
            {
                Utility.LogError("OnWrite--->>>Error", ex.Message);
            }
        }
        /// <summary>
        /// 接收到消息
        /// </summary>
        private void OnReceive(byte[] _bytes, int _length)
        {
            memStream.Seek(0, SeekOrigin.End);
            memStream.Write(_bytes, 0, _length);
            //Reset to beginning
            memStream.Seek(0, SeekOrigin.Begin);
            //消息体：先判断是否大于头部
            while (RemainingBytes() > protocol.totalHead)
            {
                //直接抛给业务层处理
                ByteBuffer _buffer = protocol.Get(memStream, reader);
                if (_buffer != null)
                {
                    if (onGetted != null) onGetted(_buffer);
                }
                else
                {
                    return;
                }

                ////这里读取了4字节的消息头部
                //int _messageLen = reader.ReadInt32();
                ////回滚4字节消息头，计算总体长度
                //memStream.Position = memStream.Position - 4;
                //// 总长 = 消息偏移量 + 消息体长
                //int _totalLength = protocol.totalHead + _messageLen;
                //if (HasEnough(_totalLength))
                //{
                    //BinaryWriter _writer = new BinaryWriter(_ms);
                    //_writer.Write(reader.ReadBytes(_totalLength));
                    //_ms.Seek(0, SeekOrigin.Begin);
                    //OnReceivedMessage(_ms);
                //}
                //else
                //{
                //    return;
                //}
            }
            //Create a new stream with any leftover bytes
            byte[] _leftover = reader.ReadBytes((int)RemainingBytes());
            memStream.SetLength(0);
            //Clear
            memStream.Write(_leftover, 0, _leftover.Length);
        }
        /// <summary>
        /// 剩余的字节
        /// </summary>
        private long RemainingBytes()
        {
            return memStream.Length - memStream.Position;
        }
        private bool HasEnough(long _c)
        {
            return memStream.Length >= _c;
        }
        /// <summary>
        /// 接收到消息
        /// </summary>
        private void OnReceivedMessage(MemoryStream _ms)
        {
            BinaryReader _r = new BinaryReader(_ms);
            //直接抛给业务层处理
            ByteBuffer _buffer = protocol.Get(_ms, _r);
            
            if (onGetted != null) onGetted(_buffer);
        }
        /// <summary>
        /// 会话发送
        /// </summary>
        private void SessionSend(byte[] bytes)
        {
            WriteMessage(bytes);
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        public void Close()
        {
            //主动关闭链接不发送断开消息
            reader.Close();
            memStream.Close();
            if (client != null)
            {
                if (client.Connected) client.Close();
                client = null;
            }
            dealSendQueue = false;
        }
        /// <summary>
        /// 发送连接请求
        /// </summary>
        public virtual void Connect()
        {
            memStream = new MemoryStream();
            reader = new BinaryReader(memStream);
            ConnectServer();
        }
        /// <summary>
        /// 处理请求消息队列
        /// </summary>
        private void DealSendQueue ()
        {
            if (isConnected)
            {
                while (sendQueue.Count > 0)
                {
                    Send(sendQueue.Dequeue());
                }
                Utility.RemoveTimer(dealSendQueueID);
                dealSendQueue = false;
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        public virtual void Send(ByteBuffer buffer)
        {
            if (!isConnected)
            {
                //服务器暂未链接，压入请求队列
                sendQueue.Enqueue(buffer);
                if (!dealSendQueue)
                {
                    Connect();
                    dealSendQueueID = Utility.CreateTimer(DealSendQueue, -1, -1, 0);
                    dealSendQueue = true;
                }
                return;
            }
            
            byte[] _array = protocol.Set(buffer);

            SessionSend(_array);
            buffer.Close();
        }
        /// <summary>
        /// 设置回调消息
        /// </summary>
        public virtual void OnGetted(Action<ByteBuffer> _ac)
        {
            onGetted = _ac;
        }

        public void OnMessage(Action<Message> _ac)
        {
            onMessage = _ac;
        }
    }
}