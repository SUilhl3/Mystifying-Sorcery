using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteractions : MonoBehaviour
{
    public GameObject pageUI;

    [SerializeField] private bool inRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pageUI.SetActive(false);
    }

 
    public void onInteract(InputAction.CallbackContext context)
    {
        if(context.performed && inRange)
        {
            if(pageUI) {pageUI.SetActive(false);}
            else { pageUI.SetActive(true);}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = false;
            pageUI.SetActive(false);
        }
    }
}
