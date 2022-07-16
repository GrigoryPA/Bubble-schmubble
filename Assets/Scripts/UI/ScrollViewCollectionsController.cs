using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollViewCollectionsController : MonoBehaviour
{
    public int width;
    public GameObject itemPrefab;
    public GameObject content;
    public Color normalColor;
    public Color selectedColor;
    public Color unavailableColor;
    public GameObject selectedItem;
    public UnityEvent<GameObject> OnValueChanged;

    private Dictionary<GameObject, StickerPack> items;

    private void Awake()
    {
        items = new Dictionary<GameObject, StickerPack>(GameManager.allStickerPacks.Count);
        SetContentSize();

        InitItem(ref itemPrefab, 0, GameManager.allStickerPacks[0]);//������������������� ������ ������ �������, ����� ��� ������������
        items.Add(itemPrefab, GameManager.allStickerPacks[0]);
        ChangeSelecte(itemPrefab);//���������� ��� ��� ��������� �� ���������

        //���������� ��� ��� ���������� ������� �������� � ��������
        for (int i = 1; i < GameManager.allStickerPacks.Count; i++)
        {
            //������� � �������������� ����� ������� ���������
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            InitItem(ref newItem, i, GameManager.allStickerPacks[i]);
            items.Add(newItem, GameManager.allStickerPacks[i]);

            //���� ����� �������� ���������� ������� ���������� ��������� � ��������� �� ������ ������ � ����
            if (GameManager.allStickerPacks[i] == GameManager.instance.selectedPack)
            {
                ChangeSelecte(newItem); //�������� ���� ������ ��� ���������
            }
        }

        OnValueChanged.AddListener(ChangeSelecte); //������ �� ������� ��������� ������ ����� ������� ������ ���������
        OnValueChanged.AddListener(ApplySelectionToGameManager);
    }

    private void InitItem(ref GameObject newItem, int pos, StickerPack stickerPack)
    {
        newItem.name = newItem.name + " " + pos.ToString();

        //������ ������ �������� � ������ �����
        RectTransform contentRT = content.GetComponent<RectTransform>();
        RectTransform itemT = newItem.GetComponent<RectTransform>();
        itemT.localPosition = new Vector3((pos % width) * itemT.rect.width - contentRT.pivot.x*contentRT.rect.width,
                                        -(itemT.pivot.y * itemT.rect.height) * (pos / width), 
                                        itemT.localPosition.z);
        
        //���������� ����� ��� ���������
        Image itemI = newItem.GetComponentInChildren<Image>();
        itemI.sprite = stickerPack.mainIcon;

        //���� ���� ����� �������� ��� ����������
        if (GameManager.purchasedAssets.purchasedStickerPacks.Contains(stickerPack))
        {
            newItem.GetComponentInChildren<Button>().interactable = true; //��������� ��������������
            itemI.color = normalColor; // ������ ��� ���������� ����
        }
        else
        {
            newItem.GetComponentInChildren<Button>().interactable = false; //��������� ��������������
            itemI.color = unavailableColor; //������ ���� ����������������
        }
    }

    //����� �� ������� �������, ��� ������ ������� ��������� �������
    public void OnItemSelected(GameObject obj) => OnValueChanged.Invoke(obj);

    //����� ��������� �������
    private void ChangeSelecte(GameObject newSelecte)
    {
        //����� ��������� � �������
        if (selectedItem)
        {
            ChangeColor(selectedItem, normalColor);
        }

        //���������� ����� ������
        selectedItem = newSelecte;

        //�������� ����� ������
        ChangeColor(selectedItem, selectedColor);
    }

    //�������� ����� �������
    private void ChangeColor(GameObject item, Color newColor)
    {
        Image image = item.GetComponentInChildren<Image>();
        image.color = newColor;
    }
    
    public void ApplySelectionToGameManager(GameObject obj)
    {
        GameManager.instance.selectedPack = items[selectedItem];
    }

    public static void SetSize(RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

    public void SetContentSize()
    {
        //������� ������ ������ ��������
        RectTransform contentRT = content.GetComponent<RectTransform>();
        RectTransform itemT = itemPrefab.GetComponent<RectTransform>();

        int kX = width;
        int kY = Mathf.CeilToInt(((float)GameManager.allStickerPacks.Count) / ((float)width));
        Vector2 newSize = new Vector2(kX, kY) * itemT.rect.size;
        SetSize(contentRT, newSize);
    }
}
