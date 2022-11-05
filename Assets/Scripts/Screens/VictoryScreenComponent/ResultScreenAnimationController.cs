using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Screens.VictoryScreenComponent
{
	public class ResultScreenAnimationController : MonoBehaviour
	{
		[SerializeField]
		private float _conversionTime;
		[SerializeField]
		private TextMeshProUGUI _textMoney;

		[Header("Появление экрана")]
		[SerializeField]
		private float _screenTime;
		[SerializeField]
		private Vector3 _startScale;
		[SerializeField]
		private Vector3 _endScale;
		[SerializeField]
		private Transform _screen;
		[Space(20f)]
		
		private Sequence _sequence;

		public void PlayAnimation(int amountCash)
		{
			_sequence = DOTween.Sequence();

			_screen.localScale = _startScale;
			_sequence.Append(_screen.DOScale(_endScale, _screenTime));
			_sequence.AppendCallback(() =>
			{ 
				PlayAnimationMoneyTransfer(amountCash);
			});
		}

		private async UniTaskVoid PlayAnimationMoneyTransfer(int amountCash)
		{
			var currentTime = 0f;

			while (currentTime < _conversionTime)
			{
				currentTime += Time.deltaTime;
				
				var currentAmountCash = Mathf.RoundToInt(Mathf.Lerp(0, amountCash, currentTime / _conversionTime));
				
				_textMoney.text = currentAmountCash.ToString();

				await UniTask.Yield(PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
			}
			
			_textMoney.text = amountCash.ToString();
		}

		private void OnDestroy()
		{
			_sequence?.Kill();
		}
	}
}