using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{


    public GameObject target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
    }
    private void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), Time.deltaTime * speed);
    }
}
