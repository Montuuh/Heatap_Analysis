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

public class HeatMapRepresentation : MonoBehaviour
{
    public int gridWidth;
    public int gridHeight;

    public InformationType informationType;
    public Vector3 initialPosition;

    public Grid grid;
    public DataReader reader;

    private float maxWeightNormalized = 10f;

    private Cell[,] gridArray;
    public GameObject objectToInstantiate;

    public Gradient gradient;

    public HeatMapRepresentation(int gridWidth, int gridHeight, float cellSize, InformationType infoType, DataReader reader, Grid grid, Vector3 initialPos, Gradient gradient, GameObject prefab)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.informationType = infoType;
        this.reader = reader;
        this.grid = grid;
        this.initialPosition = initialPos;
        this.gradient = gradient;

        this.objectToInstantiate = prefab;

        this.gridArray = new Cell[gridWidth, gridHeight];

        DistributeWeights(informationType);

        GenerateHeatMap();
    }
    void Update()
    {
        // If F5 pressed, get positions from bbdd
        if (Input.GetKeyDown(KeyCode.F5))
        {
            HeatMapRepresentation heatMap = new HeatMapRepresentation(gridWidth, gridHeight, 1, informationType, reader, grid, initialPosition, gradient, objectToInstantiate);
        }

        // If F6 pressed, get jumps from bbdd
        if (Input.GetKeyDown(KeyCode.F6))
        {
            HeatMapRepresentation heatMap = new HeatMapRepresentation(gridWidth, gridHeight, 1, informationType, reader, grid, initialPosition, gradient, objectToInstantiate);
        }

        // If F7 pressed, get attacks from bbdd
        if (Input.GetKeyDown(KeyCode.F7))
        {
            HeatMapRepresentation heatMap = new HeatMapRepresentation(gridWidth, gridHeight, 1, informationType, reader, grid, initialPosition, gradient, objectToInstantiate);
        }
    }
    void DistributeWeights(InformationType informationType)
    {
        switch (informationType)
        {
            case InformationType.HIT:
                break;
            case InformationType.DEATH:
                break;
            case InformationType.ATTACK:
                break;
            case InformationType.JUMP:
                break;
            case InformationType.CHECKPOINT:
                break;
            case InformationType.HEAL:
                break;
            case InformationType.INTERACTUABLE:
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