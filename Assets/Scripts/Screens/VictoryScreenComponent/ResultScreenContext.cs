using Assets.Scripts.Managers.ScreensManager;

namespace Screens.VictoryScreenComponent
{
	public class ResultScreenContext : BaseContext
	{
		public int Cash { get; }

		public ResultScreenContext(int cash)
		{
			Cash = cash;
		}
	}
}