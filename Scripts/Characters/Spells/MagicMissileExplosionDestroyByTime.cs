using UnityEngine;
using System.Collections;

public class MagicMissileExplosionDestroyByTime : MonoBehaviour {
    public int lifetime;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
