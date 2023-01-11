using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using System;
using System.IO.Compression;
using UnityEngine.Networking;

public enum EventTypePHP
{
    none = -1,
    player,
    position
}

public abstract class FormToPHP
{
    public string url;
    public EventTypePHP type;
    public WWWForm form;
    public UnityWebRequest www;
    public string response;
    public bool isDone;
    public bool isError;
    public bool isTimeout;

    public FormToPHP(string url, EventTypePHP type)
    {
        this.url = url;
        this.type = type;
        form = new WWWForm();
        www = null;
        response = "";
        isDone = false;
        isError = false;
        isTimeout = false;
    }
    public void Send()
    {
        SaveForm();
        SendFormToPhp();
    }
    public abstract void SaveForm();
    public void SendFormToPhp()
    {
        www = UnityWebRequest.Post(url, form);
        www.SendWebRequest();
        while (!www.isDone)
        {
            if (www.isNetworkError || www.isHttpError)
            {
                isError = true;
                Debug.Log("ERROR: " + www.error);
                break;
            }
        }
        if (!isError)
        {
            Receive();
        }
    }
    public abstract void Receive();
    public abstract void DecodeResponse();
}

public class PlayerForm : FormToPHP
{
    public int playerID;
    
    public PlayerForm(string url, EventTypePHP type) : base(url, type)
    {
        this.url = url;
        this.type = type;
    }

    public override void SaveForm()
    {
        form.AddField("type", type.ToString());
    }

    public override void Receive()
    {
        response = www.downloadHandler.text;
        Debug.Log(response);

        isDone = true;
    }

    public override void DecodeResponse()
    {
        playerID = int.Parse(response);
    }
}

public class PositionForm : FormToPHP
{
    public Vector3 position;
    public string character;
    public PositionForm(string url, EventTypePHP type, Vector3 position, string character) : base(url, type)
    {
        this.url = url;
        this.type = type;
        this.position = position;
        this.character = character;
    }

    public override void SaveForm()
    {
        form.AddField("type", type.ToString());
        form.AddField("positionX", position.x.ToString());
        form.AddField("positionY", position.y.ToString());
        form.AddField("positionZ", position.z.ToString());
        form.AddField("character", character);
    }

    public override void Receive()
    {
        response = www.downloadHandler.text;
        Debug.Log(response);

        isDone = true;
    }

    public override void DecodeResponse()
    {
        throw new NotImplementedException();
    }
}
