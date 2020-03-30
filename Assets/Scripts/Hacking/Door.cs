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
        SetPower(openInitially);
    }

    public override void PowerOnAction(){ OpenDoor();}
    public override void PowerOffAction() { CloseDoor(); }

    private void OpenDoor(){ anim.SetTrigger("Open");}
    private void CloseDoor(){ anim.SetTrigger("Close"); }

}
