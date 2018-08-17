using UnityEngine;

namespace ua.com.gdg.devfest
{
  public class FPSDisplay : MonoBehaviour
  {

    public int avgFrameRate;

    private void Awake()
    {
      Application.targetFrameRate = 100;
    }

    private void Update()
    {
      float current = 0;
      current = Time.frameCount / Time.time;
      avgFrameRate = (int) current;
      
      Debug.Log(avgFrameRate);
    }
  }
}