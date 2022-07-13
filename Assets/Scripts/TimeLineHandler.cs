using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine;

public class TimeLineHandler : MonoBehaviour
{
    PlayerCollision pc;
    private UnityAction playerDeadAction;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        pc = GameObject.FindObjectOfType<PlayerCollision>();
        // Create the action if it doesn't already exist
        //
        if (playerDeadAction == null)
            playerDeadAction = new UnityAction(PlayerDeadEventCallback);

        // Register to the event
        //
        pc.RegisterForEvent(playerDeadAction);
    }
    private void OnDisable()
    {
        // Unregister from the event when this object is disabled or destroyed
        //
        pc.UnregisterEvent(playerDeadAction);
    }

    private void PlayerDeadEventCallback()
    {
        var pd = GetComponent<PlayableDirector>();
        pd.Stop();
    }
}
