using UnityEngine;
using System.Collections;

public class FireballExplosionDestroyByContact : MonoBehaviour {
    public int lifetime;
    public int damage;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
    }
}
