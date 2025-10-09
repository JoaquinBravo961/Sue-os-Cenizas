using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Text AmmosText;
    public int AmmosAmount;

    public static Ammo instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        AmmosText.text = "x "+AmmosAmount.ToString();
    }

    public void SubItem(int subItemAmount)
    {
        AmmosAmount += subItemAmount;
        AmmosText.text = "x " + AmmosAmount.ToString();

    }
}
