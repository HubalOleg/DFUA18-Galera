using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ua.com.gdg.devfest
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour {

		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private List<AudioClip> _audioClipList;

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

		//---------------------------------------------------------------------
		// Public
		//---------------------------------------------------------------------

		public void PlayRandomClip()
		{
			var position = Random.Range(0, _audioClipList.Count - 1);
			_audioSource.clip = _audioClipList[position];
			_audioSource.Play();
		}

	}
}
