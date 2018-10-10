using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	
	
	//---------------------------------------------------------------------
	// Editor
	//---------------------------------------------------------------------

	[SerializeField] private float _gameSceneLoadDelay;
	
	//---------------------------------------------------------------------
	// Internal
	//---------------------------------------------------------------------

	private const string GAME_SCENE_NAME = "GameScene";

	//---------------------------------------------------------------------
	// Messages
	//---------------------------------------------------------------------
	
	private void Start () 
	{
		Invoke("LoadGame", _gameSceneLoadDelay);
	}
	
	//---------------------------------------------------------------------
	// Helpers
	//---------------------------------------------------------------------

	private void LoadGame()
	{
		SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
	}
}
