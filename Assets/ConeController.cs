using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{
    GameObject myCamera;

    void Start()
    {
        this.myCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (this.myCamera.GetComponent<Transform>().position.z > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}