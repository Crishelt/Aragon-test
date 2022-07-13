using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject explosionPS;
    private UnityEvent deadEvent = new UnityEvent();

    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision with: {other.gameObject.tag}");
        isAlive = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Playercontroller>().enabled = false;
        foreach (MeshRenderer childRender in GetComponentsInChildren<MeshRenderer>())
        {
            childRender.enabled = false;
        }
        explosionPS.GetComponent<ParticleSystem>().Play();
        RaiseDeadEvent();
        Invoke("ReloadLevel", 1f);

    }

    private void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void RegisterForEvent(UnityAction action)
    {
        deadEvent.AddListener(action); // register action to receive the event callback
    }
    public void UnregisterEvent(UnityAction action)
    {
        deadEvent.RemoveListener(action); // unregister to stop receiving the event callback
    }
    private void RaiseDeadEvent()
    {
        deadEvent.Invoke(); // raise the event for all listeners
    }
}
