using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;

    private Touch touch;
    private float speedMod;

    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    void Start()
    {
        speedMod = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
        this.transform.Translate(horizontalMove, 0, verticalSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedMod,
                    transform.position.y,
                    transform.position.z);
            }
        }

        if (transform.position.x < leftLimit.position.x)
        {
            transform.position = new Vector3(leftLimit.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }
        else if (transform.position.x > rightLimit.position.x)
        {
            transform.position = new Vector3(rightLimit.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }
    }
}
