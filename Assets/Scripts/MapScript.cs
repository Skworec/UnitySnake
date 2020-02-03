using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public static MapScript instance;

    [SerializeField] GameObject wallTile;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private RaycastController rycstCntrl;
    [SerializeField] private GameObject FoodTile;
    public int Width
    {
        get { return width; }
        set
        {
            width = (value < MaxWidth && value > 0) ? value : MaxWidth;
        }
    }
    public int Height
    {
        get { return height; }
        set
        {
            height = (value < MaxHeight && value > 0) ? value : MaxHeight;
        }
    }
    private int MaxWidth = 19;
    private int MaxHeight = 29;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DataController.instance.onEat.AddListener(OnEatHandler);
        SpawnMap();
        rycstCntrl.IsActive = true;
        SpawnFood();
    }
    private void SpawnMap()
    {
        float tileSize = 0.4375f;
        for (int i = -Width / 2; i <= Width / 2; i++)
        {
            for (int j = Height / 2 - 1; j >= -Height / 2 ; j--)
            {
                if (i == -Width/2)
                {
                    Instantiate(wallTile, new Vector3((i - 1) * tileSize, j * tileSize), Quaternion.identity, gameObject.transform);
                }
                else if (i == Width/2)
                {
                    Instantiate(wallTile, new Vector3((i + 1) * tileSize, j * tileSize), Quaternion.Euler(0,0,180), gameObject.transform);
                }
                if (j == Height/2 - 1)
                {
                    Instantiate(wallTile, new Vector3(i * tileSize, (j + 1) * tileSize), Quaternion.Euler(0, 0, 270), gameObject.transform);
                }
                else if (j == -Height / 2)
                {
                    Instantiate(wallTile, new Vector3(i * tileSize, (j - 1) * tileSize), Quaternion.Euler(0, 0, 90), gameObject.transform);
                }
            }
        }
    }

    private void OnEatHandler()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        while (true)
        {
            int i = Random.Range(-Width / 2, Width / 2 + 1);
            int j = Random.Range(-Height / 2, Height / 2 - 1);
            Vector3 spawnpoint = new Vector3(i * 0.4375f, j * 0.4375f, 0);
            if (!rycstCntrl.Ray(spawnpoint, FoodTile.tag))
            {
                Instantiate(FoodTile, spawnpoint, Quaternion.identity, gameObject.transform);
                break;
            }
            else
                continue;
        }
    }
}
