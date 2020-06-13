using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameQuitButton : MonoBehaviour
{
	void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
	  UnityEngine.Application.Quit();
#endif
	}
	void Update()
	{
		if (Input.GetKey(KeyCode.Q)) Quit();
	}
	public void OnClick()
	{
		Debug.Log("StageSelectButton クリック。");

		// ボタンの色を変える
		gameObject.GetComponent<Image>().color = Color.cyan;

		// ゲームシーンの非同期ロード開始    
		Debug.Log("アプリケーションの終了。");
		Quit();
	}
}