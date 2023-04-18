using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public int maxRooms;
    public int minRoomSize;
    public int maxRoomSize;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] cornerTiles;
    public GameObject[] tunnelTiles;

    private int[,] map;
    private List<Room> rooms;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Создаем двумерный массив для представления карты
        map = new int[mapWidth, mapHeight];
        rooms = new List<Room>();

        // Заполняем карту стенами
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                map[x, y] = 1;
            }
        }

        // Создаем первую комнату
        Room firstRoom = new Room(mapWidth / 2, mapHeight / 2, Random.Range(minRoomSize, maxRoomSize + 1), Random.Range(minRoomSize, maxRoomSize + 1));
        rooms.Add(firstRoom);
        CreateRoom(firstRoom);

        // Создаем остальные комнаты
        for (int i = 1; i < maxRooms; i++)
        {
            Room newRoom = GenerateRoom();
            bool roomIntersects = false;

            // Проверяем, пересекается ли новая комната с уже созданными комнатами
            foreach (Room room in rooms)
            {
                if (newRoom.Intersect(room))
                {
                    roomIntersects = true;
                    break;
                }
            }

            // Если комната не пересекается с другими, создаем ее и соединяем с предыдущей комнатой
            if (!roomIntersects)
            {
                rooms.Add(newRoom);
                CreateRoom(newRoom);

                int previousRoomIndex = rooms.Count - 2;
                int tunnelStartX = rooms[previousRoomIndex].x + rooms[previousRoomIndex].width / 2;
                int tunnelStartY = rooms[previousRoomIndex].y + rooms[previousRoomIndex].height / 2;
                int tunnelEndX = newRoom.x + newRoom.width / 2;
                int tunnelEndY = newRoom.y + newRoom.height / 2;

                if (Random.Range(0, 2) == 0)
                {
                    CreateHorizontalTunnel(tunnelStartX, tunnelEndX, tunnelStartY);
                    CreateVerticalTunnel(tunnelStartY, tunnelEndY, tunnelEndX);
                }
                else
                {
                    CreateVerticalTunnel(tunnelStartY, tunnelEndY, tunnelStartX);
                    CreateHorizontalTunnel(tunnelStartX, tunnelEndX, tunnelEndY);
                }
            }
        }

        DrawMap();
    }

    void DrawMap()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                if (map[i, j] == 1)
                {
                    Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (map[i,j] == 0)
                {
                    Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (map[i, j] == 2)
                {
                    Instantiate(cornerTiles[Random.Range(0, cornerTiles.Length)], new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (map[i, j] == 3)
                {
                    Instantiate(tunnelTiles[Random.Range(0, tunnelTiles.Length)], new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    Room GenerateRoom()
    {
        int roomWidth = Random.Range(minRoomSize, maxRoomSize + 1);
        int roomHeight = Random.Range(minRoomSize, maxRoomSize + 1);
        int roomX = Random.Range(1, mapWidth - roomWidth - 1);
        int roomY = Random.Range(1, mapHeight - roomHeight - 1);

        Room newRoom = new Room(roomX, roomY, roomWidth, roomHeight);

        return newRoom;
    }

    void CreateRoom(Room room)
    {
        for (int x = room.x; x < room.x + room.width; x++)
        {
            for (int y = room.y; y < room.y + room.height; y++)
            {
                if (x == room.x || x == room.x + room.width - 1 || y == room.y || y == room.y + room.height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }

        // Создаем угловые тайлы
        map[room.x, room.y] = 2;
        map[room.x + room.width - 1, room.y] = 2;
        map[room.x, room.y + room.height - 1] = 2;
        map[room.x + room.width - 1, room.y + room.height - 1] = 2;
    }

    void CreateHorizontalTunnel(int startX, int endX, int y)
    {
        for (int x = Mathf.Min(startX, endX); x <= Mathf.Max(startX, endX); x++)
        {
            map[x, y] = 3;
        }
    }

    void CreateVerticalTunnel(int startY, int endY, int x)
    {
        for (int y = Mathf.Min(startY, endY); y <= Mathf.Max(startY, endY); y++)
        {
            map[x, y] = 3;
        }
    }
}

public class Room
{
    public int x;
    public int y;
    public int width;
    public int height;

   
public Room(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public bool Intersect(Room other)
    {
        if (x >= other.x + other.width || other.x >= x + width)
        {
            return false;
        }

        if (y >= other.y + other.height || other.y >= y + height)
        {
            return false;
        }

        return true;
    }
}