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

        InitItem(ref itemPrefab, 0, GameManager.allStickerPacks[0]);//проинициализировали первый обхект образец, чтобы его использовать
        items.Add(itemPrefab, GameManager.allStickerPacks[0]);
        ChangeSelecte(itemPrefab);//выставляем его как выбранный по умолчанию

        //проходимся пов сем оставшимся наборам стикеров в ресурсах
        for (int i = 1; i < GameManager.allStickerPacks.Count; i++)
        {
            //создаем и инициализируем новые объекты коллекции
            GameObject newItem = Instantiate(itemPrefab, content.transform);
            InitItem(ref newItem, i, GameManager.allStickerPacks[i]);
            items.Add(newItem, GameManager.allStickerPacks[i]);

            //если набор стикеров созданного обхекты колллекции совпадает с выбранным на данный момент в игре
            if (GameManager.allStickerPacks[i] == GameManager.instance.selectedPack)
            {
                ChangeSelecte(newItem); //отмечаем этот объект как выбранный
            }
        }

        OnValueChanged.AddListener(ChangeSelecte); //вешаем на событие изменения выбора метод который меняет выделение
        OnValueChanged.AddListener(ApplySelectionToGameManager);
    }

    private void InitItem(ref GameObject newItem, int kefPosX, StickerPack stickerPack)
    {
        newItem.name = newItem.name + " " + kefPosX.ToString();

        //ставим объект колекции в нужное место
        RectTransform itemT = newItem.GetComponent<RectTransform>();
        itemT.localPosition = new Vector3(kefPosX * itemT.rect.width, -(itemT.pivot.y * itemT.rect.height), itemT.localPosition.z);
        
        //выставляем окнку для коллекции
        Image itemI = newItem.GetComponentInChildren<Image>();
        itemI.sprite = stickerPack.mainIcon;

        //если этот набор стикеров уже приобретен
        if (GameManager.purchasedAssets.purchasedStickerPacks.Contains(stickerPack))
        {
            itemI.color = normalColor; // ставим ему нормальный цвет
        }
        else
        {
            itemI.color = unavailableColor; //ставим цвет заблокированного
            newItem.GetComponentInChildren<Button>().interactable = false; //запрещаем взамиодействие
        }
    }

    //метод на событии нажатия, для вызова события выделения объекта
    public void OnItemSelected(GameObject obj) => OnValueChanged.Invoke(obj);

    //метод выделения объекта
    private void ChangeSelecte(GameObject newSelecte)
    {
        //снять выделение с обхекта
        if (selectedItem)
        {
            ChangeColor(selectedItem, normalColor);
        }

        //запоминаем новый объект
        selectedItem = newSelecte;

        //выделяем новый объект
        ChangeColor(selectedItem, selectedColor);
    }

    //изменени цвета объекта
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
