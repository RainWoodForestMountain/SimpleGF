using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.InteropServices;
using LitJson;

namespace GameFramework
{
    public class PlantformSolution : MonoBase
    {
        public void PlantformMsgCome(string _json)
        {
            Utility.Log("--------------------PlantformMsgCome: " + _json);
            MessageModule.instance.Recevive(string.Empty, MessageType.PlatformResponse, _json);
        }
        public void SendTo(Message _msg)
        {
            MessageBody_String _s = _msg.body as MessageBody_String;
            if (_s == null)
            {
                Utility.LogError("给平台发送的json字符串为空！");
                return;
            }

            //特殊的部分
            JsonData _jsonData = JsonMapper.ToObject(_s.content);
            switch (_jsonData["keyword"].Value)
            {
                //bugly需要双端注册
                case "BuglyInit":
                    BuglyInit.Init(_jsonData["appid"].Value);
                    break;
                    //return;
            }

            //派发给第三方
            SendTo(_s.content);
        }
        public void SendTo(string _json)
        {
#if UNITY_EDITOR
#elif UNITY_IOS || UNITY_IPHONE
            SendToPlantform(_json);
#elif UNITY_ANDROID
			Context.Call ("SendToPlantform", _json);
#endif
        }

#if UNITY_IOS || UNITY_IPHONE
        [DllImport("__Internal")]
        public static extern void SendToPlantform(string _json);
#endif

#if UNITY_ANDROID
        public static AndroidJavaObject Context
        {
            get
            {
                if (context == null)
                {
                    using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        context = jc.GetStatic<AndroidJavaObject>("currentActivity");
                    }
                }
                return context;
            }
        }
        public static AndroidJavaObject context;
#endif
    }
}
