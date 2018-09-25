using UnityEngine;

namespace ua.org.gdg.galera
{
  public class PaddleBehaviour : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private GameEvent _fullRevolution;
    [SerializeField] private int _checkpointsNumber;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private CheckpointBehaviour _lastEnteredCheckpoint;
    private int _checkpointCombo;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
      var checkpoint = other.GetComponent<CheckpointBehaviour>();
      
      if(checkpoint == null) return;

      if (_lastEnteredCheckpoint != null && _lastEnteredCheckpoint == checkpoint.PreviousCheckpoint)
      {
        if (IsComboCompleted())
        {
          _fullRevolution.Raise();
          ResetCombo();
        }
        
        _checkpointCombo++;
      }
      else
      {
        ResetCombo();
      }
      
      SetLastEnteredCheckpoint(checkpoint);
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------
    
    private bool IsComboCompleted()
    {
      return _checkpointCombo == _checkpointsNumber;
    }

    private void ResetCombo()
    {
      _checkpointCombo = 0;
    }

    private void SetLastEnteredCheckpoint(CheckpointBehaviour checkpoint)
    {
      _lastEnteredCheckpoint = checkpoint;
    }
  }
}