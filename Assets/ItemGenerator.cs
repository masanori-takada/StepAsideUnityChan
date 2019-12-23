using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //本スクリプトファイルを、のちに作成するItemGeneratorオブジェクトにアタッチした後、
    //public修飾子がついたコンセントが、インスペクターに表示され、各Prefabをアタッチできる。
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;

    GameObject unitychan;
    //GameObject cone;
    //GameObject coin;
    //GameObject car;

    //GameObject other;

    //int startPos = -160;
    //int goalPos = 120;
    float posRange = 3.4f;
    float delta = 0;
    float span = 30.0f;

    void Start()
    {
        this.unitychan = GameObject.Find("unitychan");
    }

    void Update()
    {
        this.delta += this.unitychan.GetComponent<Rigidbody>().velocity.z * Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            int num = Random.Range(1, 11);
            //乱数により出るアイテムを決定
            if (num <= 2)
            //20%の確率で、コーンがX軸方向に一列に出現。
            {
                for (float j = -1; j <= 1; j += 0.4f)
                //x座標が−1から1まで、0.4間隔で並び続ける
                //jは本かっこ内で使える
                //jの数字型は0.4刻みのため、floatにしている
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, unitychan.transform.position.z + 40);
                    //x座標は−４から４まで並び続ける、y座標はもとのconePrefabのまま、z座標は上記のi座標参照
                }
            }

            else
            //残り80%の確率の中で、60%コイン配置、30%車配置、10%なし
            {
                for (int j = -1; j <= 1; j++)
                {
                    int item = Random.Range(1, 11);
                    //アイテム発生の乱数
                    int offsetZ = Random.Range(-5, 6);
                    //アイテム発生時の、Z方向の不規則性の乱数

                    if (1 <= item && item <= 6)  //60%コイン配置
                    {
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position
                            = new Vector3(posRange * j, coin.transform.position.y, unitychan.transform.position.z + 40);
                        //x座標は、posRangeとjにより３パターン、z座標はoffsetZにより複数パターン

                    }

                    else if (7 <= item && item <= 9)
                    {
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position
                            = new Vector3(posRange * j, car.transform.position.y, unitychan.transform.position.z + 40);
                    }
                }
            }
        }

    }
}