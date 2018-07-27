using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.IO;



namespace GameFramework 
{
	public partial class HotUpdateModule : ModuleBase
    {
        private HotUpdateSolutionTotal total;
        private HotUpdateSolutionFileList filelist;

        public override void Init(long _id)
        {
            base.Init(_id);

            total = new HotUpdateSolutionTotal();
            filelist = new HotUpdateSolutionFileList();
            MessageModule.instance.AddListener(MessageType.StartHotupdate, StartHotupdate);
        }
        public override void Destroy()
        {
            base.Destroy();
            
            MessageModule.instance.RemoveListener(MessageType.StartHotupdate, StartHotupdate);
        }
        private void StartHotupdate(Message _msg)
        {
            MessageBody_String _s = _msg.body as MessageBody_String;
            LitJson.JsonData _data = LitJson.JsonMapper.ToObject(_s.content);
            if (_data["op"] == null) return;
            switch (_data["op"].Value)
            {
                case "total":
                    total.OnStart(_data);
                    break;
                case "filelist":
                    filelist.OnStart(_data);
                    break;
            }
        }
    }
}