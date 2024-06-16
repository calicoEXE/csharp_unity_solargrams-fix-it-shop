using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Specify rotation speed
    public float rotationSpeed = 5f;

    // Specify rotation axis
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        // Get the current local rotation
        Vector3 currentRotation = transform.localEulerAngles;

        // Rotate the GameObject based on its local position and rotation
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationAmount);

        // Print the new local rotation (optional)
        //Debug.Log("New Local Rotation: " + transform.localEulerAngles);
    }
}
