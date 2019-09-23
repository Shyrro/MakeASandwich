using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 10.0f;
    private float _inputX = 0f;
    [HideInInspector]
    public static bool lookingLeft = true;

    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        _inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        if (_inputX > 0 && lookingLeft)
        {
            transform.Rotate(0, 180, 0);
            lookingLeft = false;
        }
        else if (_inputX < 0 && !lookingLeft)
        {
            transform.Rotate(0, -180, 0);
            lookingLeft = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 force = new Vector3(_inputX * _speed * Time.fixedDeltaTime, 0, 0);
        rb.AddForce(force, ForceMode.VelocityChange);
        //var delta = _speed * Time.fixedDeltaTime;
        //var x = _inputX * delta;
        //var newPos = rb.position + new Vector3(x, 0, 0);

        //transform.position = Vector3.Lerp(rb.position, newPos, delta);
    }

}