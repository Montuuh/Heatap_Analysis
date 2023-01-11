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
    
    private static int playerID;

    private static string url = "https://citmalumnes.upc.es/~davidmm24/Delivery3/Importers.php";


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        NewPlayer(DateTime.Now);
    }

    public void Heal()
    {
        
    }

    static public void OnHit(Vector3 position, string whoGotHit)
    {
        // not implemented
    }
    static public void OnDeath(Vector3 position, string whoDied)
    {
        // not implemented
    }
    static public void OnAttack(Vector3 position, string whoAttacked)
    {
        if (playerID != 0)
            instance.SendAttack(position, whoAttacked);
    }

    static public void OnJump(Vector3 position, string name)
    {
        if (playerID != 0)
            instance.SendJump(position, DateTime.Now, name);
    }
    static public void OnCheckpoint(string checkpointID, int currentHealth)
    {
        // not implemented
    }

    static public void OnHeal(Vector3 position, int currentHealth)
    {
        // not implemented
    }

    static public void OnInteractuable(Vector3 position, string interactuableName)
    {
        // not implemented
    }

    static public void SendPosition(Vector3 position, string name)
    {
        if (playerID != 0)
            instance.SendPosition(position, name, DateTime.Now);
    }

    #region Player
    void NewPlayer(DateTime date)
    {
        StartCoroutine(SendNewPlayer(date));
    }

    IEnumerator SendNewPlayer(DateTime date)
    {
        WWWForm form = new WWWForm();
        form.AddField("dateTime", date.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("function", "NewPlayer");
        string url = "https://citmalumnes.upc.es/~davidmm24/Delivery3/Importers.php";

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log("Form upload complete! Response: " + www.downloadHandler.text);
                playerID = int.Parse(www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Position
    void SendPosition(Vector3 position, string name, DateTime date)
    {
        StartCoroutine(SendNewPosition(position, name, date));
    }
    IEnumerator SendNewPosition(Vector3 position, string name, DateTime date)
    {
        WWWForm form = new WWWForm();
        form.AddField("function", "NewPosition");
        form.AddField("dateTime", date.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("playerID", playerID);
        form.AddField("x", position.x.ToString());
        form.AddField("y", position.y.ToString());
        form.AddField("z", position.z.ToString());
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log("Form upload complete! Response: " + www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Jump
    void SendJump(Vector3 position, DateTime date, string name)
    {
        StartCoroutine(SendNewJump(position, date, name));
    }

    IEnumerator SendNewJump(Vector3 position, DateTime date, string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("function", "NewJump");
        form.AddField("dateTime", date.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("playerID", playerID);
        form.AddField("x", position.x.ToString());
        form.AddField("y", position.y.ToString());
        form.AddField("z", position.z.ToString());
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log("Form upload complete! Response: " + www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Attack
    void SendAttack(Vector3 position, string name)
    {
        StartCoroutine(SendNewAttack(position, name));
    }

    IEnumerator SendNewAttack(Vector3 position, string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("function", "NewAttack");
        form.AddField("dateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("playerID", playerID);
        form.AddField("x", position.x.ToString());
        form.AddField("y", position.y.ToString());
        form.AddField("z", position.z.ToString());
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log("Form upload complete! Response: " + www.downloadHandler.text);
            }
        }
    }
    #endregion
}
