using ua.org.gdg.devfest;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//---------------------------------------------------------------------
	// Editor
	//---------------------------------------------------------------------
	
	[SerializeField] public Text _revolutionsText;
	public IntVariable _revolutionNumbers;
	
	//---------------------------------------------------------------------
	// Events
	//---------------------------------------------------------------------

	public void OnFullRevolution()
	{
		_revolutionNumbers.RuntimeValue += 1;
		_revolutionsText.text = "Revolution Numbers: " + _revolutionNumbers.RuntimeValue;
	}
}
