using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("----- Elements ------")]
    [SerializeField] private Transform world;
    private Chunk[,] _grid;

    [Header("----- Data ------")]
    [SerializeField] private int gridSize;
    [SerializeField] private int gridScale;


    [Header("----- Data ------")]
    private WorldData _worldData;
    private string _dataPath;
    private bool _shouldSave;

    [Header("----- Chunk Meshes -----")]
    [SerializeField] private Mesh[] chunkShapes;

    private void OnEnable()
    {
        Chunk.OnUnlocked += ChunkOnUnlockedHandler;
        Chunk.OnPriceChanged += ChunkOnPriceChangedHandler;
    }



    private void Start()
    {
        _dataPath = Application.dataPath + "/WorldData.txt";

        LoadWorld();
        Initialize();

        InvokeRepeating("TrySaveGame", 1f, 1f);
    }

    private void OnDisable()
    {
        Chunk.OnUnlocked -= ChunkOnUnlockedHandler;
        Chunk.OnPriceChanged -= ChunkOnPriceChangedHandler;
    }

    private void Initialize()
    {
        for (int i = 0; i < world.childCount; i++)
        {
            world.GetChild(i).GetComponent<Chunk>().Initialize(_worldData.chunkPrices[i]);
        }

        InitializeGrid();
        UpdateChunkWalls();
        UpdateGridRenderers();
    }

    private void InitializeGrid()
    {
        _grid = new Chunk[gridSize, gridSize];

        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();

            Vector2Int chunkGridPosition = new Vector2Int((int)chunk.transform.position.x / gridScale,
                                                        (int)chunk.transform.position.z / gridScale);
            chunkGridPosition += new Vector2Int(gridSize / 2, gridSize / 2);

            _grid[chunkGridPosition.x, chunkGridPosition.y] = chunk;
            chunk.gameObject.name = chunkGridPosition.x.ToString() + chunkGridPosition.y.ToString();
        }
    }

    private void UpdateChunkWalls()
    {
        // Loop along the x axis
        for (int x = 0; x < _grid.GetLength(0); x++)
        {
            // Loop along the z axis
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                Chunk chunk = _grid[x, y];

                if (chunk == null) continue;
                // Our Chunks
                Chunk frontChunk = null;
                Chunk rigtChunk = null;
                Chunk backChunk = null;
                Chunk leftChunk = null;

                #region Check Grid
                #region Front Chunk
                if (IsValidGridPosition(x, y + 1))
                {
                    frontChunk = _grid[x, y + 1];
                }
                #endregion
                #region Right Chunk
                if (IsValidGridPosition(x + 1, y))
                {
                    rigtChunk = _grid[x + 1, y];
                }
                #endregion
                #region  Back Chunk
                if (IsValidGridPosition(x, y - 1))
                {
                    backChunk = _grid[x, y - 1];
                }
                #endregion
                #region Left Chunk
                if (IsValidGridPosition(x - 1, y))
                {
                    leftChunk = _grid[x - 1, y];
                }
                #endregion
                #endregion

                int configuration = 0;
                if (frontChunk != null && frontChunk.IsUnlocked())
                    configuration = configuration + 1;
                if (rigtChunk != null && rigtChunk.IsUnlocked())
                    configuration = configuration + 2;
                if (backChunk != null && backChunk.IsUnlocked())
                    configuration = configuration + 4;
                if (leftChunk != null && leftChunk.IsUnlocked())
                    configuration = configuration + 8;

                // We know the configuration of the chunk
                chunk.UpdateWalls(configuration);
            }
        }
    }

    private void UpdateGridRenderers()
    {
        for (int x = 0; x < _grid.GetLength(0); x++)
        {
            // Loop along the z axis
            for (int y = 0; y < _grid.GetLength(1); y++)
            {

                Chunk chunk = _grid[x, y];

                if (chunk == null) continue;

                if(chunk.IsUnlocked()) continue;

                Chunk frontChunk = IsValidGridPosition(x,y+1) ? _grid[x, y + 1] : null;
                Chunk rightChunk = IsValidGridPosition(x + 1, y) ? _grid[x + 1, y] : null;
                Chunk backChunk = IsValidGridPosition(x, y - 1) ? _grid[x, y - 1] : null;
                Chunk leftChunk = IsValidGridPosition(x-1, y) ? _grid[x - 1, y] : null;

                if(frontChunk != null && frontChunk.IsUnlocked())
                {
                    chunk.DisplayLockedElements();
                }
                else if (rightChunk!= null && rightChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (backChunk != null && backChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (leftChunk != null && leftChunk.IsUnlocked())
                    chunk.DisplayLockedElements();

            }
        }
    }

    private bool IsValidGridPosition(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
        {
            return false;
        }
        return true;
    }

    private void TrySaveGame()
    {
        Debug.Log("Trying to save");
        if (_shouldSave)
        {
            SaveWorld();
            _shouldSave = false;
        }
    }

    private void ChunkOnUnlockedHandler()
    {
        Debug.Log("Chunk Unlocked !");
        UpdateChunkWalls();
        UpdateGridRenderers();

        SaveWorld();
    }

    private void ChunkOnPriceChangedHandler()
    {
        _shouldSave = true;
        SaveWorld();
    }

    private void LoadWorld()
    {
        string data = "";

        if (!File.Exists(_dataPath))
        {
            FileStream fs = new FileStream(_dataPath, FileMode.Create);

            _worldData = new WorldData();
            for (int i = 0; i < world.childCount; i++)
            {
                int chunkInitialPrice = world.GetChild(i).GetComponent<Chunk>().GetInitialPrice();
                _worldData.chunkPrices.Add(chunkInitialPrice);
            }

            string worldDataString = JsonUtility.ToJson(_worldData, true);
            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);


            fs.Write(worldDataBytes);
            fs.Close();
        }
        else
        {
            data = File.ReadAllText(_dataPath);
            _worldData = JsonUtility.FromJson<WorldData>(data);

            if (_worldData.chunkPrices.Count < world.childCount)
                UpdateData();
        }
    }

    private void UpdateData()
    {
        // How mant chunks are missing in out data
        int missingData = world.childCount - _worldData.chunkPrices.Count;

        for (int i = 0; i < missingData; i++)
        {
            int chunkIndex = world.childCount - missingData + i;
            int chunkPrice = world.GetChild(chunkIndex).GetComponent<Chunk>().GetInitialPrice();
            _worldData.chunkPrices.Add(chunkPrice);
        }
    }

    private void SaveWorld()
    {
        if (_worldData.chunkPrices.Count != world.childCount)
        {
            _worldData = new WorldData();
        }

        for (int i = 0; i < world.childCount; i++)
        {
            int chunkCurrentPrice = world.GetChild(i).GetComponent<Chunk>().GetCurrentPrice();

            if (_worldData.chunkPrices.Count > i)
            {
                _worldData.chunkPrices[i] = chunkCurrentPrice;
            }
            else
            {
                _worldData.chunkPrices.Add(chunkCurrentPrice);
            }
        }
        string data = JsonUtility.ToJson(_worldData, true);

        File.WriteAllText(_dataPath, data);
        Debug.LogWarning("Data Saved !");
    }
}
