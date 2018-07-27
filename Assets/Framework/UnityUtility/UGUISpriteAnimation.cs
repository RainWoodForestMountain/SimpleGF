using UnityEngine;
using UnityEngine.UI;

namespace GameFramework {
	[RequireComponent(typeof(Image))]

	public class UGUISpriteAnimation : MonoBase {
		[SerializeField] [Tooltip("整图源文件")]
		private Texture2D source;

		[SerializeField] [Tooltip("水平切割数量，针对源文件，先横后竖")]
		private uint horizontalCount = 1;

		[SerializeField] [Tooltip("竖直切割数量，针对源文件，先横后竖")]
		private uint verticalCount = 1;

		[SerializeField] [Tooltip("间隔帧")]
		private uint frame = 5;

		[SerializeField] [Tooltip("是否循环")]
		private bool loop = true;

		[SerializeField] [Tooltip("如果是单次播放，是否在完成后关闭")]
		private bool disable = false;

		[SerializeField] [Tooltip("如果是单次播放，是否在完成后删除")]
		private bool destroy = false;

		[SerializeField] [Tooltip("是否播放时自动调整sprite大小")]
		private bool auto_reset_size = false;

		[SerializeField] [Tooltip("sprite列表")]
		private Sprite[] source_sprite;

		[SerializeField]
		private List<Sprite> sprites = new List<Sprite>();

		private Image image;
		private int current = 0;
		private uint runFrame = 0;

		private void Awake() {
			image = GetComponent<Image>();
			if (source_sprite == null || source_sprite.Length == 0) {
				SetSprite(source);
			}
			else {
				SetSprite(source_sprite);
			}
		}

		private void Update() {
			if (sprites.Count == 0) return;

			//第一帧，初始化
			if (runFrame == 0) {
				image.sprite = sprites[current];
				if (auto_reset_size) {
					image.SetNativeSize();
				}
			}

			//添加帧
			runFrame++;
			if (runFrame >= frame) {
				runFrame = 0;
				current++;
				//到达结束点检查
				if (current >= sprites.Count) {
					current = 0;
					if (loop) return;
					if (disable) gameObject.SetActive(false);
					if (destroy) Destroy(gameObject);
				}
			}
		}

		private void OnDestroy() {
			Resources.UnloadAsset(source);
		}

		public void SetSprite(Texture2D _texture2D) {
			if (_texture2D == null) {
				if (image != null && image.enabled) {
					image.enabled = false;
				}

				return;
			}

			if (image != null && !image.enabled) {
				image.enabled = true;
			}

			int _width = _texture2D.width / (int) horizontalCount;
			int _height = _texture2D.height / (int) verticalCount;
			Sprite _temp = null;
			sprites.Clear();
			for (int i = 0; i < verticalCount; i++) {
				for (int j = 0; j < horizontalCount; j++) {
					_temp = Sprite.Create(_texture2D,
						new Rect(j * _width, i * _height, _width, _height),
						new Vector2(0.5f, 0.5f));
					sprites.Add(_temp);
				}
			}

			if (sprites.Count > 0) {
				image.sprite = sprites[0];
			}
		}

		public void SetSprite(Sprite[] _sprites) {
			if (_sprites == null || _sprites.Length == 0) {
				if (image != null && image.enabled) {
					image.enabled = false;
				}

				return;
			}
			if (image != null && !image.enabled) {
				image.enabled = true;
			}
			sprites.Clear();
			sprites.AddRange(_sprites);
			if (sprites.Count > 0) {
				image.sprite = sprites[0];
			}
		}
	}
}