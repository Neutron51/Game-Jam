using UnityEngine;
public class ResetWhenTooLow : MonoBehaviour
{
    public float lowestAllowed = -10.0f;
    void Update()
    {
        if (transform.position.y < lowestAllowed)
        {
            transform.position = Vector3.zero;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
