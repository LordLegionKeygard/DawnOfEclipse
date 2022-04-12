using UnityEngine;


public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private Transform interactionTransform;
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    public virtual void Interact()
    {
        //Debug.Log("Interacting with " + transform.name);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.E))
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
