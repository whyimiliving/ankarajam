using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public static PlayerHp instance;
    public GameObject endGameUi;
    public float maxHp;
    public float currentHp;
    public float hpRegen;
    [SerializeField] private GameObject sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (currentHp < maxHp)
        {
            currentHp += hpRegen * Time.deltaTime;
        }
        else
        {
            currentHp = maxHp;
        }
    }

    public void TakeDmg()
    {
        Instantiate(sfx);

        currentHp--;
        if (currentHp < 0)
        {
            Instantiate(endGameUi);
        }
    }
}
