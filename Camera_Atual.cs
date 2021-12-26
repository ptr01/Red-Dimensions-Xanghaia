using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Variaveis
    [SerializeField] private float rotationspeed = 1.0f;
    public Transform target, player;
    float mouseX, mouseY, analogX, analogY;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate() 
    {
        camcontrol();
    }

    void camcontrol()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationspeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationspeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        analogY = Mathf.Clamp(analogY, -35, 60);
        transform.LookAt(target);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
