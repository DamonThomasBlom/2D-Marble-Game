using UnityEngine;

public class PlinkoSlot : MonoBehaviour
{
    public string slotName = "Default";
    public enum SlotEffect
    {
        Release,
        TimesTwo,
        TimesThree
    }

    public SlotEffect slotEffect = SlotEffect.Release;
    public PlinkoMiniGameController plinkoMiniGameController;

    private void Start()
    {
        plinkoMiniGameController = GetComponentInParent<PlinkoMiniGameController>();    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PinkoBall"))
        {
            plinkoMiniGameController.ResetBallPosition(other.gameObject);
            TriggerEffect();
        }
    }

    public int count = 0;

    public void TriggerEffect()
    {
        count++;
        // You can customize different slot behavior here
        switch (slotEffect)
        {
            case SlotEffect.TimesTwo:
                plinkoMiniGameController.DoubleBallCount();
                break;

            case SlotEffect.TimesThree:
                plinkoMiniGameController.TripleBallCount();
                break;

            case SlotEffect.Release:
                plinkoMiniGameController.Release();
                break;
        }
    }

    public void Die() 
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        GetComponent<Collider2D>().enabled = false;
    }
}
