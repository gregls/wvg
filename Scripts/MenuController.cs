using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public int screenWidth = 1280;
	public int screenHeight = 800;

	void Start(){
		Screen.SetResolution (screenWidth, screenHeight, true);
	}

	public void loadLevel(string level){
		SceneManager.LoadScene("Level" + level, LoadSceneMode.Single);
	}
}
