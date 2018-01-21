using UnityEngine;

namespace Assets.Scripts.Cam.Effects {
	[ExecuteInEditMode]
	[RequireComponent(typeof(UnityEngine.Camera))]
	[AddComponentMenu("Image Effects/Custom/Dither")]
	public class Dither : MonoBehaviour {
		public Texture2D pattern;

		[Range(0.0f, 1.0f)]
		public float threshold = 0.45f;
		[Range(0.0f, 1.0f)]
		public float strength = 0.45f;
		public int scale = 1;

		private Material m_material;
		private Shader shader;

		private Material material {
			get {
				if (m_material == null) {
					shader = Shader.Find("Oxysoft/Dither");
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

		private void Start() {
			if (!SystemInfo.supportsImageEffects)
				enabled = false;
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material) {
				material.SetTexture("_Dither", pattern);
				material.SetInt("_Width", scale);
				material.SetInt("_Height", scale);
				material.SetFloat("_Threshold", threshold);
				material.SetFloat("_Strength", strength);
				Graphics.Blit(src, dest, material);
			}
		}

		private void OnDisable() {
			if (m_material)
				DestroyImmediate(m_material);
		}
	}
}