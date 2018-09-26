using UnityEngine;

namespace ua.org.gdg.galera
{
	[CreateAssetMenu(menuName =  "Variables/PositionVariable")]
	public class PositionVariable : ScriptableObject
	{
		[SerializeField] public string PositionName;
		[SerializeField] public int RevolutionsRequired;
		[SerializeField] public GameObject Paddle;
	}
}