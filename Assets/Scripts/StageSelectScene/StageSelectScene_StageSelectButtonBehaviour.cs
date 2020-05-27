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
        Debug.Log(TargetStageName + " ボタン　クリック!");

        Debug.Log("シーンの非同期読込開始. シーン名: " + TargetStageName + ".");
        SceneManager.LoadSceneAsync(TargetStageName);

    }
    
}
