using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("How many second will happen before the object destroys itself")]
    [SerializeField] float destroyInSeconds = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }

}
