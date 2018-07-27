using System;
using BestHTTP;
using LitJson;

namespace GameFramework
{
    public class HTTPSolution : INetInteractive
    {
        public IProtocol protocol { get; private set; }
        public string host { get; private set; }
        public int port { get; private set; }

        private Action<ByteBuffer> onGetted;
        private string fullUrl;

        public HTTPSolution (string _url, int _port = 80)
        {
            host = _url;
            port = _port;
            fullUrl = Utility.MergeString(host, ":", port);
        }
        
        public void Send(ByteBuffer buffer)
        {
            string _json = buffer.ReadString();
            Dictionary<string, string> _kvs = LitJson.JsonMapper.ToObject<Dictionary<string, string>>(_json);

            //两个必要参数：http请求类型和子路径
            string _met = _kvs.ContainsKey("__methods") ? _kvs["__methods"] : "get";
            _met = _met.ToLower();
            _kvs.Remove("__methods");
            string _suburl = _kvs.ContainsKey("__suburl") ? _kvs["__suburl"] : string.Empty;
            _kvs.Remove("__suburl");

            _suburl = Utility.MergeString(fullUrl, _suburl);
            if (_met.Equals ("get"))
            {
                SendGet(_suburl, _kvs);
            }
            else
            {
                SendPost(_suburl, _kvs);
            }
        }

        private void SendGet(string _subUrl, Dictionary<string, string> _kvs)
        {
            bool _first = true;
            foreach (System.Collections.Generic.KeyValuePair<string, string> _kvp in _kvs)
            {
                if (_first) _subUrl = Utility.MergeString(_subUrl, "?", _kvp.Key, "=", _kvp.Value);
                else _subUrl = Utility.MergeString(_subUrl, "&", _kvp.Key, "=", _kvp.Value);
                _first = false;
            }
            HTTPRequest _request = HTTPManager.SendRequest(_subUrl, HTTPMethods.Get, true, true, OnResponse);
            _request.Send();
        }

        private void SendPost(string _subUrl, Dictionary<string, string> _kvs)
        {
            HTTPRequest _request = HTTPManager.SendRequest(_subUrl, HTTPMethods.Post, true, true, OnResponse);
            foreach (System.Collections.Generic.KeyValuePair<string, string> _kvp in _kvs)
            {
                _request.AddField(_kvp.Key, _kvp.Value);
            }
            _request.Send();
        }

        private void OnResponse (HTTPRequest _request, HTTPResponse _response)
        {
            if (_request == null || _response == null)
            {
                //TODO:log
                return;
            }
            ByteBuffer _byteBuffer = new ByteBuffer();
            _byteBuffer.WriteBytes(_response.Data);
            onGetted(_byteBuffer);
        }

        public void OnGetted(Action<ByteBuffer> _ac)
        {
            onGetted = _ac;
        }

        public void Close()
        {
        }

        public void Connect()
        {
        }

        public void Set(JsonData _json)
        {
        }

        public void OnMessage(Action<Message> _ac)
        {
        }
    }
}