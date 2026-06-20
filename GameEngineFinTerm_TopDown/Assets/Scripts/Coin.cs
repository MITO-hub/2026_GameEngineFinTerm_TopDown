using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin(value);

            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX(SfxType.Coin);

            Destroy(gameObject);
        }
    }
}
