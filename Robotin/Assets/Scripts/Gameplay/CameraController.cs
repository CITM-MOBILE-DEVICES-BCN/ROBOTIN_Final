using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    float ymin = 1;
    float ymax = 1000;
    float y;
    public GameObject player;

    private void Awake()
    {
        player = GameManager.instance.currentLevel.player;
    }

    void Start()
    {

    }

    void SetY()
    {
        y = player.transform.position.y;;
        if (y < ymin)
        {
            y = ymin;
        }
        if (y > ymax)
        {
            y = ymax;
        }
    }

    void SetCameraPosition()
    {
        SetY();
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPosition();
    }
}
