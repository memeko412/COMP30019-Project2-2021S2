using UnityEngine;
using System.Collections;

public class DestroyIfOffscreen : MonoBehaviour {
    // code from tututial
	void OnBecameInvisible () {
        Destroy(this.gameObject);
	}
}
