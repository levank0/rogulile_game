using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour
{
    public float speed; // �������� ����������� ��������
    public float xMin ; // ����������� �������� ���������� X
    public float xMax ; // ������������ �������� ���������� X
    public float yMin ; // ����������� �������� ���������� Y
    public float yMax ; // ������������ �������� ���������� Y

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
    }
}
