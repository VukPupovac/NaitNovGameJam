using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyPopup : MonoBehaviour, IPointerClickHandler
{
    private string responseMessage;

    public void SetResponse(string response)
    {
        responseMessage = response;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ResponsePopupManager.Instance.ShowResponse(responseMessage);

        Destroy(gameObject);
    }
}