using UnityEngine;

namespace ua.org.gdg.galera
{
  public class PaddleBehaviour : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private GameEvent _fullRevolution;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private CheckpointBehaviour _lastEnteredCheckpoint;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
      var checkpoint = other.GetComponent<CheckpointBehaviour>();
      
      if(checkpoint == null) return;

      if (_lastEnteredCheckpoint == null)
      {
        SetLastEnteredCheckpoint(checkpoint);
        return;
      }

      if (_lastEnteredCheckpoint == checkpoint.PreviousCheckpoint)
      {
        if (checkpoint.IsFinal)
        {
          _fullRevolution.Raise();
          ResetLastEnteredCheckpoint();
          return;
        }

        SetLastEnteredCheckpoint(checkpoint);
      }
      else
      {
        ResetLastEnteredCheckpoint();
      }
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private void ResetLastEnteredCheckpoint()
    {
      _lastEnteredCheckpoint = null;
    }

    private void SetLastEnteredCheckpoint(CheckpointBehaviour checkpoint)
    {
      _lastEnteredCheckpoint = checkpoint;
    }
  }
}