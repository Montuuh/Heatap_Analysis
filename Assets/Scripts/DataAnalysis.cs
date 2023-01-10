using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using System;
using System.IO.Compression;
using UnityEngine.Networking;

public class DataAnalysis : MonoBehaviour
{
    public static Action<DateTime> OnNewPlayer; // Date
    public static Action<DateTime, uint> OnNewSession; // Date, PlayerID
    public static Action<DateTime, uint> OnEndSession; // Date, PlayerID
    public static Action<int, DateTime> OnBuyItem; //Item id and date
    public static uint playerID;

    private void OnEnable()
    {
        OnNewPlayer += NewPlayer;
        OnNewSession += NewSession;
        OnEndSession += EndSession;
        //Callbacks.OnBuyItem += OnBuyItem;
    }

    private void OnDisable()
    {
        OnNewPlayer -= NewPlayer;
        OnNewSession -= OnNewSession;
        OnEndSession -= OnEndSession;
        //Callbacks.OnBuyItem -= OnBuyItem;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        OnNewPlayer.Invoke(DateTime.Now);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnApplicationQuit()
    {
        //OnEndSession.Invoke(DateTime.Now, playerID);
        //WWWForm form = new WWWForm();
        //form.AddField("dateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //form.AddField("playerID", playerID.ToString());
        //form.AddField("function", "EndSession");
        //string url = "https://citmalumnes.upc.es/~davidmm24/Delivery3/Importers.php";

        //using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        //{
        //    www.downloadHandler = new DownloadHandlerBuffer();
        //    www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //        Debug.Log(www.error);
        //    else
        //    {
        //        Debug.Log("Form upload complete! Response: " + www.downloadHandler.text);
        //    }
        //}
    }

    void  NewPlayer(DateTime date)
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
                playerID = uint.Parse(www.downloadHandler.text);

                OnNewSession.Invoke(DateTime.Now, playerID);
            }
        }
    }

    void NewSession(DateTime date, uint playerID)
    {
        StartCoroutine(SendNewSession(date, playerID));
    }

    IEnumerator SendNewSession(DateTime date, uint playerID)
    {
        WWWForm form = new WWWForm();
        form.AddField("dateTime", date.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("playerID", playerID.ToString());
        form.AddField("function", "NewSession");
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
                OnEndSession.Invoke(DateTime.Now, playerID);
            }
        }
    }

    void EndSession(DateTime date, uint playerID)
    {
        StartCoroutine(SendEndSession(date, playerID));
    }

    IEnumerator SendEndSession(DateTime date, uint playerID)
    {
        WWWForm form = new WWWForm();
        form.AddField("dateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("playerID", playerID.ToString());
        form.AddField("function", "EndSession");
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
            }
        }
    }
}
