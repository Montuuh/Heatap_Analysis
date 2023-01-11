using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataReader : MonoBehaviour
{
    public uint playerID = 0;
    static private DataReader instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If F5 pressed, get positions from bbdd
        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(GetPositions());
        }

        // If F6 pressed, get jumps from bbdd
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(GetJumps());
        }

        // If F7 pressed, get attacks from bbdd
        if (Input.GetKeyDown(KeyCode.F7))
        {
            StartCoroutine(GetAttacks());
        }
    }

    IEnumerator GetPositions()
    {
        // Get positions from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetPositions");
        form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "positions");
        }

        www.Dispose();
    }

    IEnumerator GetJumps()
    {
        // Get jumps from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetJumps");
        form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "jumps");
        }

        www.Dispose();
    }

    IEnumerator GetAttacks()
    {
        // Get attacks from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetAttacks");
        form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "attacks");
        }

        www.Dispose();
    }

    private void DecodePositions(string text, string type)
    {
        if (text == "0 results")
            return;
        
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> jumps = new List<Vector3>();
        List<Vector3> attacks = new List<Vector3>();
        string lineSeparator = "|*|";
        string valueSeparator = "|/|";
        string[] lines = text.Split(lineSeparator.ToCharArray());
        foreach (string line in lines)
        {
            string[] values = line.Split(valueSeparator.ToCharArray());
            if (values.Length == 4)
            {
                string name = values[0];
                float x = float.Parse(values[1]);
                float y = float.Parse(values[2]);
                float z = float.Parse(values[3]);

                switch (type)
                {
                    case "positions":
                        positions.Add(new Vector3(x, y, z));
                        break;
                    case "jumps":
                        jumps.Add(new Vector3(x, y, z));
                        break;
                    case "attacks":
                        attacks.Add(new Vector3(x, y, z));
                        break;
                }
                Debug.Log("Name: " + name + " X: " + x + " Y: " + y + " Z: " + z);
            }
        }
    }
}
