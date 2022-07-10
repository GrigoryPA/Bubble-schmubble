using UnityEngine;
using System.IO;

//������������ ����, � ������� ��������� ��� ��� ����������
namespace SaveData
{
    //������������� �����, ����������� ������� ��������� �������� ����
    [System.Serializable]
    public class RecordsList
    {
        public Record[] records = new Record[10]; //�� �����������
        public const string KEY = "RECORDS";

        public RecordsList() { }

        public RecordsList(string nameCSV)
        {
            string[][] table = SaveManager.LoadTextAssetCSV(nameCSV);
            records = new Record[table.Length];
            for (int i = 0; i < table.Length; i++)
            {
                records[i] = new Record(int.Parse(table[i][0]), System.DateTime.Parse(table[i][1]));
            }
        }

        public int AddNewRecord(int score)
        {
            Record newRecord = new Record(score);
            int state = -1;
            for (int i = records.Length - 1; i >= 0; i--)
            {
                if (records[i] > newRecord)
                {
                    continue;
                }

                //������ ������������ �������� � ������ �� ����� ����� � ����� �� �������
                if (records[i] == newRecord)
                {
                    records[i] = newRecord;
                    return i;
                }

                //��������� ��������� ������� �������� ������ ������ � ����� �� �����
                if (records[i] < newRecord)
                {
                    state = i;
                    break;
                }
            }

            if (state < 0)
            {
                return -1;
            }
            else
            {
                //�������� ����� ������� � ���������� ������ �������� ��������
                for (int i = 0; i < state; i++)
                {
                    records[i] = records[i + 1];
                }
                records[state] = newRecord; //��������� �� ������� ����� ����� ������

                return state;
            }
        }
    }
}
