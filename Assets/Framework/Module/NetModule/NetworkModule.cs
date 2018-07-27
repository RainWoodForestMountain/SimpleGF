using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using LitJson;

namespace GameFramework
{
    public class NetworkModule : ModuleBase
    {
        private long updateEventId;
        private Dictionary<string, INetInteractive> solution;
        private Queue<ByteBuffer>  mEvents = new Queue<ByteBuffer>();
        private Queue<Message> mMsgs = new Queue<Message>();

        public override void Init(long _id)
        {
            base.Init(_id);
            mEvents = new Queue<ByteBuffer>();
            solution = new Dictionary<string, INetInteractive>();
            updateEventId = TimeModule.instance.Register(Update, -1, -1);
            
            MessageModule.instance.AddListener(MessageType.ServerConnect, ServerConnect);
            MessageModule.instance.AddListener(MessageType.ServerRequest, ServerRequest);
            MessageModule.instance.AddListener(MessageType.ServerClose, ServerClose);
            //MessageModule.instance.AddListener(MessageType.NetConnectCheck, StartNetCheck);
        }
        public override void Destroy()
        {
            base.Destroy();
            foreach (var _kvp in solution)
            {
                _kvp.Value.Close();
            }

            TimeModule.instance.RemoveTimeNodeByID(updateEventId);
            MessageModule.instance.RemoveListener(MessageType.ServerConnect, ServerConnect);
            MessageModule.instance.RemoveListener(MessageType.ServerRequest, ServerRequest);
            MessageModule.instance.RemoveListener(MessageType.ServerClose, ServerClose);
            //MessageModule.instance.RemoveListener(MessageType.NetConnectCheck, StartNetCheck);
        }

        private void ServerClose(Message _msg)
        {
            if (_msg.body == null) return;
            MessageBody_String _bb = _msg.body as MessageBody_String;
            if (_bb == null)
            {
                Utility.LogError("没有请求数据体或者消息结构错误：", _msg.body.key);
                return;
            }

            JsonData _data = JsonMapper.ToObject(_bb.content);
            if (_data["name"] == null)
            {
                Utility.LogError("必须指定服务器名称！");
                return;
            }

            CloseConnect(_data["name"].Value);
        }
        private void ServerConnect (Message _msg)
        {
            if (_msg.body == null) return;
            MessageBody_String _bb = _msg.body as MessageBody_String;
            if (_bb == null)
            {
                Utility.LogError("没有请求数据体或者消息结构错误：", _msg.body.key);
                return;
            }

            JsonData _data = JsonMapper.ToObject(_bb.content);
            if (_data["name"] == null)
            {
                Utility.LogError("必须指定服务器名称！");
                return;
            }
            if (_data["type"] == null)
            {
                Utility.LogError("未指定需要连接的服务器类型！");
                return;
            }
            if (_data["ip"] == null || _data["port"] == null)
            {
                Utility.LogError("参数错误！");
                return;
            }

            Connect(_data);
        }
        private void CloseConnect(string _name)
        {
            if (solution.ContainsKey(_name))
            {
                INetInteractive _net = solution[_name];
                solution.Remove(_name);
                _net.Close();
            }
        }
        private void Connect (JsonData _data)
        {
            if (solution.ContainsKey(_data["name"].Value)) return;
            INetInteractive _net = null;
            switch (_data["type"].Value)
            {
                case "socket":
                    _net = new SocketSolution(_data["ip"].Value, _data["port"].IntValue);
                    break;
                case "http":
                    _net = new HTTPSolution(_data["ip"].Value, _data["port"].IntValue);
                    break;
                default:
                    return;
            }
            
            _net.OnGetted(AddEvent);
            _net.OnMessage(AddMessage);
            _net.Connect();
            solution.Add(_data["name"].Value, _net);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        private void ServerRequest(Message _msg)
        {
            MessageBody_ByteBuffer _bb = _msg.body as MessageBody_ByteBuffer;
            if (_bb == null)
            {
                Utility.LogError("没有请求数据体或者消息结构错误：", _msg.body.key);
                return;
            }

            string _key = _msg.body.key;
            if (!solution.ContainsKey(_key))
            {
                Utility.LogError("不存在的服务器连接：", _key);
                return;
            }

            ByteBuffer _buffer = new ByteBuffer(_bb.content.ToBytes());
            solution[_key].Send(_buffer);
        }

        private void AddEvent (ByteBuffer _byteBuffer)
        {
            mEvents.Enqueue(_byteBuffer);
        }
        private void AddMessage(Message _msg)
        {
            mMsgs.Enqueue(_msg);
        }
        /// <summary>
        /// 发送广播
        /// </summary>
        private void Update()
        {
            while (mEvents.Count > 0)
            {
                MessageModule.instance.Recevive(string.Empty, MessageType.ServerResponse, mEvents.Dequeue());
            }
            while (mMsgs.Count > 0)
            {
                MessageModule.instance.Recevive(mMsgs.Dequeue());
            }
        }

        //private void StartNetCheck(Message _msg)
        //{
        //    MessageBody_String _s = _msg.body as MessageBody_String;
        //    TimeModule.instance.RunIEnumerator(NetCheck(_s.content));
        //}

        //private const int netCheckSpaceTime = 2;
        //private IEnumerator NetCheck(string _url)
        //{
        //    Ping _ping = null;
        //    int _ac = 0;
        //    float _to = 10;
        //    float _spt = netCheckSpaceTime / _to;

        //    while (true)
        //    {
        //        if (Application.internetReachability == NetworkReachability.NotReachable)
        //        {
        //            //ping失败，无网络
        //            MessageModule.instance.Recevive("lostconnect", MessageType.NetLostConnect, "lostconnect");
        //            yield return new WaitForSeconds(netCheckSpaceTime);
        //            continue;
        //        }
        //        _ping = new Ping(_url);
        //        while (!_ping.isDone)
        //        {
        //            yield return new WaitForSeconds(_spt);
        //            _ac++;
        //            if (_ac > _to)
        //            {
        //                //ping失败，无网络
        //                MessageModule.instance.Recevive("lostconnect", MessageType.NetLostConnect, "lostconnect");
        //                break;
        //            }
        //        }
        //        if (_ping.isDone)
        //        {
        //            MessageModule.instance.Recevive("lostconnect", MessageType.NetDelay, _ac.ToString());
        //        }
        //        _ping.DestroyPing();
        //        _ac = 0;
        //        //2秒检查一次
        //        yield return new WaitForSeconds(netCheckSpaceTime);
        //    }
        //}
    }
}