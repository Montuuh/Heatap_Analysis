using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Globalization;

public struct Position
{
    public Vector3 position;
    public string name;
}

public struct Jump
{
    public Vector3 position;
    public string name;
}

public struct Attack
{
    public Vector3 position;
    public string whoAttacked;
}

public class DataReader : MonoBehaviour
{
    public uint playerID = 0;
    static private DataReader instance;

    public List<Position> positionList = new List<Position>();
    public List<Jump> jumpList = new List<Jump>();
    public List<Attack> attackList = new List<Attack>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(GetPositions());
        StartCoroutine(GetJumps());
        StartCoroutine(GetAttacks());
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
        
        List<Vector3> attacks = new List<Vector3>();

        string lineSeparator = "|*|";
        string valueSeparator = "|/|";
        string[] lines = text.Split(lineSeparator);

        foreach (string line in lines)
        {

            string[] values = line.Split(valueSeparator);
            if (values.Length == 4)
            {
                string name = values[0];
                float x = float.Parse(values[1], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
                float y = float.Parse(values[2], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
                float z = float.Parse(values[3], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));

                switch (type)
                {
                    case "positions":
                        positionList.Add(new Position() { position = new Vector3(x,y,z), name = name});
                        break;
                    case "jumps":
                        jumpList.Add(new Jump() { position = new Vector3(x, y, z), name = name });
                        break;
                    case "attacks":
                        attackList.Add(new Attack() { position = new Vector3(x, y, z), whoAttacked = name });
                        break;
                }
                Debug.Log("Name: " + name + " X: " + x + " Y: " + y + " Z: " + z);
            }
        }
    }
}
