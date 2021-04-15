using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    [HideInInspector]
    public bool isPressed;
    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        isPressed = false;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = !isClicked;
    }
}