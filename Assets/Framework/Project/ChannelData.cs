using System.Text;

namespace GameFramework
{
    public class ChannelDataPackage
    {
        public int channelID;
        public string appName;
        public string wxappID;
        public string buglyIOSID;
        public string buglyAndroidID;
        public string talkingDataID;
        public string shareID;
        public string sharesecID;
        public string ocAppTeamID;
    }
    public class ChannelData
    {
        public static ChannelDataPackage current;
        public static string[] channels = new string[] { "默认" };

        public static void Refresh()
        {
            current = new ChannelDataPackage();
            string _json = Utility.LoadResourceConfigFile(ProjectDatas.NAME_CHANNEL_FILE);

            if (string.IsNullOrEmpty(_json)) current = new ChannelDataPackage();
            else current = LitJson.JsonMapper.ToObject<ChannelDataPackage>(_json);
        }
        private static void Record()
        {
            string _json = LitJson.JsonMapper.ToJson(current);
#if UNITY_EDITOR
            Utility.RecordConfigTextFile(ProjectDatas.NAME_CHANNEL_FILE, _json);
#endif
        }

        public static string channel;

        public static void SetByBundleid(string _bundleid)
        {
#if UNITY_EDITOR
            Refresh();
            channel = _bundleid;
            switch (_bundleid)
            {
                case "com.tencent.tmgp.mmddszfy":
                    current.channelID = 1002; //应用宝
                    current.appName = "十洲风云";
                    current.wxappID = "wx4acbb84d2d1f6ffb";
                    current.buglyIOSID = "4cc3e626ff";
                    current.buglyAndroidID = "02e910a829";
                    current.talkingDataID = "5C5C8DC5C9864730B58B5126AF875C30";
                    current.shareID = "2639a90d6ba30";
                    current.sharesecID = "385be6524a600df4184f675302d9ea29";
                    current.ocAppTeamID = "";
                    break;
                case "com.hjhd.szfy":
                    current.channelID = 1;
                    current.appName = "十洲风云";
                    current.wxappID = "wx4acbb84d2d1f6ffb";
                    current.buglyIOSID = "4cc3e626ff";
                    current.buglyAndroidID = "02e910a829";
                    current.talkingDataID = "5C5C8DC5C9864730B58B5126AF875C30";
                    current.shareID = "2639a90d6ba30";
                    current.sharesecID = "385be6524a600df4184f675302d9ea29";
                    current.ocAppTeamID = "NYZA658JU6";
                    break;
                case "com.hjhd.aysdp":
                    current.channelID = 2;
                    break;
            }
            Record();
#endif
        }
        public static void SetChannelByIndex(int _index)
        {
            //string _cs = channels[_index];
            //Record();
        }
    }
}