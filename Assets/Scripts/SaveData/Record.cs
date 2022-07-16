using UnityEngine;

//Пространство имен, в котором находится все для сохранений
namespace SaveData
{
    //Сериализуемый класс, описывающий текущие параметры настроек игры
    [System.Serializable]
    public class Record
    {
        public int score = 0;
        public System.DateTime date = System.DateTime.Now;

        public Record()
        {
            score = 0;
            date = System.DateTime.Now;
        }

        public Record(int score, System.DateTime dataTime)
        {
            this.score = score;
            date = dataTime;
        }

        public Record(int score)
        {
            this.score = score;
            date = System.DateTime.Now;
        }

        public string DateTimeToString()
        {
            return date.ToString("dd/MM/yyyy") + " " + date.ToString("hh:mm:ss");
        }

        public override string ToString()
        {
            return  DateTimeToString() + " " + score.ToString();
        }

        public static bool operator >(Record r1, Record r2) => r1.score > r2.score;

        public static bool operator <(Record r1, Record r2) => r1.score < r2.score;

        public static bool operator ==(Record r1, Record r2) => r1.score == r2.score;

        public static bool operator !=(Record r1, Record r2) => !(r1.score == r2.score);

        public override bool Equals(object obj)
        {
            if (obj is Record record) return score == record.score;
            return false;
        }

        public override int GetHashCode()
        {
            return score.GetHashCode();
        }
    }
}
