using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScene_BackToTitleButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private string targetStageName = "TitleScene"; 
    public void OnClick()
    {
        Debug.Log("StageSelectScene_BackToTitleButton　クリック!");
        
        Debug.Log("タイトルシーンの非同期読込開始");
        SceneManager.LoadSceneAsync(targetStageName);

    }
}
