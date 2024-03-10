using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization.Scripts;
using TMPro;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Asset usage example.
	/// </summary>
	public class Example : MonoBehaviour
	{
		public Text FormattedText;
		[SerializeField] public TMP_FontAsset FontAssetEnglish;
		[SerializeField] public TMP_FontAsset FontAssetChinese;

		/// <summary>
		/// Called on app start.
		/// </summary>
		public void Awake()
		{
			LocalizationManager.Read();

			switch (Application.systemLanguage)
			{
				case SystemLanguage.Russian:
					LocalizationManager.Language = "Chinese";
					ChangeFont(FontAssetChinese);
					break;
				default:
					LocalizationManager.Language = "English";
					ChangeFont(FontAssetEnglish);
					break;
			}

			// This way you can localize and format strings from code.
			// FormattedText.text = LocalizationManager.Localize("Settings.Example.PlayTime", TimeSpan.FromHours(10.5f).TotalHours);

			// This way you can subscribe to LocalizationChanged event.

		}

		/// <summary>
		/// Change localization at runtime.
		/// </summary>
		public void SetLocalization(string localization)
		{
			LocalizationManager.Language = localization;

			Debug.Log(localization);
		}


		/// <summary>
		/// Write a review.
		/// </summary>
		public void Review()
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/120113");
		}

		public void ChangeFont(TMP_FontAsset fontObject)
		{
			foreach (TextMeshPro textMeshPro3D in GameObject.FindObjectsOfType<TextMeshPro>())
			{
				textMeshPro3D.font = fontObject;
			}

			foreach (TextMeshProUGUI textMeshProUi in GameObject.FindObjectsOfType<TextMeshProUGUI>())
			{
				textMeshProUi.font = fontObject;
			}
		}
	}
}