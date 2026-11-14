using System.Collections;
using TMPro;
using UnityEngine;

public class NPCPopup : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform popupParent;

    [Header("Dialogue")]
    [SerializeField] private string[] messages;

    [Header("Timing")]
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 15f;
    [SerializeField] private float popupDuration = 3f;
    [SerializeField] private float characterDelay = 0.05f;

    [Header("Spawn Area")]
    [SerializeField] private PolygonCollider2D spawnArea;

    private void Start() // replace with game manager
    {
        StartNPCPopup();
    }

    public void StartNPCPopup()
    {
        StartCoroutine(PopupLoop());
    }

    private IEnumerator PopupLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            StartCoroutine(SpawnPopup());
        }
    }

    private IEnumerator SpawnPopup()
    {
        if (messages.Length == 0)
        {
            yield break;
        }

        string message = messages[Random.Range(0, messages.Length)];

        Vector2 spawnPosition = GetRandomPointInSpawnArea();

        GameObject popup = Instantiate(
            popupPrefab,
            spawnPosition,
            Quaternion.identity,
            popupParent);

        TextMeshProUGUI popupText =
            popup.GetComponentInChildren<TextMeshProUGUI>();

        if (popupText == null)
        {
            Destroy(popup);
            yield break;
        }

        yield return StartCoroutine(TypeText(popupText, message));

        float timer = 0f;

        while (timer < popupDuration && popup != null)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (popup != null)
        {
            Destroy(popup);
        }


}

private IEnumerator TypeText(TextMeshProUGUI popupText, string message)
{
    popupText.text = "";
    // Play audio here if we want a single continuous audio file

    foreach (char letter in message)
    {
        popupText.text += letter;
        // Play audio here if audio is per letter
        yield return new WaitForSeconds(characterDelay);
    }
}

private Vector2 GetRandomPointInSpawnArea()
{
    if (spawnArea == null)
    {
        return transform.position;
    }

    Bounds bounds = spawnArea.bounds;

    for (int i = 0; i < 100; i++)
    {
        Vector2 point = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));

        if (spawnArea.OverlapPoint(point))
        {
            return point;
        }
    }

    return bounds.center;
}
}