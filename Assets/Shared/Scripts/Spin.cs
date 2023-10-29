using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Скорость вращения объекта в градусах в секунду
    public Vector3 rotationAxis = Vector3.right; // Ось вращения (например, Vector3.up для вращения вокруг вертикальной оси)

    private void FixedUpdate()
    {
        // Вычисляем угол вращения в радианах на основе скорости
        float rotationAngle = rotationSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime;

        // Создаем кватернион для поворота
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);

        // Применяем поворот к объекту
        transform.rotation *= rotation;
    }
}
