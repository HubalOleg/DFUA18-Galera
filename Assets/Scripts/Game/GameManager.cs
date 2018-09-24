using UnityEngine;
using UnityEngine.UI;

namespace ua.org.gdg.devfest
{
	public class GameManager : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private Text _revolutionsText;
		[SerializeField] private IntVariable _revolutionNumbers;

		//---------------------------------------------------------------------
		// Events
		//---------------------------------------------------------------------

		public void OnFullRevolution()
		{
			_revolutionNumbers.RuntimeValue += 1;
			_revolutionsText.text = "Revolutions Number: " + _revolutionNumbers.RuntimeValue;
		}
	}
}