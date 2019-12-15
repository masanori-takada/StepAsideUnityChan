using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    GameObject myCamera;

    void Start()
    {
        this.transform.Rotate(0, Random.Range(0, 360), 0);
        //回転を開始する角度を決定(1コマ目の０からの速度と捉えることもできる)

        this.myCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        this.transform.Rotate(0, 3, 0);
        //3の一定速度で回っていく

        if (this.myCamera.GetComponent<Transform>().position.z > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
