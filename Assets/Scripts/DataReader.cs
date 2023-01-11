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

public struct Death
{
    public Vector3 position;
    public string whoDied;
}

public struct Hit
{
    public Vector3 position;
    public string whoWasHit;
}

public struct Heal
{
    public Vector3 position;
    public int currentHealth;
}

public struct Checkpoint
{
    public Vector3 position;
    public int currentHealth;
}

public struct Interactuable
{
    public Vector3 position;
    public string interactuableName;
}

public class DataReader : MonoBehaviour
{
    public uint playerID = 0;
    public bool allData = false;

    static private DataReader instance;

    public List<Position> positionList = new List<Position>();
    public List<Jump> jumpList = new List<Jump>();
    public List<Attack> attackList = new List<Attack>();
    public List<Death> deathList = new List<Death>();
    public List<Hit> hitList = new List<Hit>();
    public List<Heal> healList = new List<Heal>();
    public List<Checkpoint> checkpointList = new List<Checkpoint>();
    public List<Interactuable> interactuableList = new List<Interactuable>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetInfoFromBBDD();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
            GetInfoFromBBDD();
    }

    void GetInfoFromBBDD()
    {
        positionList.Clear();
        jumpList.Clear();
        attackList.Clear();

        StartCoroutine(GetPositions());
        StartCoroutine(GetJumps());
        StartCoroutine(GetAttacks());
        StartCoroutine(GetDeaths());
        StartCoroutine(GetHits());
    }

    IEnumerator GetPositions()
    {
        // Get positions from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetPositions");
        if(!allData) form.AddField("playerID", playerID.ToString());
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
        if (!allData) form.AddField("playerID", playerID.ToString());
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
        if (!allData) form.AddField("playerID", playerID.ToString());
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

    IEnumerator GetDeaths()
    {
        // Get deaths from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetDeaths");
        if (!allData) form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "deaths");
        }

        www.Dispose();
    }

    IEnumerator GetHits()
    {
        // Get hits from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetHits");
        if (!allData) form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "hits");
        }

        www.Dispose();
    }

    IEnumerator GetInteractuables()
    {
        // Get interactuables from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetInteractuables");
        if (!allData) form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodePositions(www.downloadHandler.text, "interactuables");
        }

        www.Dispose();
    }

    IEnumerator GetHeals()
    {
        // Get heals from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetHeals");
        if (!allData) form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodeHealCheckpoint(www.downloadHandler.text, "heals");
        }

        www.Dispose();
    }

    IEnumerator GetCheckpoints()
    {
        // Get checkpoints from bbdd
        WWWForm form = new WWWForm();
        form.AddField("function", "GetCheckpoints");
        if (!allData) form.AddField("playerID", playerID.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/Delivery3/Exporters.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            DecodeHealCheckpoint(www.downloadHandler.text, "checkpoints");
        }

        www.Dispose();
    }

    private void DecodePositions(string text, string type)
    {
        if (text == "0 results")
            return;
        
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
                    case "deaths":
                        deathList.Add(new Death() { position = new Vector3(x, y, z), whoDied = name });
                        break;
                    case "hits":
                        hitList.Add(new Hit() { position = new Vector3(x, y, z), whoWasHit = name });
                        break;
                    case "interactuables":
                        interactuableList.Add(new Interactuable() { position = new Vector3(x, y, z), interactuableName = name });
                        break;
                }
                Debug.Log("Name: " + name + " X: " + x + " Y: " + y + " Z: " + z);
            }
        }
    }

    private void DecodeHealCheckpoint(string text, string type)
    {
        if (text == "0 results")
            return;

        string lineSeparator = "|*|";
        string valueSeparator = "|/|";
        string[] lines = text.Split(lineSeparator);

        foreach (string line in lines)
        {

            string[] values = line.Split(valueSeparator);
            if (values.Length == 4)
            {
                int currentHealth = int.Parse(values[0]);
                float x = float.Parse(values[1], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
                float y = float.Parse(values[2], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
                float z = float.Parse(values[3], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));

                switch (type)
                {
                    case "heals":
                        healList.Add(new Heal() { position = new Vector3(x, y, z), currentHealth = currentHealth });
                        break;
                    case "checkpoints":
                        checkpointList.Add(new Checkpoint() { position = new Vector3(x, y, z), currentHealth = currentHealth });
                        break;
                }
                Debug.Log("Name: " + name + " X: " + x + " Y: " + y + " Z: " + z);
            }
        }
    }
}
