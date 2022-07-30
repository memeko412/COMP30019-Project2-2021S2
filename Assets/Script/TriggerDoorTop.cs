using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorTop : MonoBehaviour
{
    Spawn spawn;
    void Start() {
		spawn = FindObjectOfType<Spawn> ();
	}
    void OnTriggerEnter(Collider colid) {
        spawn.SetWhichDoorIsOpen(3); //top door
        if (spawn.GetDoorIsOpen() == false) {
            spawn.SetDoorIsOpen(true);
        }
    }
}
