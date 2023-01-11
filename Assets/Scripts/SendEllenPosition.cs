using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEllenPosition : MonoBehaviour
{
    public float timeBetweenDataSends = 0.5f;
    private float timer;

    private void Start()
    {
        timer = 1f;
    }

    void Update()
    {
        if (timer >= timeBetweenDataSends)
        {
            timer = 0f;
            DataSender.SendPosition(this.transform.position, this.name);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
