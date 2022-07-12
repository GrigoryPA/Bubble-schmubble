using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollViewCollectionsController : MonoBehaviour
{
    public GameObject content;
    public Color normalColor;
    public Color selectedColor;
    public GameObject defaultItem;
    public GameObject selectedItem;
    public UnityEvent<GameObject> OnValueChanged;
    public Dictionary<StickerPack, GameObject> items;

    private void Awake()
    {
        StickerPackController[] array = content.GetComponentsInChildren<StickerPackController>();
        items = new Dictionary<StickerPack, GameObject>(array.Length);
        foreach (var x in array)
        {
            items.Add(x.stikerPack, x.gameObject);
            ChangeColor(x.gameObject, normalColor);
        }

        if (GameManager.instance.selectedPack)
        {
            ChangeSelecte(items[GameManager.instance.selectedPack]);
        }
        else
        {
            ChangeSelecte(defaultItem);
        }

        OnValueChanged.AddListener(ChangeSelecte);
    }

    public void OnItemSelected(GameObject obj) => OnValueChanged.Invoke(obj);

    private void ChangeSelecte(GameObject newSelecte)
    {
        if (selectedItem)
        {
            ChangeColor(selectedItem, normalColor);
        }

        selectedItem = newSelecte;

        ChangeColor(selectedItem, selectedColor);
    }

    private void ChangeColor(GameObject item, Color newColor)
    {
        Image image = item.GetComponentInChildren<Image>();
        image.color = newColor;
    }
}
