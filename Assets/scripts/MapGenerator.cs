using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] rooms;
    public int mapSize;
    public float roomSize;
    private Vector2 centerPosition;

    private List<Vector2> positions = new List<Vector2>();

    private void Start()
    {
        centerPosition = new Vector2(mapSize / 2, mapSize / 2);
        GenerateMap();
    }

    private void GenerateMap()
    {
        positions.Add(centerPosition);
        foreach (GameObject room in rooms)
        {
            GenerateRoom(room);
        }
    }

    private void GenerateRoom(GameObject roomPrefab)
    {
        Vector2 newPosition = GetRandomPosition();
        Instantiate(roomPrefab, newPosition * roomSize, Quaternion.identity, transform);
        positions.Add(newPosition);
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 newPosition = Vector2.zero;
        int i = 0;
        do
        {
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0: // Up
                    newPosition = new Vector2(centerPosition.x, centerPosition.y + 1);
                    break;
                case 1: // Down
                    newPosition = new Vector2(centerPosition.x, centerPosition.y - 1);
                    break;
                case 2: // Left
                    newPosition = new Vector2(centerPosition.x - 1, centerPosition.y);
                    break;
                case 3: // Right
                    newPosition = new Vector2(centerPosition.x + 1, centerPosition.y);
                    break;
            }
            i++;
        } while (positions.Contains(newPosition) && i < 100);

        return newPosition;
    }
}
