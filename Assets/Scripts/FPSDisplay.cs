using UnityEngine;

namespace ua.com.gdg.devfest
{
  public class FPSDisplay : MonoBehaviour
  {

    float deltaTime = 0.0f;
 
    void Update()
    {
      deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
 
    void OnGUI()
    {
      int w = Screen.width, h = Screen.height;
 
 
      float msec = deltaTime * 1000.0f;
      float fps = 1.0f / deltaTime;
      string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
      Debug.Log(text);
    }
  }
}