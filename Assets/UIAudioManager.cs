using UnityEngine;


namespace ua.org.gdg.galera
{
  [RequireComponent(typeof(AudioSource))]
  public class UIAudioManager : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private AudioClip _promoteClip;
    [SerializeField] private AudioClip _gameOverClip;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private AudioSource _audioSource;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
    }
    
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------


    public void PlayPromoteClip()
    {
      _audioSource.clip = _promoteClip;
      _audioSource.Play();
    }

    public void PlayGameOverClip()
    {
      _audioSource.clip = _gameOverClip;
      _audioSource.Play();
    }
  }
}