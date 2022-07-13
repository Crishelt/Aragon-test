using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction firing;
    [SerializeField] GameObject[] lasers;
    [SerializeField] float speed = 30f;
    [SerializeField] float rotationSpeed = 9f;
    [SerializeField] float xRange = 15f;
    [SerializeField] float yRange = 9f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = -0.5f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        movement.Enable();
        firing.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Disabling playerController");
        movement.Disable();
        firing.Disable();
        EnableEmission(false);

    }
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float newXPos = (speed * xThrow * Time.deltaTime);
        float newYPos = (speed * yThrow * Time.deltaTime);

        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + newXPos, -xRange, xRange),
            Mathf.Clamp(transform.localPosition.y + newYPos, -yRange, yRange),
            transform.localPosition.z
        );
    }
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            Quaternion.Euler(pitch, yaw, roll),
            Time.deltaTime * rotationSpeed
        );
    }

    private void ProcessFiring()
    {
        bool isFiring = firing.ReadValue<float>() == 1;
        EnableEmission(isFiring);
    }

    private void EnableEmission(bool enable)
    {
        foreach (var laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = enable;
        }
    }
}
