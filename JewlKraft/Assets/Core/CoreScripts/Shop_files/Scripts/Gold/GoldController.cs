using UnityEngine;

public class GoldController : MonoBehaviour
{
    public static GoldController Instance { get; private set; }

    [SerializeField] private int gold = 0;

    public int Gold => gold;

    private void Start()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }
    public System.Action<int> OnGoldChanged;
}
