using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyPopup : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Play audio here if we want a sound for the popup going away
        Destroy(gameObject);
    }

}
