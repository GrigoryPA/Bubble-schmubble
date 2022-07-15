using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashView : MonoBehaviour
{
    public Text moneyText;
    public Text crystalText;

    private void Update()
    {
        moneyText.text = GameManager.cashAccount.money.ToString();
        crystalText.text = GameManager.cashAccount.crystal.ToString();
    }
}
