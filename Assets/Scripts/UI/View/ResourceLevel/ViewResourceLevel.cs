using System;
using ResourceSystem;
using TMPro;
using UnityEngine;

namespace View.ResourceLevel
{
	public class ViewResourceLevel
	{
		public RectTransform TextMoneyTransform => _textMoney.rectTransform;

		private readonly TextMeshProUGUI _textMoney;
		private readonly ResourceManagerLevel _resourceManagerLevel;
		private readonly ViewResourceAnimationController _viewResourceAnimationController;

		private int _currentAmount;

		public ViewResourceLevel(Settings settings, ResourceManagerLevel resourceManagerLevel, ViewResourceAnimationController viewResourceAnimationController)
		{
			_textMoney = settings.TextMoney;
			_resourceManagerLevel = resourceManagerLevel;
			_viewResourceAnimationController = viewResourceAnimationController;

			_currentAmount = _resourceManagerLevel.GetResource(ResourceType.Cash).Amount;
			_textMoney.text = _currentAmount.ToString();
		}

		public void AddResource(int price)
		{
			_currentAmount += price;
			SetMoney();
		}

		public void DecreaseResource(int price)
		{
			_currentAmount -= price;
			SetMoney();
		}

		private void SetMoney()
		{
			_textMoney.text = _currentAmount.ToString();
			_viewResourceAnimationController.PlayAnimation();
		}

		[Serializable]
		public class Settings
		{
			public TextMeshProUGUI TextMoney;
		}
	}
}