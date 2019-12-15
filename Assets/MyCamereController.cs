using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamereController : MonoBehaviour
{
    GameObject unitychan;
    float difference;

    void Start()
    {
        this.unitychan = GameObject.Find("unitychan");
        this.difference = this.unitychan.transform.position.z - this.transform.position.z;
    }

    void Update()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - this.difference);
    }
}
