using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnctionPanel : MonoBehaviour
{
    public GameObject loading; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (loading.activeSelf)
        {
            loading.transform.Rotate(Vector3.back, 3, Space.World);
        }
    }
}
