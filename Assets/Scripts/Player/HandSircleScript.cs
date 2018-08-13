using System;
using UnityEngine;

namespace ua.com.gdg.devfest
{
  public class HandSircleScript : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [Range(0, 2)] [SerializeField] private float _radius;

    // Use this for initialization
    private void Awake()
    {
      _center = new Vector2(transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
      SetCoordinatesOnCircle(DaydreamControllerHandler.Instance.Angle);
    }

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    public Vector2 _center;
    public Vector2 position;

    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private void SetCoordinatesOnCircle(float angle)
    {
      position = _center + GetCoordinatesByAngle(angle);
      transform.SetPositionAndRotation(new Vector3(transform.position.x, position.x, position.y), transform.rotation);
    }

    private Vector2 GetCoordinatesByAngle(float angle)
    {
      int quarter;
      float nAngle = NormalizeAngle(angle, out quarter);
      
      Debug.Log(quarter);
      
      float x = _radius * Mathf.Cos(DegreeToRadian(nAngle));
      float y = _radius * Mathf.Sin(DegreeToRadian(nAngle));

      switch (quarter)
      {
        case 0:
          return new Vector2(x, -y);
        case 1:
          return new Vector2(-y, -x);
        case 2:
          return new Vector2(-x, y);
        case 3:
          return new Vector2(y, x);
      }

      return new Vector2(x, y);
    }

    private float NormalizeAngle(float angle, out int quarter)
    {
      quarter = GetQuarter(angle);
      return angle - 90 * quarter;
    }

    private int GetQuarter(float angle)
    {
      if (angle >= 0 && angle < 90) return 0;
      if (angle >= 90 && angle < 180) return 1;
      if (angle >= 180 && angle < 270) return 2;
      if (angle >= 270 && angle < 360) return 3;
      return 1;
    }

    private float DegreeToRadian(float angle)
    {
      return (float) (Math.PI * angle / 180f);
    }
  }
}