using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Door : MonoBehaviour {
	Animation doorOpen;
    GameObject theDoor;
	Transform aim;
	void Start() {
		if (GameObject.FindGameObjectWithTag("Player")!=null) {
            aim = GameObject.FindGameObjectWithTag ("Player").transform;
        }
		theDoor = FindClosestDoor();
	}
	public GameObject FindClosestDoor()
    {
        GameObject[] allDoors;
		allDoors = GameObject.FindGameObjectsWithTag("DoorDown");

        GameObject closestDoor = null;
        float minimumDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;
        foreach (GameObject door in allDoors)
        {
            float currentDistance = (door.transform.position - playerPosition).sqrMagnitude;
            if (currentDistance < minimumDistance)
            {
				minimumDistance = currentDistance;
                closestDoor = door;
            }
        }
        return closestDoor;
    }

	void Update() {
		theDoor = FindClosestDoor();

	} 
	void OnTriggerEnter ( Collider obj){
		if (obj.tag == "Player" && GameObject.FindGameObjectWithTag("Enemy") == null) {
			if(theDoor) {
				theDoor.GetComponent<Animation>().Play("open");
			}
		}
	}

	void OnTriggerExit ( Collider obj  ){
		if (obj.tag == "Player" && GameObject.FindGameObjectWithTag("Enemy") == null) {
			theDoor.GetComponent<Animation>().Play("close");
		}
	}

	public GameObject getGameObject() {
		return theDoor;
	}


}