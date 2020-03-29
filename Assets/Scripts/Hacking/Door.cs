using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator anim;
    [SerializeField] int charge_req;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OpenDoor(){ anim.SetTrigger("Open");}
    void CloseDoor(){ anim.SetTrigger("Close"); }

}
