using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
	
	//---------------------------------------------------------------------
	// Internal
	//---------------------------------------------------------------------

	private const string GAME_SCENE_NAME = "GameScene";

	//---------------------------------------------------------------------
	// Messages
	//---------------------------------------------------------------------
	
	private void Start () 
	{
		Invoke("LoadGame", 2f);
	}
	
	//---------------------------------------------------------------------
	// Helpers
	//---------------------------------------------------------------------

	private void LoadGame()
	{
		SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
	}
}
