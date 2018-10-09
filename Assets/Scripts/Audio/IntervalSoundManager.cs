using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ua.com.gdg.devfest
{
	[RequireComponent(typeof(AudioSource))]
	public class IntervalSoundManager : MonoBehaviour {

		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private List<AudioClip> _audioClipList;

		[SerializeField] private bool _isGameActive = true;
		
		[SerializeField] private float _minRandomInterval;
		[SerializeField] private float _maxRandomInterval;

		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------
		
		private AudioSource _audioSource;
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			StartCoroutine(PlaySoundWithInterval());
		}

		//---------------------------------------------------------------------
		// Public
		//---------------------------------------------------------------------

		private void PlayRandomClip()
		{
			var position = Random.Range(0, _audioClipList.Count - 1);
			_audioSource.clip = _audioClipList[position];
			_audioSource.Play();

		}
		
		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------

		private IEnumerator PlaySoundWithInterval()
		{	
			yield return new WaitForSeconds(GetRandomInterval());
			
			while (_isGameActive)
			{
				PlayRandomClip();

				yield return new WaitForSeconds(GetRandomInterval());
			}
		}

		private float GetRandomInterval()
		{
			return Random.Range(_minRandomInterval, _maxRandomInterval);
		}
	}
}
