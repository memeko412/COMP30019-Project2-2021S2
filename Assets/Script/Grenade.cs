using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionDelay = 2f;
    public float explosionRange = 3f;
    public LayerMask grenadeLayer;
    public GameObject explosionParticleEffectPrefab;
    float explosionCount;
    bool isExploded = false;
    public int explosionDamge;

    void Start()
    {
        explosionCount = explosionDelay;
    }


    void Update()
    {
        explosionCount -= Time.deltaTime;
        if (!isExploded && explosionCount <= 0) {
            GameObject explosionObject = Instantiate(explosionParticleEffectPrefab,transform.position,transform.rotation);
            Destroy(explosionObject,2f);
            Collider[] allCollidingObjects = Physics.OverlapSphere(transform.position,explosionRange,grenadeLayer);
            foreach (Collider affectedObjects in allCollidingObjects) {
                IDestroyable damagedObject = affectedObjects.GetComponent<IDestroyable>();
                if (damagedObject != null) {
                    damagedObject.DeductHealth(explosionDamge);
                }

            }
            FindObjectOfType<AudioManager>().Play("grenade");
            Destroy(gameObject);

        }
    }
}
