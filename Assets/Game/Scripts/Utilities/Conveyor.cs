using UnityEngine;

public class Conveyor : MonoBehaviour
{

    [SerializeField] 
    private float speed = 5;

    private void OnCollisionStay2D(Collision2D other)
    {
        other.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}