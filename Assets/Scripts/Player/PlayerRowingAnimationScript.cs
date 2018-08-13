using System;
using UnityEngine;

namespace ua.com.gdg.devfest
{
  public class PlayerRowingAnimationScript : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private Animator _animator;
    [Range(0, 20)] [SerializeField] private float _sensitivity;
    [Range(0, 90)] [SerializeField] private float _paddleOffsetX;
    [Range(0, 90)] [SerializeField] private float _paddleOffsetY;
    [SerializeField] private Transform _controllerTransform;
    
    [Space]
    [Header("Controller simulator")]
    [SerializeField] private bool _simulateController;
    [Range(-50, 50)] [SerializeField] private float _x;
    [Range(-140, -40)] [SerializeField] private float _y;
    
    [Space]
    [Header("Debug")]
    [Range(0, 1)] [SerializeField] private float _animationProgress;
    [Range(0, 359)] [SerializeField] private float _currentAngle;
   


    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _minX = DEFAULT_X - _paddleOffsetX;
      _minY = DEFAULT_Y - _paddleOffsetY;
      _maxX = DEFAULT_X + _paddleOffsetX;
      _maxY = DEFAULT_Y + _paddleOffsetY;
    }

    // Use this for initialization
    private void Start()
    {
      _animator.speed = 0;
    }

    // Update is called once per frame
    private void Update()
    {
      //without
      if (_simulateController)
      {
        _animationProgress = GetAnimationProgressWithAngle(_x, _y);
        _animator.Play("Rowing", -1, _animationProgress);
      }
      //with daydream controller
      else
      {
        float x = NormalizeAngle(_controllerTransform.localEulerAngles.x, _minX, _maxX);
        float y = NormalizeAngle(_controllerTransform.localEulerAngles.y, _minY, _maxY);
        
        //_animator.Play("Rowing", -1, GetAnimationProgress(x, y));
        _animator.Play("Rowing", -1, GetAnimationProgressWithAngle(x, y));
      }
    }

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    // default values +- offset
    private float _maxX, _maxY, _minX, _minY;
    
    private const float STAGE_0 = 0;
    private const float STAGE_1 = .35f;
    private const float STAGE_2 = .57f;
    private const float STAGE_3 = .7f;
    private const float STAGE_4 = 1;

    private const float DEFAULT_Y = -90;
    private const float DEFAULT_X = 0;

    private float NormalizeAngle(float x, float minX, float maxX)
    {
      if (x > 180) x -= 360;
      return Mathf.Clamp(x, minX, maxX);
    }

    private float GetAnimationProgress(float x, float y)
    {
      float left, right, leftX, leftY, rightX, rightY;
      GetStageParameters(x, y, out left, out right, out leftX, out leftY, out rightX, out rightY);
      
      float normalized = Normalize2DTo_0_1(leftX, leftY, rightX, rightY, x, y);

      return NormalizeToRange(normalized, left, right);
    }

    private float GetAnimationProgressWithAngle(float x, float y)
    {
      float angle, left, right, minAngle, maxAngle;
      
      angle = GetAngle(x, y);
      
      //!!DEBUG!!
      _currentAngle = angle;
      
      GetStageParameters(angle, out left, out right, out minAngle, out maxAngle);
      float normalized = NormalizeTo_0_1(minAngle, maxAngle, angle);
      
      return NormalizeToRange(normalized, left, right);
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
      
      if (x > 0 && y > 0) return alpha;       // I
      if (x > 0 && y < 0) return 180 - alpha; // II
      if (x < 0 && y < 0) return 180 + alpha; // III
      if (x < 0 && y > 0) return 360 - alpha; // IV
      if (x == 0 && y > 0) return 0;
      if (x > 0 && y == 0) return 90;
      if (x == 0 && y > 0) return 180;
      if (x < 0 && y == 0) return 270;

      return 0;
    }
    
    private float RadianToDegree(float angle)
    {
      return (float) (angle * (180.0 / Math.PI));
    }
    
    private void GetStageParameters(float x, float y, out float left, out float right, out float leftX, out float leftY,
      out float rightX, out float rightY)
    {
      //ranges be like [left, right]
      if (y >= DEFAULT_Y && y <= _maxY && x >= DEFAULT_X && x <= _maxX)
      {
        Debug.Log("Stage1");
        GetStage1Parameters(out left, out right, out leftX, out leftY, out rightX, out rightY);
        return;
      }

      if (y >= _minY && y <= DEFAULT_Y && x >= DEFAULT_X && x <= _maxX)
      {
        Debug.Log("Stage2");
        GetStage2Parameters(out left, out right, out leftX, out leftY, out rightX, out rightY);
        return;
      }

      if (y >= _minY && y <= DEFAULT_Y && x >= _minX && x <= DEFAULT_X)
      {
        Debug.Log("Stage3");
        GetStage3Parameters(out left, out right, out leftX, out leftY, out rightX, out rightY);
        return;
      }

      if (y >= DEFAULT_Y && y <= _maxY && x >= _minX && x <= DEFAULT_X)
      {
        Debug.Log("Stage4");
        GetStage4Parameters(out left, out right, out leftX, out leftY, out rightX, out rightY);
        return;
      }

      throw new Exception("Unknown angle");
    }
    
    private void GetStageParameters(float angle, out float left, out float right, out float minAngle, out float maxAngle)
    {
      if (angle >= 0 && angle < 90)
      {
        GetStage1Parameters(out left, out right, out minAngle, out maxAngle);
        return;
      }

      if (angle >= 90 && angle < 180)
      {
        GetStage2Parameters(out left, out right, out minAngle, out maxAngle);
        return;
      }

      if (angle >= 180 && angle < 270)
      {
        GetStage3Parameters(out left, out right, out minAngle, out maxAngle);
        return;
      }

      if (angle >= 270 && angle < 360)
      {
        GetStage4Parameters(out left, out right, out minAngle, out maxAngle);
        return;
      }
      
      Debug.Log(angle);
      throw new Exception("Unknown angle");
    }

    // left = (x, y) - rotation of start of stage. left end of range
    // right = (x, y) - rotation of end of stage. right end of range
    
    private void GetStage1Parameters(out float left, out float right, out float leftX, out float leftY, out float rightX,
      out float rightY)
    {
      left = STAGE_0;
      right = STAGE_1;
      leftX = DEFAULT_X;
      leftY = _maxY;
      rightX = _maxX;
      rightY = DEFAULT_Y;
    }

    private void GetStage1Parameters(out float left, out float right, out float minAngle, out float maxAngle)
    {
      left = STAGE_0;
      right = STAGE_1;
      minAngle = 0;
      maxAngle = 90;
    }

    private void GetStage2Parameters(out float left, out float right, out float leftX, out float leftY, out float rightX,
      out float rightY)
    {
      left = STAGE_1;
      right = STAGE_2;
      leftX = _maxX;
      leftY = DEFAULT_Y;
      rightX = DEFAULT_X;
      rightY = _minY;
    }
    
    private void GetStage2Parameters(out float left, out float right, out float minAngle, out float maxAngle)
    {
      left = STAGE_1;
      right = STAGE_2;
      minAngle = 90;
      maxAngle = 180;
    }

    private void GetStage3Parameters(out float left, out float right, out float leftX, out float leftY, out float rightX,
      out float rightY)
    {
      left = STAGE_2;
      right = STAGE_3;
      leftX = DEFAULT_X;
      leftY = _minY;
      rightX = _minX;
      rightY = DEFAULT_Y;
    }
    
    private void GetStage3Parameters(out float left, out float right, out float minAngle, out float maxAngle)
    {
      left = STAGE_2;
      right = STAGE_3;
      minAngle = 180;
      maxAngle = 270;
    }

    private void GetStage4Parameters(out float left, out float right, out float leftX, out float leftY, out float rightX,
      out float rightY)
    {
      left = STAGE_3;
      right = STAGE_4;
      leftX = _minX;
      leftY = DEFAULT_Y;
      rightX = DEFAULT_X;
      rightY = _maxY;
    }
    
    private void GetStage4Parameters(out float left, out float right, out float minAngle, out float maxAngle)
    {
      left = STAGE_3;
      right = STAGE_4;
      minAngle = 270;
      maxAngle = 360;
    }

    private float NormalizeToRange(float value, float left, float right)
    {
      return value * (right - left) + left;
    }

    private float Normalize2DTo_0_1(float leftX, float leftY, float rightX, float rightY, float currentX, float currentY)
    {
      float x = NormalizeTo_0_1(leftX, rightX, currentX);
      float y = NormalizeTo_0_1(leftY, rightY, currentY);
      return (x + y) / 2;
    }

    private float NormalizeTo_0_1(float left, float right, float current)
    {
      if (right > left)
      {
        //Usual normalization
        return (current - left) / (right - left);
      }

      // if right end of range has lesser value compute it like usual normalization and
      // invert it (1 - normalization)
      return 1 - (current - right) / (left - right);
    }
  }
}