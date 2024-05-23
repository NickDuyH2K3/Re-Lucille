using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour
{
    //Slot appearance
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;
    [SerializeField]
    private Image playerDisplayImage;
    //Item data
    [SerializeField]
    private ItemTypeEnum itemType = new ItemTypeEnum();
    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    //Slot status
    private bool slotInUse;


    public void EquipGear(Sprite itemSprite,string itemName, string itemDescription)
    {
        //Update image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = true;

        //Update information
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        
        //Update the display image
        playerDisplayImage.sprite = itemSprite;

        slotInUse = true;
    }
}
