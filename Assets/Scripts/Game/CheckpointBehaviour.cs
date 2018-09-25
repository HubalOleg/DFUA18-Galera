using UnityEngine;

namespace ua.org.gdg.galera
{
	public class CheckpointBehaviour : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private CheckpointBehaviour _previousCheckpoint;
		[SerializeField] private bool _isFinal;
		
		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------

		public bool IsFinal
		{
			get { return _isFinal; }
		}

		public CheckpointBehaviour PreviousCheckpoint
		{
			get { return _previousCheckpoint; }
		}
	}
}