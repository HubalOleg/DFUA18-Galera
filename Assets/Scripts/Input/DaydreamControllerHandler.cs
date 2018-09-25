using System;
using UnityEngine;

namespace ua.org.gdg.galera
{
  public class DaydreamControllerHandler : Singleton<DaydreamControllerHandler>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [Space] [Header("Parameters")]
    [SerializeField] private Transform _controllerTransform;
    [Range(0, 20)] [SerializeField] private float _sensitivity;
    [Range(0, 90)] [SerializeField] private float _xOffset;
    [Range(0, 90)] [SerializeField] private float _yOffset;

    [Space] 
    [Header("Controller simulator")] 
    [SerializeField] private bool _simulateController;

    [Space] 
    [Header("Status")]
    [Range(-90, 90)] [SerializeField] private float _x;
    [Range(-180, 0)] [SerializeField] private float _y;
    [Range(0, 359)] [SerializeField] private float _currentAngle;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Update()
    {
      _currentAngle = Angle;

      if (!_simulateController)
      {
        _x = X;
        _y = Y;
      }
    }

    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------

    public float Angle
    {
      get
      {
        return GetAngle(X, Y);
      }
    }

   private float X
    {
      get
      {
        if (_simulateController) return _x;
        return NormalizeAngle(_controllerTransform.localEulerAngles.x, MinX, MaxX);
      }
    } 
    
    private float Y
    {
      get
      {
        if (_simulateController) return _y;
        return NormalizeAngle(_controllerTransform.localEulerAngles.y, MinY, MaxY);
      }
    }

    private float MinX
    {
      get { return DEFAULT_X - _xOffset; }
    }
    
    private float MaxX
    {
      get { return DEFAULT_X + _xOffset; }
    }
    
    private float MinY
    {
      get { return DEFAULT_Y - _yOffset; }
    }
    
    private float MaxY
    {
      get { return DEFAULT_Y + _yOffset; }
    }

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private const float DEFAULT_Y = -90;
    private const float DEFAULT_X = 0;

    private float NormalizeAngle(float x, float minX, float maxX)
    {
      if (x > 180) x -= 360;
      return Mathf.Clamp(x, minX, maxX);
    }

    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private float GetAngle(float x, float y)
    {
      // normalize values to fit circle with center in (DEFAULT_X, DEFAULT_Y)
      // and brings them to I coordinate quarter
      x -= DEFAULT_X;
      y -= DEFAULT_Y;
      float alpha = RadianToDegree(Mathf.Atan2(Mathf.Abs(x), Mathf.Abs(y)));

      if (x > 0 && y > 0) return alpha; // I
      if (x > 0 && y < 0) return 180 - alpha; // II
      if (x < 0 && y < 0) return 180 + alpha; // III
      if (x < 0 && y > 0) return 360 - alpha; // IV
      if (x == 0 && y > 0) return 0;
      if (x > 0 && y == 0) return 90;
      if (x == 0 && y < 0) return 180;
      if (x < 0 && y == 0) return 270;

      return 0;
    }

    private float RadianToDegree(float angle)
    {
      return (float) (angle * (180f / Math.PI));
    }

  }
}