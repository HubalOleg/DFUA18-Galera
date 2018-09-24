using UnityEngine;

namespace ua.org.gdg.devfest
{
	[CreateAssetMenu(menuName =  "Variables/PositionVariable")]
	public class PositionVariable : ScriptableObject
	{
		[SerializeField] private string _positionName;
		[SerializeField] private int _revolutionsRequired;
	}
}