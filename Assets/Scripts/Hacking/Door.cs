using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PoweredObject
{

    public Animator anim;
    public bool openInitially;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        reset();
    }

    public override void PowerOnAction(){ OpenDoor();}
    public override void PowerOffAction() { CloseDoor(); }

    public void OpenDoor(){ anim.SetTrigger("Open");}
    public void CloseDoor(){ anim.SetTrigger("Close"); }
    public void reset(){ }

}
