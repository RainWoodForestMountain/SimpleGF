using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public class UIModule : ModuleBase
    {
        private IUILayer uiLayer;
        public override void Init(long _id)
        {
            base.Init(_id);
            uiLayer = new UGUILayerSolution();
            uiLayer.Start();
            MessageModule.instance.AddListener(MessageType.UILayersAddObject, AddObjectToLayer);
            MessageModule.instance.AddListener(MessageType.UILayersRemoveObject, RemoveObjectFromLayer);
            MessageModule.instance.AddListener(MessageType.UILayersRefresh, Refresh);
        }
        public override void Destroy()
        {
            base.Destroy();
            uiLayer.Destroy();
            MessageModule.instance.RemoveListener(MessageType.UILayersAddObject, AddObjectToLayer);
            MessageModule.instance.RemoveListener(MessageType.UILayersRemoveObject, RemoveObjectFromLayer);
            MessageModule.instance.RemoveListener(MessageType.UILayersRefresh, Refresh);
        }

        private void Refresh(Message _msg)
        {
            int _layer = -1;
            if (_msg.body != null)
            {
                if (!int.TryParse(_msg.body.key, out _layer))
                {
                    try
                    {
                        _layer = (int)Enum.Parse(typeof(UILayers), _msg.body.key);
                    }
                    catch
                    {
                        _layer = -1;
                    }
                }
            }
            uiLayer.Refresh(_layer);
        }
        private int BuildLayer (string _lay)
        {
            int _layer = -1;
            if (!int.TryParse(_lay, out _layer))
            {
                try
                {
                    _layer = (int)Enum.Parse(typeof(UILayers), _lay);
                }
                catch
                {
                    _layer = -1;
                }
            }
            return _layer;
        }
        private void AddObjectToLayer (Message _msg)
        {
            if (_msg.msgType != MessageType.UILayersAddObject) return;
            MessageBody_GameObject _body = _msg.body as MessageBody_GameObject;
            if (_body == null) return;
            int _layer = BuildLayer(_body.key);
            if (_layer < 0) return;
            if (_body.content == null) return;
            uiLayer.AddObjectToLayer(_body.content, _layer);
        }
        private void RemoveObjectFromLayer(Message _msg)
        {
            if (_msg.msgType != MessageType.UILayersAddObject) return;
            MessageBody_GameObject _body = _msg.body as MessageBody_GameObject;
            if (_body == null) return;
            int _layer = BuildLayer(_body.key);
            if (_layer < 0) return;
            if (_body.content == null) return;
            uiLayer.RemoveObjectFromLayer(_body.content, _layer);
        }
    }
}