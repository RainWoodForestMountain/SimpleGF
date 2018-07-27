using System;
using LuaInterface;
using UnityEngine;
using UnityEngine.Video;

namespace GameFramework {
	[RequireComponent(typeof(VideoPlayer))]
	public class VideoPlayerEvent : MonoBase {
		private VideoPlayer videoPlayer;

		private Action playendAction = null;
		private bool runOnce = false;

		public bool IsPlayEnd {
			get {
				return videoPlayer.frame >= (long) videoPlayer.frameCount ;
			}
		}
		public void SetPlayerEnd(Action callback) {
			playendAction = callback;
		}
		
		private void Awake() {
			videoPlayer = GetComponent<VideoPlayer>();
			runOnce = false;
		}

		private void Update() {
			if (runOnce) {
				return;
			}
			
			if (IsPlayEnd) {
				runOnce = true;
				if (playendAction != null) {
					playendAction();
				}
			}
		}
	}
}