using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickMover : MonoBehaviour
{
    public float moveSpeed;
    bool loopBack;

    private void Start()
    {
        loopBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 座標を取得
        Vector3 pos = transform.localPosition;

        // 移動量を把握
        // 外側に膨らんだら
        if (3.3f < pos.x)
        {
            loopBack = false;
        }
        else if (pos.x < 0.0f)
        {
            loopBack = true;
        }

        // 座標更新
        if(!loopBack)
        {
            // ゴール方向に移動
            pos.x -= moveSpeed;
        }
        else
        {
            // 逆方向に移動
            pos.x += moveSpeed;
        }

        transform.localPosition = pos;
    }
}
