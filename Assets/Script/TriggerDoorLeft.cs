using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorLeft : MonoBehaviour
{
    Spawn spawn;
    void Start() {
		spawn = FindObjectOfType<Spawn> ();
	}
    void OnTriggerEnter(Collider colid) {
        spawn.SetWhichDoorIsOpen(1); //left door
        if (spawn.GetDoorIsOpen() == false) {
            spawn.SetDoorIsOpen(true);
        }
    }
}
