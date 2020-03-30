using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityLight : RegularLight
{

    public List<Door> past_doors;
    public override void PowerOnAction(){ base.LightsOff(); }
    public override void PowerOffAction(){ base.LightsOn(); }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "robot")
        {
            foreach (Door i in past_doors)
            {
                
            }
        }
    }
}
