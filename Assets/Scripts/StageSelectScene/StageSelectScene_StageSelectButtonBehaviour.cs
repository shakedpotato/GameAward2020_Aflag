using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// StageSelectButtonBehaviour
// ステージセレクト画面の各ステージのパネルオブジェクトにアタッチする。
public class StageSelectScene_StageSelectButtonBehaviour : MonoBehaviour
{

    // GameSceneのGameControllerObjの初期化処理に合わせて、このクラスが持つべき要素が異なるので、すり合わせをすること TODO---!>
    [SerializeField]
    private string TargetStageName = "tmp";


    public void Start()
    {
        Transform button = transform.Find("Button");
    }

    public void OnClickButton()
    {
        Debug.Log("OnClick was called. The next stage name is " + TargetStageName + ".");

        Debug.Log("Initiate loading the next scene. Scene name is " + TargetStageName + ".");
        SceneManager.LoadSceneAsync(TargetStageName);

    }
    
}
