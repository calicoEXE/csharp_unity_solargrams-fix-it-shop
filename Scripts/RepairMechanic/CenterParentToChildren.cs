using UnityEngine;

public class CenterParentToChildren : MonoBehaviour
{
    void Start()
    {
        // Call the function to center the parent to its children
        CenterParentToChildrenPosition();
    }

    void CenterParentToChildrenPosition()
    {
        // Get all children of the parent
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        // Skip the parent itself
        if (children.Length > 1)
        {
            // Calculate the center position of all children
            Vector3 centerPosition = Vector3.zero;
            foreach (Transform child in children)
            {
                centerPosition += child.position;
            }
            centerPosition /= (children.Length - 1); // Subtracting 1 to account for the parent

            // Set the parent's position to the calculated center position
            transform.position = centerPosition;
        }
    }
}
