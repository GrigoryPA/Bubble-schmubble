using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace JumperGame
{
    public class GameController : MonoBehaviour
    {
        public LowerBorder lowerBorder;
        public UnityEvent<string> onUpdatedData;
        public UnityEvent onSimpeEndGame;

        public int currentScore = 0;
        public int Money { get => currentScore / 10; }
        public int Crystal { get => currentScore / 500; }

        private void OnEnable()
        {
            lowerBorder.onPlayerCollided.AddListener(StartSimpleEndGame);
        }

        public void TakeScore(int height)
        {
            currentScore = height;
            onUpdatedData.Invoke(currentScore.ToString());
        }

        private void StartSimpleEndGame() => onSimpeEndGame.Invoke();

        public void SetMoneyText(Text textUI) => textUI.text = Money.ToString();
        public void SetCrystalText(Text textUI) => textUI.text = Crystal.ToString();

        public void TakeAndSaveCash()
        {
            GameManager.TakeMoney(Money);
            GameManager.TakeCrystal(Crystal);
        }

        public void ContinueGame()
        {
            GameManager.cashAccount.money -= Money;
            GameManager.cashAccount.crystal -= Crystal;
            GameManager.SaveCashAccount();
        }
    }
}
