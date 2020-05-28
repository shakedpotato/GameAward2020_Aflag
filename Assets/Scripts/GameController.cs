using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> gameObjects;
    private int ListSize = 0;
    private int NextStageNum = 0;
    private int CurrentStageNum = -1;
    private GameObject StageObject;

    public static GameController instance { get; private set; }


    Water2D.Water2D_Spawner Water2D_Spawner;
    GameObject[] waterdrop;

    
    /*
     ステージ遷移したい場合　必要なところでこれを呼ぶ
        GameController.instance.NextScene();
    */

    void Start()
    {

        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        ListSize = gameObjects.Count;
        StageObject = null;
        NextStageNum = 0;
        CurrentStageNum = -1;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(NextStageNum != CurrentStageNum)
        {
            InstantiateStage(NextStageNum);
        }
        
    }
    void InstantiateStage(int StageNum)
    {
        if (StageNum <= ListSize)
        {
            if (StageObject) { Destroy(StageObject); }
            StageObject = Instantiate(gameObjects[StageNum]);
            StageObject.transform.SetParent(transform);
            CurrentStageNum = StageNum;
            Water2D_Spawner = GameObject.Find("Water2D_Spawner").GetComponent<Water2D.Water2D_Spawner>();
        }
    }

    private void EndGame()
    {

    }

    public int NextStage()
    {
        NextStageNum++;
       // if(NextStageNum >= ListSize) { EndGame(); } resultシーンなど
        NextStageNum %= ListSize;

        return NextStageNum;
    }
    public int GetCurret()
    {
        return CurrentStageNum;
    }

    public void Restart()
    {
        InstantiateStage(CurrentStageNum);
    }
    
}
