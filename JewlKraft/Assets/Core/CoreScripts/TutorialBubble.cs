using UnityEngine;
using TMPro;

public class TutorialBubble : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private string tutorialID; // UNIQUE ID
    [TextArea]
    [SerializeField] private string message;

    [Header("Visual")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.2f, 0);

    private GameObject bubbleInstance;

    private void Start()
    {
        // Has this tutorial already been completed?
        if (PlayerPrefs.GetInt(tutorialID, 0) == 1)
        {
            // Already used → do not show
            Destroy(this);
            return;
        }

        bubbleInstance = Instantiate(
            bubblePrefab,
            transform.position + offset,
            Quaternion.identity,
            transform
        );

        bubbleInstance
            .GetComponentInChildren<TextMeshProUGUI>()
            .text = message;
    }

    public void HideBubble()
    {
        PlayerPrefs.SetInt(tutorialID, 1);
        PlayerPrefs.Save();

        if (bubbleInstance != null)
            Destroy(bubbleInstance);

        Destroy(this);
    }
}
