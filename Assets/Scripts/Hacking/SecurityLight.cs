using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLight : RegularLight
{

    private Collider2D ownCollider;
    public List<Door> past_doors;
    public List<Door> future_doors;

    void Start()
    {
        ownCollider = GetComponent<Collider2D>();
    }
    public override void PowerOnAction(){ 
        base.LightsOff();
        ownCollider.enabled = false;
    }
    public override void PowerOffAction(){ 
        base.LightsOn();
        ownCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "robot"){ foreach (Door i in past_doors){ i.OpenDoor(); } Debug.Log("wtf"); }
        ownCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        foreach (Door i in past_doors)
            i.OpenDoor();
        foreach (Door i in future_doors)
            i.CloseDoor();
    }

    public void OnTriggerExit2D(Collider2D other) {
        // does nothing for now
    }

}
