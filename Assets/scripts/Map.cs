using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> roomPrefabs; //список макетов комнат
    public int numberOfRooms; //количество комнат на карте
    public float roomWidth = 50; //ширина каждой комнаты
    public float roomHeight = 50; //высота каждой комнаты
    public bool showGizmos; //отображение гизмо


    private List<GameObject> spawnedRooms = new List<GameObject>(); // список созданных комнат
    private void Start()
    {
        GenerMap();
    }

    private void GenerMap()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            
            //выбираем случайный макет комнаты
            int randomIndex = Random.Range(0, roomPrefabs.Count);
            GameObject newRoom = Instantiate(roomPrefabs[randomIndex]);
            float roomPositionX =i * roomWidth;
            float roomPositionY = 0f /*i* roomHeight*/;
            newRoom.transform.position = new Vector2(roomPositionX, roomPositionY); //размещаем комнаты на карте
            spawnedRooms.Add(newRoom); //добавляем созданную комнату в список
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        for (int i = 0; i < spawnedRooms.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(spawnedRooms[i].transform.position, spawnedRooms[i + 1].transform.position);
        }
    }
}
/*for (int i = 1; i < spawnedRooms.Count; i++)
{
    //выбираем случайную точку из предыдущей комнаты и текущей
    Vector2 prevRoomEnd = spawnedRooms[i - 1].GetComponentInChildren<Room>().GetRandomEndPoint();
    Vector2 currentRoomStart = spawnedRooms[i].GetComponentInChildren<Room>().GetRandomStartPoint();

    //создаем туннель
    GameObject tunnel = new GameObject("Tunnel");
    LineRenderer lineRenderer = tunnel.AddComponent<LineRenderer>();
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;
    lineRenderer.material.color = Color.gray;
    lineRenderer.SetPositions(new Vector3[] { prevRoomEnd, currentRoomStart });
}*/
