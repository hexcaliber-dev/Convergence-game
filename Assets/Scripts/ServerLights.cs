using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class ServerLights : MonoBehaviour
{

    public bool status;

    // Start is called before the first frame update
    void Start()
    {
        status = false;
    }


    void ChangeLight()
    {
        
        status = !status;
        if (status)
        {
            foreach (Transform child in transform)
            {
                UnityEngine.Experimental.Rendering.Universal.Light2D light = child.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                light.color = Color.green;
            }
        }
    }


}
