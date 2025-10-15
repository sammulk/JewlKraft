using UnityEngine;

public class Gem : MonoBehaviour
{
    public int hitsToBreak = 3;
    private int currentHits = 0;

    public void OnHit()
    {
        currentHits++;

        if (currentHits >= hitsToBreak)
        {
            BreakGem();
        }
    }

    void BreakGem()
    {
        //TODO
        Destroy(gameObject);
    }
}
