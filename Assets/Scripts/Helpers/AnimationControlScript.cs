using System;
using UnityEngine;

namespace ua.com.gdg.devfest
{
	public class AnimationControlScript : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------
		
		[SerializeField] private Animator _animator;
		[SerializeField] private string _animationName;
		[Range(0, 1)] [SerializeField] private float _animationOffset;
		[Range(0, 1)] [SerializeField] private float _animationProgress;
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------
		
		private void Start()
		{
			_animator.speed = 0;
		}

		// Update is called once per frame
		void Update()
		{
				_animationProgress = GetAnimationProgressWithAngle(DaydreamControllerHandler.Instance.Angle);
				_animator.Play(_animationName, -1, _animationProgress);
		}
		
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------
		
		private float GetAnimationProgressWithAngle(float angle)
		{
			float left, right, minAngle, maxAngle;

			GetStageParameters(angle, out left, out right, out minAngle, out maxAngle);
			float normalized = Normalization.NormalizeTo_0_1(minAngle, maxAngle, angle);

			return Normalization.NormalizeToRange(normalized, left, right) + _animationOffset;
		}
		
		private void GetStageParameters(float angle, out float left, out float right, out float minAngle,
			out float maxAngle)
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
		
		private const float QUARTER_0 = 1;
		private const float QUARTER_1 = .75f;
		private const float QUARTER_2 = .5f;
		private const float QUARTER_3 = .25f;
		private const float QUARTER_4 = 0;

		// left = (x, y) - rotation of start of stage. left end of range
		// right = (x, y) - rotation of end of stage. right end of range

		private void GetStage1Parameters(out float left, out float right, out float minAngle, out float maxAngle)
		{
			left = QUARTER_0;
			right = QUARTER_1;
			minAngle = 0;
			maxAngle = 90;
		}

		private void GetStage2Parameters(out float left, out float right, out float minAngle, out float maxAngle)
		{
			left = QUARTER_1;
			right = QUARTER_2;
			minAngle = 90;
			maxAngle = 180;
		}

		private void GetStage3Parameters(out float left, out float right, out float minAngle, out float maxAngle)
		{
			left = QUARTER_2;
			right = QUARTER_3;
			minAngle = 180;
			maxAngle = 270;
		}

		private void GetStage4Parameters(out float left, out float right, out float minAngle, out float maxAngle)
		{
			left = QUARTER_3;
			right = QUARTER_4;
			minAngle = 270;
			maxAngle = 360;
		}
	}
}