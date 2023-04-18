using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // �������� ����������� ��������
    public float xMin ; // ����������� �������� ���������� X
    public float xMax ; // ������������ �������� ���������� X
    public float yMin ; // ����������� �������� ���������� Y
    public float yMax ; // ������������ �������� ���������� Y

    public GameObject Bullet;
    public Transform BulletPosition;
    public float Period = 1;
    float timerFire;


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // �������� �������� ������ �����/������
        float verticalInput = Input.GetAxis("Vertical"); // �������� �������� ������ �����/����

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput); // ������� ������ ��������
        transform.Translate(moveDirection * speed * Time.deltaTime); // ���������� ������

        // ������������ ����������
        float xPosition = Mathf.Clamp(transform.position.x, xMin, xMax);
        float yPosition = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);

       if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            timerFire = 0;
        }
        timerFire = timerFire + Time.deltaTime;timerFire = timerFire + Time.deltaTime;
    }
}
