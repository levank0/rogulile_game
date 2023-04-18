using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public List<GameObject> cardLayouts; // список макетов карт
    public GameObject cardPrefab; // префаб карты
    public Transform spawnPoint; // точка по€влени€
    public int maxRooms = 10; // максимальное количество комнат
    public float roomSize = 10f; // размер каждой комнаты

    private List<GameObject> rooms = new List<GameObject>(); // список всех созданных комнат
    private GameObject previousRoom; // предыдуща€ комната

    void Start()
    {
        SpawnRoom();
    }

    void SpawnRoom()
    {
        int direction = Random.Range(0, 4); // случайное направление

        // ѕровер€ем, была ли создана предыдуща€ комната
        Vector3 offset = Vector3.zero;
        if (previousRoom != null)
        {
            // ≈сли да, то смещаем точку по€влени€ относительно предыдущей комнаты
            switch (direction)
            {
                case 0: // влево
                    offset = new Vector3(-roomSize, 0, 0);
                    break;
                case 1: // вправо
                    offset = new Vector3(roomSize, 0, 0);
                    break;
                case 2: // вверх
                    offset = new Vector3(0, roomSize, 0);
                    break;
                case 3: // вниз
                    offset = new Vector3(0, -roomSize, 0);
                    break;
            }
            spawnPoint.position = previousRoom.transform.position + offset;
        }
        
        

        

        // ≈сли мы достигли максимального количества комнат, то останавливаем генерацию
        if (rooms.Count >= maxRooms)
        {
            return;
        }

        // √енерируем следующую комнату
        Invoke("SpawnRoom", 2);
    }
}