using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorRight : MonoBehaviour
{
    Spawn spawn;
    void Start() {
		spawn = FindObjectOfType<Spawn> ();
	}
    void OnTriggerEnter(Collider colid) {
        spawn.SetWhichDoorIsOpen(2); //2 is right door
        if (spawn.GetCurrentWaveNumber()  < spawn.waves.Count&& spawn.GetCurrentWaveNumber() -2 >= -1) {
            spawn.SetCurrentWaveNumber(spawn.GetCurrentWaveNumber()-2);
        }
        if (spawn.GetDoorIsOpen() == false) {
            spawn.SetDoorIsOpen(true);
        }
    }
}
