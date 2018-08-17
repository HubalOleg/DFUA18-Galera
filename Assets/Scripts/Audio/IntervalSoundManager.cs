using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ua.com.gdg.devfest
{
	public class IntervalSoundManager : MonoBehaviour {

		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private List<AudioClip> _audioClipList;
		
		[SerializeField] private float _minPitchInterval;
		[SerializeField] private float _maxPitchInterval;

		[SerializeField] private float _soundInterval;
		[SerializeField] private bool _isGameActive = true;
	
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private int _currentClipPosition = 0;
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Start()
		{
			StartCoroutine(PlaySoundWithInterval());
		}

		//---------------------------------------------------------------------
		// Public
		//---------------------------------------------------------------------

		public void PlayNextClip()
		{
			_audioSource.clip = _audioClipList[_currentClipPosition];
			_audioSource.pitch = Random.Range(_minPitchInterval, _maxPitchInterval);
			_audioSource.Play();

			_currentClipPosition = ++_currentClipPosition % _audioClipList.Count;
		}
		
		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------

		private IEnumerator PlaySoundWithInterval()
		{	
			while (_isGameActive)
			{
				PlayNextClip();
				yield return new WaitForSeconds(_soundInterval);
			}
		}
	}
}
