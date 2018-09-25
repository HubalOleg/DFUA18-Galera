using System;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.UI;

namespace ua.org.gdg.devfest
{
	public class GameManager : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[Header("UI")]
		[SerializeField] private Text _revolutionsText;
		[SerializeField] private Text _positionText;
		
		[Space]
		[Header("Variables")]
		[SerializeField] private IntVariable _revolutionsNumber;
		[SerializeField] private PositionVariable[] _positions;
		
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private PositionVariable _currentPosition;
		private int _revolutionsForNextPosition;

		//---------------------------------------------------------------------
		// Events
		//---------------------------------------------------------------------

		public void OnFullRevolution()
		{
			_revolutionsNumber.RuntimeValue += 1;
			UpdateRevolutionsNumber(_revolutionsNumber.RuntimeValue);
			SalaryReview();
		}
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Start()
		{
			UpdateCurrentPosition(_positions[0]);
			UpdateRevolutionsNumber(_revolutionsNumber.RuntimeValue);
		}

		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------

		private void UpdateCurrentPosition(PositionVariable position)
		{
			_currentPosition = position;
			SetRevolutionsForNextPosition();
			_positionText.text = string.Format("Current position: {0}", position.name);
		}

		private void SetRevolutionsForNextPosition()
		{
			var nextPosition = GetNextPosition();
			_revolutionsForNextPosition = nextPosition.RevolutionsRequired;
		}

		private PositionVariable GetNextPosition()
		{
			var index = Array.IndexOf(_positions, _currentPosition);
			var nextIndex = Mathf.Clamp(index + 1, 0, _positions.Length - 1);
			
			return _positions[nextIndex];
		}

		private void SalaryReview()
		{
			if (_revolutionsNumber.RuntimeValue >= _revolutionsForNextPosition)
			{
				var nextPosition = GetNextPosition();
				UpdateCurrentPosition(nextPosition);
			}
		}

		private void UpdateRevolutionsNumber(int numbers)
		{
			_revolutionsText.text = string.Format("Revolutions number: {0}", numbers);
		}
	}
}