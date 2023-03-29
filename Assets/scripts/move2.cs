using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour
{
    public float speed; // скорость перемещения квадрата
    public float xMin ; // минимальное значение координаты X
    public float xMax ; // максимальное значение координаты X
    public float yMin ; // минимальное значение координаты Y
    public float yMax ; // максимальное значение координаты Y

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // получаем значения клавиш влево/вправо
        float verticalInput = Input.GetAxis("Vertical"); // получаем значения клавиш вверх/вниз

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput); // создаем вектор движения
        transform.Translate(moveDirection * speed * Time.deltaTime); // перемещаем объект

        // ограничиваем координаты
        float xPosition = Mathf.Clamp(transform.position.x, xMin, xMax);
        float yPosition = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
    }
}
