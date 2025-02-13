using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referensi ke Transform pemain
    public float offsetX = 0f; // Offset horizontal kamera dari pemain
    public float cameraY = 0f; // Posisi vertikal tetap kamera
    public float cameraZ = -10f; // Posisi Z tetap kamera untuk 2D

    void Start()
    {
        // Jika cameraY tidak diatur di Inspector, gunakan posisi Y awal kamera
        if (cameraY == 0f)
        {
            cameraY = transform.position.y;
        }

        // Jika cameraZ tidak diatur di Inspector, gunakan posisi Z awal kamera
        if (cameraZ == 0f)
        {
            cameraZ = transform.position.z;
        }
    }

    void LateUpdate()
    {
        // Set posisi kamera ke posisi pemain dengan offset horizontal dan posisi vertikal tetap
        transform.position = new Vector3(player.position.x + offsetX, cameraY, cameraZ);
    }
}