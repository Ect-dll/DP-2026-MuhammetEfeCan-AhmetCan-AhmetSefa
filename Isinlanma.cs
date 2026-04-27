using UnityEngine;

public class Isinlanma : MonoBehaviour
{
    public Transform hedef;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "karakter")
        {
            other.transform.position = hedef.position;
        }
    }
}