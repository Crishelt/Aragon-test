using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreValue = 10;
    Scoreboard scoreboard;

    private void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        parent = GameObject.FindGameObjectWithTag("SpawnRuntime").transform;
    }

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"hit by {other.gameObject.name}");
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        scoreboard.IncreaseScore(scoreValue);
        Destroy(gameObject);
    }

}
