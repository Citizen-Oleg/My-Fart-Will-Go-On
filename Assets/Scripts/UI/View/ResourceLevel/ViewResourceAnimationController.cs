using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace View.ResourceLevel
{
	public class ViewResourceAnimationController : IDisposable
	{
		private readonly Settings _settings;
		private readonly TextMeshProUGUI _monetText;

		private Sequence _sequence;
		private bool _isPlayAnimation;
		
		public ViewResourceAnimationController(Settings settings, ViewResourceLevel.Settings settingsView)
		{
			_settings = settings;
			_monetText = settingsView.TextMoney;
		}

		public void PlayAnimation()
		{
			if (_isPlayAnimation)
			{
				return;
			}

			_isPlayAnimation = true;
			_sequence = DOTween.Sequence();
			var startScale = _monetText.transform.localScale;
			_sequence.Append(_monetText.transform.DOScale(_settings.ScaleIncrease, _settings.ResizingTime / 2));
			_sequence.Append(_monetText.transform.DOScale(startScale, _settings.ResizingTime / 2));
			_sequence.AppendCallback(() => _isPlayAnimation = false);
		}

		public void Dispose()
		{
			_sequence?.Kill();
		}
		
		[Serializable]
		public class Settings
		{
			public float ResizingTime;
			public Vector3 ScaleIncrease;
		}
	}
}