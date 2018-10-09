using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ua.org.gdg.galera
{
	public class GameManager : MonoBehaviour
	{
		private const float PROGRESSBAR_ANIMATION_DURATION = .5f;
		
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[Header("UI")]
		[SerializeField] private Text _revolutionsText;
		[SerializeField] private Text _positionText;
		[SerializeField] private Slider _positionProgressBar;
		[SerializeField] private FadingText _promotingText;
		[SerializeField] private GameObject _gameOverText;

		[Space]
		[Header("Game")] 
		[SerializeField] private PaddleBehaviour _paddle;
		
		[Space]
		[Header("Variables")]
		[SerializeField] private IntVariable _revolutionsNumber;
		[SerializeField] private PositionVariable[] _positions;
		
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private PositionVariable _currentPosition;
		private int _revolutionsForNextPosition;
		private int _positionRevolutionsStep;
		private bool _gameOver;
		private GvrControllerInputDevice _controller;

		//---------------------------------------------------------------------
		// Events
		//---------------------------------------------------------------------

		public void OnFullRevolution()
		{
			_revolutionsNumber.RuntimeValue += 1;
			UpdateRevolutionsNumber(_revolutionsNumber.RuntimeValue);
			SalaryReview();
			UpdatePositionProgress();
		}
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Start()
		{
			UpdateCurrentPosition(_positions[0]);
			UpdateRevolutionsNumber(_revolutionsNumber.RuntimeValue);
			_controller = GvrControllerInput.GetDevice(GvrControllerHand.Dominant);
		}

		private void Update()
		{
			if (_controller != null && _controller.GetButtonDown(GvrControllerButton.App))
			{
				SceneManager.LoadScene("WelcomeScene");
			}
		}

		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------

		private void UpdateRevolutionsNumber(int numbers)
		{
			_revolutionsText.text = string.Format("Revolutions number: {0}", numbers);
		}
		
		private void SalaryReview()
		{
			if (_revolutionsNumber.RuntimeValue <= _revolutionsForNextPosition || _gameOver) return;
			
			var nextPosition = GetNextPosition();
			UpdateCurrentPosition(nextPosition);
			StartCoroutine(AnimatedPositionUpdate(PROGRESSBAR_ANIMATION_DURATION, _positionProgressBar.value, 0));
			
			if (CheckIfGameOver())
			{
				_gameOverText.SetActive(true);
			}
			else
			{
				_promotingText.ShowText(1.5f);
			}
		}
		
		private PositionVariable GetNextPosition()
		{
			var index = Array.IndexOf(_positions, _currentPosition);
			var nextIndex = Mathf.Clamp(index + 1, 0, _positions.Length - 1);
			
			return _positions[nextIndex];
		}

		private void UpdateCurrentPosition(PositionVariable position)
		{
			_currentPosition = position;
			SetRevolutionsForNextPosition();
			_paddle.SetPaddleMesh(position);
			
			_positionText.text = string.Format("Current position: {0}", position.PositionName);
		}

		private void SetRevolutionsForNextPosition()
		{
			var nextPosition = GetNextPosition();
			_revolutionsForNextPosition = nextPosition.RevolutionsRequired;
			_positionRevolutionsStep = _revolutionsForNextPosition - _revolutionsNumber.RuntimeValue;
		}

		private bool CheckIfGameOver()
		{
			return _gameOver = _currentPosition == _positions[_positions.Length - 1];
		}

		private void UpdatePositionProgress()
		{
			if (_gameOver) return;
			
			var progress = GetPositionProgress();
			StartCoroutine(AnimatedPositionUpdate(PROGRESSBAR_ANIMATION_DURATION, _positionProgressBar.value, progress));
		}

		private float GetPositionProgress()
		{
			var revolutionsLeft = _revolutionsForNextPosition - _revolutionsNumber.RuntimeValue;
			var revolutionsMade = _positionRevolutionsStep - revolutionsLeft;
			var normalizedRevolutionsMade = (float)revolutionsMade / _positionRevolutionsStep;

			return normalizedRevolutionsMade;
		}

		private IEnumerator AnimatedPositionUpdate(float time, float start, float end)
		{
			var timeLeft = time;
			
			while (timeLeft > 0)
			{
				_positionProgressBar.value = Mathf.Lerp(start, end, 1 - timeLeft / time);
				timeLeft -= Time.deltaTime;
				yield return new WaitForSeconds(.01f);
			}

			_positionProgressBar.value = end;
		}
	}
}