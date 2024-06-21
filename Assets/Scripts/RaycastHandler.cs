using UnityEngine;
using UnityEngine.SceneManagement;
using static UIHandler;

public class RaycastHandler : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;
    public IInteractable interactableObject; // Renamed intrect to avoid confusion with interaction

    void Update()
    {
        bool rayHit = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, rayHit ? Color.green : Color.red);

        if (rayHit)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable != interactableObject) // Check if it's a different interactable object
                {
                    if (interactableObject != null)
                    {
                        if (SceneManager.GetActiveScene().name != "My Supermarket 1")
                            DisableInteractBtn(); // Disable interaction for the previous interactable
                        else
                            SupermarketLevelsManager.Instance.DisableInteractBtn();
                        interactableObject.NonInteract();
                    }
                    interactableObject = interactable; // Set the new interactable object
                    interactableObject.Interact(); // Interact with the new object
                }
            }
            else if (interactableObject != null)
            {
                if (SceneManager.GetActiveScene().name != "My Supermarket 1")
                    DisableInteractBtn(); // Disable interaction if no interactable object is hit
                else
                    SupermarketLevelsManager.Instance.DisableInteractBtn();
                interactableObject.NonInteract();
                interactableObject = null; // Clear the reference to the interactable object
            }
        }
        else if (interactableObject != null)
        {
            if (SceneManager.GetActiveScene().name != "My Supermarket 1")
                DisableInteractBtn(); // Disable interaction if no object is hit by the raycast
            else
                SupermarketLevelsManager.Instance.DisableInteractBtn();
            interactableObject.NonInteract();
            interactableObject = null; // Clear the reference to the interactable object
        }
    }
}
