using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Cell
{
    public float weight;
}

public enum InformationType
{
    HIT,
    DEATH,
    ATTACK,
    JUMP,
    CHECKPOINT,
    HEAL,
    INTERACTUABLE,
    POSITION
}

public enum DisplayInfo
{
    ALL,
    PLAYER,
    ENEMIES
}

public class HeatMapRepresentation : MonoBehaviour
{

    public Gradient gradient;
    public InformationType informationType;

    [Header("Set other than 'ALL' on hit/death/attack")]
    public DisplayInfo displayInfo;

    [Header("Don't change")]
    public GameObject objectToInstantiate;

    public Grid grid;
    public DataReader reader;

    private int gridWidth = 130;
    private int gridHeight = 80;
    private Vector3 initialPosition = new Vector3(-33.5f, 0, -39.5f);

    private float maxWeightNormalized = 10f;

    private Cell[,] gridArray;

    public HeatMapRepresentation(int gridWidth, int gridHeight, float cellSize, InformationType infoType, DisplayInfo displayInfo, DataReader reader, Grid grid, Vector3 initialPos, Gradient gradient, GameObject prefab)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.informationType = infoType;
        this.displayInfo = displayInfo;
        this.reader = reader;
        this.grid = grid;
        this.initialPosition = initialPos;
        this.gradient = gradient;

        this.objectToInstantiate = prefab;

        this.gridArray = new Cell[gridWidth, gridHeight];

        DistributeWeights(informationType);

        GenerateHeatMap();
    }

    private void Awake()
    {
        gridWidth = 130;
        gridHeight = 80;
        initialPosition = new Vector3(-33.5f, 0, -39.5f);
    }
    void Update()
    {
        // If F1 pressed, get positions from bbdd
        if (Input.GetKeyDown(KeyCode.F1))
        {
            informationType = InformationType.POSITION;
            ReloadHeatMap();
        }

        // If F2 pressed, get jumps from bbdd
        if (Input.GetKeyDown(KeyCode.F2))
        {
            informationType = InformationType.JUMP;
            ReloadHeatMap();
        }


        // If F3 pressed, get attacks from bbdd
        if (Input.GetKeyDown(KeyCode.F3))
        {
            informationType = InformationType.ATTACK;
            ReloadHeatMap();
        }

        // If F4 pressed, get deaths from bbdd
        if (Input.GetKeyDown(KeyCode.F4))
        {
            informationType = InformationType.DEATH;
            ReloadHeatMap();
        }

        // If F5 pressed, get hits from bbdd
        if (Input.GetKeyDown(KeyCode.F5))
        {
            informationType = InformationType.HIT;
            ReloadHeatMap();
        }

        // If F6 pressed, get heals from bbdd
        if (Input.GetKeyDown(KeyCode.F6))
        {
            informationType = InformationType.HEAL;
            ReloadHeatMap();
        }

        // If F7 pressed, get checkpoint from bbdd
        if (Input.GetKeyDown(KeyCode.F7))
        {
            informationType = InformationType.CHECKPOINT;
            ReloadHeatMap();
        }

        // If F8 pressed, get interactuable from bbdd
        if (Input.GetKeyDown(KeyCode.F8))
        {
            informationType = InformationType.INTERACTUABLE;
            ReloadHeatMap();
        }
    }

    void ReloadHeatMap()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        new HeatMapRepresentation(gridWidth, gridHeight, 1, informationType, displayInfo, reader, grid, initialPosition, gradient, objectToInstantiate);
    }

    void DistributeWeights(InformationType informationType)
    {
        switch (informationType)
        {
            case InformationType.HIT:
                foreach (var position in reader.hitList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    switch (displayInfo)
                    {
                        case DisplayInfo.ALL:
                            gridArray[cellPos.x, cellPos.y].weight++;
                            break;
                        case DisplayInfo.PLAYER:
                            if(position.whoWasHit == "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        case DisplayInfo.ENEMIES:
                            if (position.whoWasHit != "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                break;
            case InformationType.DEATH:
                foreach (var position in reader.deathList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    switch (displayInfo)
                    {
                        case DisplayInfo.ALL:
                            gridArray[cellPos.x, cellPos.y].weight++;
                            break;
                        case DisplayInfo.PLAYER:
                            if (position.whoDied == "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        case DisplayInfo.ENEMIES:
                            if (position.whoDied != "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                break;
            case InformationType.ATTACK:
                foreach (var position in reader.attackList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    switch (displayInfo)
                    {
                        case DisplayInfo.ALL:
                            gridArray[cellPos.x, cellPos.y].weight++;
                            break;
                        case DisplayInfo.PLAYER:
                            if (position.whoAttacked == "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        case DisplayInfo.ENEMIES:
                            if (position.whoAttacked != "Ellen")
                            {
                                gridArray[cellPos.x, cellPos.y].weight++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                break;
            case InformationType.JUMP:
                foreach (var position in reader.jumpList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    gridArray[cellPos.x, cellPos.y].weight++;
                }
                break;
            case InformationType.CHECKPOINT:
                foreach (var position in reader.checkpointList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    gridArray[cellPos.x, cellPos.y].weight++;
                }
                break;
            case InformationType.HEAL:
                foreach (var position in reader.healList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    gridArray[cellPos.x, cellPos.y].weight++;
                }
                break;
            case InformationType.INTERACTUABLE:
                foreach (var position in reader.interactuableList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    gridArray[cellPos.x, cellPos.y].weight++;
                }
                break;
            case InformationType.POSITION:
                foreach (var position in reader.positionList)
                {
                    Vector3Int cellPos = grid.WorldToCell(position.position);
                    gridArray[cellPos.x, cellPos.y].weight++;
                }
                break;
            default:
                break;
        }
        
        //Normalize Scales
        if(gridArray.Length > 0)
        {
            float maxWeightFound = 0;

            foreach (var cell in gridArray)
            {
                if (cell.weight > maxWeightFound) maxWeightFound = cell.weight;
            }

            //Esta linea es una guarrada pq en el punto de spawn inicial se acumula demasiado peso y así se hace una mayor distinción de colores en el resto del mapa
            if (informationType == InformationType.POSITION) maxWeightFound /= 2;

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y].weight = gridArray[x, y].weight * maxWeightNormalized / maxWeightFound;
                }
            }
        }
    }

    void GenerateHeatMap()
    {
        for (int x = 0; x < gridArray.GetLength(0) / grid.cellSize.x; x++)
        {
            for (int y = 0; y < gridArray.GetLength(1) / grid.cellSize.x; y++)
            {
                GameObject cubeG = GameObject.Instantiate(objectToInstantiate);
                cubeG.transform.SetParent(GameObject.Find("DataRepresentation").transform);

                float widthScale = grid.cellSize.x - 0.2f;
                float heightScale = 0.6f;

                Vector3 newScale = new Vector3(widthScale, cubeG.transform.localScale.y * heightScale + cubeG.transform.localScale.y * gridArray[x,y].weight * heightScale, widthScale);
                cubeG.transform.localScale = newScale;

                cubeG.transform.position = new Vector3(initialPosition.x + x * grid.cellSize.x, cubeG.transform.localScale.y / 2, initialPosition.z + y * grid.cellSize.z);

                float time = (float)gridArray[x, y].weight / (float)maxWeightNormalized;
                Color color = gradient.Evaluate(time);
                cubeG.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}