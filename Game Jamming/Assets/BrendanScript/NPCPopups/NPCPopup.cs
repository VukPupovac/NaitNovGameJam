using System.Collections;
using TMPro;
using UnityEngine;

public class NPCPopup : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform popupParent;

    [Header("Portraits")]
    [SerializeField] private GameObject png1Prefab;
    [SerializeField] private GameObject png2Prefab;

    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Dialogue")]
    [SerializeField] private PopupDialogueSO dialogueList;

    [Header("Timing")]
    [SerializeField] private float minTime = 10f;
    [SerializeField] private float maxTime = 15f;
    [SerializeField] private float popupDuration = 3f;
    [SerializeField] private float characterDelay = 0.05f;

    [Header("Spawn Area")]
    [SerializeField] private PolygonCollider2D spawnArea;

    private void Start()
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
        if (dialogueList == null || dialogueList.dialogues.Count == 0)
        {
            yield break;
        }

        PopupDialogue dialogue = dialogueList.dialogues[
            Random.Range(0, dialogueList.dialogues.Count)];

        Vector2 spawnPosition = GetRandomPointInSpawnArea();

        GameObject popup = Instantiate(
            popupPrefab,
            spawnPosition,
            Quaternion.identity,
            popupParent);

        GameObject portrait = null;

        // Randomly choose portrait
        bool usePng1 = Random.value < 0.5f;

        if (usePng1)
        {
            portrait = Instantiate(
                png1Prefab,
                rightSpawnPoint.position,
                Quaternion.identity,
                rightSpawnPoint);
        }
        else
        {
            portrait = Instantiate(
                png2Prefab,
                leftSpawnPoint.position,
                Quaternion.identity,
                leftSpawnPoint);
        }

        TextMeshProUGUI popupText = popup.GetComponentInChildren<TextMeshProUGUI>();

        DestroyPopup destroyPopup = popup.GetComponent<DestroyPopup>();

        if (destroyPopup != null)
        {
            destroyPopup.SetResponse(dialogue.response);
        }

        if (popupText == null)
        {
            Destroy(popup);
            yield break;
        }

        yield return StartCoroutine(
            TypeText(
                popupText,
                dialogue.message));

        float timer = 0f;

        while (timer < popupDuration &&
               popup != null)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (popup != null)
        {
            Destroy(popup);
        }

        if (portrait != null)
        {
            Destroy(portrait);
        }
    }

    private IEnumerator TypeText(
        TextMeshProUGUI popupText,
        string message)
    {
        popupText.text = "";

        foreach (char letter in message)
        {
            popupText.text += letter;

            yield return new WaitForSeconds(
                characterDelay);
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