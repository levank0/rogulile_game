using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dunr : MonoBehaviour
{
    public int dungeonWidth = 50;
    public int dungeonHeight = 50;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public float tileWidth = 1f;
    public float tileHeight = 1f;

    private int[,] map;
    private List<Vector2Int> visitedTiles;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Инициализируем карту
        map = new int[dungeonWidth, dungeonHeight];
        visitedTiles = new List<Vector2Int>();

        // Устанавливаем стены по краям карты
        for (int x = 0; x < dungeonWidth; x++)
        {
            map[x, 0] = 1;
            map[x, dungeonHeight - 1] = 1;
        }
        for (int y = 0; y < dungeonHeight; y++)
        {
            map[0, y] = 1;
            map[dungeonWidth - 1, y] = 1;
        }

        // Создаем первую комнату
        int startX = dungeonWidth / 2;
        int startY = dungeonHeight / 2;
        visitedTiles.Add(new Vector2Int(startX, startY));
        map[startX, startY] = 2;
        CreateRoom(startX, startY, 8);

        // Создаем новые комнаты
        int maxRooms = 10;
        int numRooms = 1;

        while (numRooms < maxRooms)
        {
            Vector2Int currentTile = GetRandomVisitedTile();
            int numConnections = CountConnectedRooms(currentTile);

            if (numConnections > 1 && Random.value < 0.2f)
            {
                continue;
            }

            CreateRoom(currentTile.x, currentTile.y, Random.Range(3, 10));
            numRooms++;
        }

        // Создаем блоки на основе карты
        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                if (map[x, y] == 1)
                {
                    Vector3 wallPosition = new Vector3(x * tileWidth, y * tileHeight, 0f);
                    Instantiate(wallPrefab, wallPosition, Quaternion.identity);
                }
                else if (map[x, y] == 2)
                {
                    Vector3 floorPosition = new Vector3(x * tileWidth, y * tileHeight, 0f);
                    Instantiate(floorPrefab, floorPosition, Quaternion.identity);
                }
            }
        }
    }

    void CreateRoom(int x, int y, int size)
    {
        for (int i = x - size / 2; i < x + size / 2; i++)
        {
            for (int j = y - size / 2; j < y + size / 2; j++)
            {
                if (i < 1 || i >= dungeonWidth - 1 || j < 1 || j >= dungeonHeight - 1)
                {
                    continue;
                }

                map[i, j] = 2;
                visitedTiles.Add(new Vector2Int(i, j));
            }
        }
    }


        int CountConnectedRooms(Vector2Int tile)
        {
        int count = 0;

        if (tile.x > 1 && map[tile.x - 1, tile.y] == 2)
        {
            count++;
        }
        if (tile.x < dungeonWidth - 2 && map[tile.x + 1, tile.y] == 2)
        {
            count++;
        }
        if (tile.y > 1 && map[tile.x, tile.y - 1] == 2)
        {
            count++;
        }
        if (tile.y < dungeonHeight - 2 && map[tile.x, tile.y + 1] == 2)
        {
            count++;
        }

        return count;
    }

    Vector2Int GetRandomVisitedTile()
    {
        return visitedTiles[Random.Range(0, visitedTiles.Count)];
    }
}