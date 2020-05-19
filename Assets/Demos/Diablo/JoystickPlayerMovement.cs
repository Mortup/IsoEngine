using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Animator> animators;
    [SerializeField] private float speed;

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float inputMagnitude = input.magnitude;

        transform.position = transform.position + (Vector3)(input * speed * Time.deltaTime);

        foreach (Animator a in animators) {
            a.SetFloat("velX", input.x);
            a.SetFloat("velY", input.y);
            a.SetFloat("velocity", inputMagnitude);
        }
    }
}
