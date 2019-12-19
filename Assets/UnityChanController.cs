using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;　　//UI(ゲームオーバー、クリア等の判定/表示）を使用するので必須


public class UnityChanController : MonoBehaviour
{
    Animator myAnimator;
    //アニメーションするためのコンポーネントを入れるための型準備
    Rigidbody myRigidbody;
    //リジッドボディもまずは型を準備

    float forwardForce = 800.0f;
    //前進するための力
    float turnForce = 500.0f;
    //左右に移動するための力
    float movableRange = 3.4f;
    //左右の移動できる範囲
    float upForce = 500.0f;
    //ジャンプするための力

    float coefficient = 0.95f;
    //障害物に当たった時の動きの減衰係数
    bool isEnd = false;
    //ゲーム終了の判定変数（初期値はfalse)

    GameObject stateText;
    //ゲーム終了のUIテキスト（まずは型の準備）
    GameObject scoreText;
    //ポイントのUIテキスト（まずは型の準備）
    int score = 0;
    //スコアの初期値

    bool isLButtonDown = false;
    bool isRButtonDown = false;
    bool isJumpButtonDown = false;


    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        //アニメータコンポーネントを取得
        this.myAnimator.SetFloat("Speed", 1.0f);
        //Animatorクラスの「SetFloat」関数は、第一引数に与えられたパラメータに、第二引数の値を代入する関数です。
        //また、第一引数のバラメータがアニメーション再生の条件に使われています。
        //UnityChanLocomotionsの設定では、Speedパラメータが0.8以上の値の場合に走るアニメーションとなる設定となっている（アニメータで確認）
        this.myRigidbody = GetComponent<Rigidbody>();
        //リジッドボディコンポーネントを取得
        this.stateText = GameObject.Find("GameResultText");
        //UIテキストを拾ってくる
        this.scoreText = GameObject.Find("ScoreText");
        //UIテキストを拾ってくる
    }

    void Update()
    {
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);
        //this.transform.forward が「オブジェクトの前方方向の単位ベクトル」
        //前方方向とは、カメラから見たゲームシーンの前方方向

        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
            //AddForceは、引数がベクトル担っている必要がある。上記のようにベクトル型の変数でもよい。
        }

        if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.movableRange > this.transform.position.x)
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        // Animatorクラスの「GetCurrentAnimatorStateInfo(0)」で現在のアニメーションの状態を取得し、
        //「IsName」関数で取得したステートの名前が引数の文字列と一致しているかどうかを調べます。
        //現在の状態が　Jump と一致しているなら、という意味になる
        {
            this.myAnimator.SetBool("Jump", false);
            //Animatorクラスの「SetBool」関数は、第一引数に与えられたパラメータに、第二引数の値を代入する関数です。
            //また、第一引数のバラメータがアニメーション再生の条件に使われています。
            //setするものの型に応じて、SetFloat　や、　SetBool（真偽値）　がある
        }

        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加）
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

        if (this.isEnd)
        //上記のOnTriggerEnter関数で、this.isEnd　が　true になった場合のケース
        {
            //全ての力に減衰係数をかけて、徐々に止まるようにする
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;

            this.myAnimator.speed *= this.coefficient;
            //myAnimatorのメンバ変数にspeedが存在する
        }

    }

    public void GetJumpButtonDown()  //ボタンUIに貼り付けるためのpublic
    {
        if (this.transform.position.y < 0.5f)
        //横移動では押した状態をキープしている状態でGetKey、ジャンプでは押した瞬間という行為でGetKeyDown、
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    public void GetLeftButtonDown()  //ボタンUIに貼り付けるためのpublic
    {
        this.isLButtonDown = true;
        //左右のボタンダウンアップはどのように機能しているのか？？？？？
    }

    public void GetLeftButtonUp()  //ボタンUIに貼り付けるためのpublic
    {
        this.isLButtonDown = false;
    }

    public void GetRightButtonDown()  //ボタンUIに貼り付けるためのpublic
    {
        this.isRButtonDown = true;
    }

    public void GetRightButtonUp()  //ボタンUIに貼り付けるためのpublic
    {
        this.isRButtonDown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴールへの到着
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //コインの衝突
        if (other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            GetComponent<ParticleSystem>().Play();
            //パーティクルシステムクラスのプレイ関数により、自分に設定されているパーティクルが再生される
            Destroy(other.gameObject);
            //コインは消える
        }
    }





}
