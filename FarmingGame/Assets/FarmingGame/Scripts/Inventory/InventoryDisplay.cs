using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [Header("----- Elements -----")]
    [SerializeField] private Transform cropContainerParent;
    [SerializeField] private UICropContainer uICropContainer;


    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();
        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer cropContainerInstance = Instantiate(uICropContainer, cropContainerParent);
            Sprite cropIcon = DataManager.Instance.GetCropIconFropCropType(items[i].cropType);
            cropContainerInstance.Configure(cropIcon, items[i].amount);
        }
    }
    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer containerInstance;
            if (i < cropContainerParent.childCount)
            {
                containerInstance = cropContainerParent.GetChild(i).GetComponent<UICropContainer>();
                containerInstance.gameObject.SetActive(true);
            }
            else
            {
                containerInstance = Instantiate(uICropContainer, cropContainerParent);
            }

            Sprite cropIcon = DataManager.Instance.GetCropIconFropCropType(items[i].cropType);
            containerInstance.Configure(cropIcon, items[i].amount);
        }

        int remainingContainer = cropContainerParent.childCount - items.Length;

        if (remainingContainer <= 0)
        {
            return;
        }

        for (int i = 0; i < remainingContainer; i++)
        {
            cropContainerParent.GetChild(items.Length + i).gameObject.SetActive(false);
        }

    }
}
