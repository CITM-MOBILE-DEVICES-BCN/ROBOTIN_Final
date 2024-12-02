using TimerModule;
using TMPro;
using UnityEngine;

namespace TimerSampleScene
{
	public class TimerViewValues : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI currentTime;
		[SerializeField]
		private TextMeshProUGUI maxTime;
		

		public void UpdateView(Timer timer, TimerService timerService)
		{
			currentTime.text = $"Duration Time: {timer.Duration}";
			maxTime.text = $"Remaining Time: {timerService.GetTimerElapsedTime(timer)}";
		}
	}
}
