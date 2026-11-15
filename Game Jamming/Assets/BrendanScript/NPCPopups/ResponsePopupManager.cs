using System.Collections;
using TMPro;
using UnityEngine;

public class ResponsePopupManager : MonoBehaviour
{
    public static ResponsePopupManager Instance { get; private set; }

    [Header("Response Popup")]
    [SerializeField] private GameObject responsePopupPrefab;
    [SerializeField] private Transform responseSpawnPoint;
    [SerializeField] private Transform responsePopupParent;

    [Header("Settings")]
    [SerializeField] private float minResponseDuration = 2f;
    [SerializeField] private float maxResponseDuration = 5f;
    [SerializeField] private float characterDelay = 0.05f;

    private GameObject activePopup;
    private Coroutine activeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowResponse(string responseMessage)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        if (activePopup != null)
        {
            Destroy(activePopup);
        }

        activeCoroutine = StartCoroutine(ResponseRoutine(responseMessage));
    }

    private IEnumerator ResponseRoutine(string message)
    {
        activePopup = Instantiate(
            responsePopupPrefab,
            responseSpawnPoint.position,
            Quaternion.identity,
            responsePopupParent);

        TextMeshProUGUI popupText = activePopup.GetComponentInChildren<TextMeshProUGUI>();

        if (popupText == null)
        {
            Destroy(activePopup);
            activePopup = null;
            yield break;
        }

        popupText.text = "";

        foreach (char letter in message)
        {
            popupText.text += letter;
            yield return new WaitForSeconds(characterDelay);
        }

        yield return new WaitForSeconds(Random.Range(minResponseDuration, maxResponseDuration));

        if (activePopup != null)
        {
            Destroy(activePopup);
            activePopup = null;
        }

        activeCoroutine = null;
    }
}