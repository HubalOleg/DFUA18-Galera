using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {
  
  //---------------------------------------------------------------------
  // Internal
  //---------------------------------------------------------------------

  private Text _text;
  private const string MESSAGE = "You have been promoted to {0}!";
  
  //---------------------------------------------------------------------
  // Messages
  //---------------------------------------------------------------------

  private void Awake()
  {
    _text = GetComponent<Text>();
  }
  
  //---------------------------------------------------------------------
  // Public
  //---------------------------------------------------------------------

  public void ShowText(float time, string position)
  {
    _text.text = string.Format(MESSAGE, position);
    StartCoroutine(FadeTextToFullAlpha(time));
  }
  
  //---------------------------------------------------------------------
  // Helpers
  //---------------------------------------------------------------------

  private IEnumerator FadeTextToFullAlpha(float t)
  {
    _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
    while (_text.color.a < 1.0f)
    {
      _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a + (Time.deltaTime / t));
      yield return null;
    }

    yield return StartCoroutine(FadeTextToZeroAlpha(t/2));
  }
 
  private IEnumerator FadeTextToZeroAlpha(float t)
  {
    _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
    while (_text.color.a > 0.0f)
    {
      _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - (Time.deltaTime / t));
      yield return null;
    }
  }
  
}
