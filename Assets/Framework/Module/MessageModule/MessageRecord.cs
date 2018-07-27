using System;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace GameFramework
{
	internal class MessageRecord
	{
		private static readonly string SAVE_PATH = UnityEngine.Application.persistentDataPath + "/MessageRecord/";

		public bool run = true;
		public int old = -30;
        private string path;

        private FileStream fs;

		//初始化
		public MessageRecord ()
		{
			//CheckAndDeletOldFile ();
			//ReadTodayFile ();
		}
		public void Destroy ()
		{
			fs.Close ();
		}
		public void Record (Message _msg)
        {
            //File.AppendAllText(path, Utility.MergeString (_msg.ToString(), "\n"));
        }
		//读取当日文件
		private void ReadTodayFile ()
		{
			DateTime _dt = DateTime.Today;
            path = SAVE_PATH + "MsgC" + _dt.ToString ("D") + ".rc";
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
		}
		//删除过时的老旧文件
		private void CheckAndDeletOldFile ()
		{
			DateTime _dt = DateTime.Today;
			_dt.AddDays (old);
			string _filePath= SAVE_PATH + "MsgC" + _dt.ToString ("D") + ".rc";
			if (File.Exists (_filePath))
			{
				File.Delete (_filePath);
			}
		}
	}
}