using UnityEngine;

namespace SaveData
{
    //—ериализуемый класс, описывающий счет игрока
    [System.Serializable]
    public class CashAccount
    {
        public int money = 0;
        public int crystal = 0;

        public CashAccount() {}

        public CashAccount(int money, int crystal)
        {
            this.money = money;
            this.crystal = crystal;
        }

        public CashAccount(string nameCSV)
        {
            string[][] table = SaveManager.LoadTextAssetCSV(nameCSV);
            money = int.Parse(table[0][1]);
            crystal = int.Parse(table[1][1]);
        }
    }
}
