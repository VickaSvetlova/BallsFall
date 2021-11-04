using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
    public void DestroyParent()
    {
        Destroy(transform.parent);
    }
}