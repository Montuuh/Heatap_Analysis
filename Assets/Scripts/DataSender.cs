using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using System;
using System.IO.Compression;
using UnityEngine.Networking;

public class DataSender : MonoBehaviour
{
    static public DataSender instance;

    //This idk if it is necessary
    private float timeSinceStart;
    private float lastDeathTime;
    private float lastCheckpointTime;


    private void Awake()
    {
        instance = this;
    }


    static public void OnHit(Vector3 position, string whoGotHit)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(position + " " + whoGotHit);
    }
    static public void OnDeath(Vector3 position, string whoDied)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(position + " " + whoDied);
    }
    static public void OnAttack(Vector3 position, string whoAttacked)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(position);
    }

    static public void OnJump(Vector3 position)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(position);
    }
    static public void OnCheckpoint(string checkpointID, int currentHealth)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log("");
    }

    static public void OnHeal(Vector3 position, int currentHealth)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log("");
    }

    static public void OnInteractuable(Vector3 position, string interactuableName)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(interactuableName);
    }

    static public void SendPosition(Vector3 position, string name)
    {
        //instance.StartCoroutine(instance.SendNewPlayer(date));
        Debug.Log(name + " is in: " + position);
    }

}
