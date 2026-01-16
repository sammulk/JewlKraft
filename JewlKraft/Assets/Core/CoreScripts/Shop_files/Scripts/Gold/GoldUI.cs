using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        GoldController.Instance.OnGoldChanged += UpdateGold;
        UpdateGold(GoldController.Instance.Gold);
    }

    private void OnDestroy()
    {
        if (GoldController.Instance != null)
            GoldController.Instance.OnGoldChanged -= UpdateGold;
    }

    private void UpdateGold(int amount)
    {
        goldText.text = $"Gold: {amount}";
    }
}
