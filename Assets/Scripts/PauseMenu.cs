using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Pausable pausable = null;
    private GameController gameController = null;

    private bool isShowing = false;

    private GameObject menuCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        // GameControllerとPausableの取得
        GameObject gameControllerObject = GameObject.Find("GameControllerObj");
        pausable = gameControllerObject.GetComponent<Pausable>();
        gameController = gameControllerObject.GetComponent<GameController>();

        // 子のメニューのCanvasを取得
        menuCanvas = gameObject.transform.Find("PauseMenuCanvas").gameObject;

        // メニューを非アクティブにする
        menuCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (pausable.pausing && !isShowing)
        { // ポーズ中でメニューが出ていなければ、メニューを出す
            ShowMenu();
        }
        else if (!pausable.pausing && isShowing)
        { // ポーズ中でなくてメニューが出ているのであれば、メニューを隠す
            HideMenuAndResumeTheGame();
        }
    }

    public void ShowMenu()
    {
        isShowing = true;
        menuCanvas.SetActive(true);

    }

    // ポーズメニューを隠すだけ
    public void HideMenu()
    {
        menuCanvas.SetActive(false);
        isShowing = false;
    }

    // ポーズメニューを隠してゲームを再開する
    public void HideMenuAndResumeTheGame()
    {
        HideMenu();
        // とりあえず
        // GameControllerObjのポーズフラグを折る
        pausable.pausing = false;
    }

    // ボタンの処理          
    public void OnClickButton_Resume()
    {
        Debug.Log("OnClickButton_Resume");

        // とりあえずで
        HideMenuAndResumeTheGame();
    }
    public void OnClickButton_Restart()
    {
        Debug.Log("OnClickButton_Restart");

        gameController.Restart();

    }
    public void OnClickButton_SelectStage()
    {
        Debug.Log("OnClickButton_SelectStage");

        Debug.Log("ステージセレクトシーンの非同期読込開始");
        SceneManager.LoadSceneAsync("SelectStageScene"); // StageSelectScene

    }
    public void OnClickButton_BackToTitle()
    {
        Debug.Log("OnClickButton_BackToTitle");

        Debug.Log("タイトルシーンの非同期読込開始");
        SceneManager.LoadSceneAsync("TitleScene");
    }
}
