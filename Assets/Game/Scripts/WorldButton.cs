using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnButtonClicked;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Implement");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Implement");
    }
}
