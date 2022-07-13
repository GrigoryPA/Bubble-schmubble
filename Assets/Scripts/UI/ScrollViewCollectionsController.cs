using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollViewCollectionsController : MonoBehaviour
{
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

    private void InitItem(ref GameObject newItem, int kefPosX, StickerPack stickerPack)
    {
        newItem.name = newItem.name + " " + kefPosX.ToString();

        //������ ������ �������� � ������ �����
        RectTransform itemT = newItem.GetComponent<RectTransform>();
        itemT.localPosition = new Vector3(kefPosX * itemT.rect.width, -(itemT.pivot.y * itemT.rect.height), itemT.localPosition.z);
        
        //���������� ����� ��� ���������
        Image itemI = newItem.GetComponentInChildren<Image>();
        itemI.sprite = stickerPack.mainIcon;

        //���� ���� ����� �������� ��� ����������
        if (GameManager.purchasedAssets.purchasedStickerPacks.Contains(stickerPack))
        {
            itemI.color = normalColor; // ������ ��� ���������� ����
        }
        else
        {
            itemI.color = unavailableColor; //������ ���� ����������������
            newItem.GetComponentInChildren<Button>().interactable = false; //��������� ��������������
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
}
