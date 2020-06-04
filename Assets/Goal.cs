using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Goal : MonoBehaviour
{
    // クリア後に生成するオブジェクト
    [SerializeField]
    private GameObject objectAfterClear;

    // ゴールしているかどうか
    bool isCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        // ゴールエリアの画像サイズをColliderの領域に合わせて調整する。
        CircleCollider2D ccol = GetComponent<CircleCollider2D>();
        Transform areaImageTransform = gameObject.transform.Find("Image");
        float scale = ccol.radius * 2.0f; // 半径1.0f == スケール 1.0f
        areaImageTransform.localScale = new Vector3(scale, scale, scale);
        // ColliderはあらかじめPrefabに追加しておく。
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCleared)
            return; // クリア後は呼ばない。

        if (collision.gameObject.name == "Ball")
        {
            // ボールが領域に入ったらクリアイベントを呼ぶ。
            Debug.Log("OnTriggerEnter2D: other is " + collision.gameObject.name);
            
            // クリア後のイベント
            Instantiate(objectAfterClear);

            // スポナーの排水を止める。


            // おわり
            isCleared = true;
        }
    }
}
