using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlotScript : MonoBehaviour, IPointerClickHandler
{
    //ITEM DATA// 
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public ItemTypeEnum itemType;

    //ITEM SLOT//
    [SerializeField]
    private Image itemImage;

    //EQUIPPED SLOT//
    [SerializeField]
    private EquippedSlot headSlot, bodySlot, legSlot, shirtSlot, mainHandSlot, offHandSlot, relicSlot, amuletSlot;

    public GameObject selectedShader;
    public bool thisItemIsSelected;

    private InventoryManager inventoryManager;


    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemTypeEnum itemType)
    {
        //Check to see if the slot is already full
        if(isFull)
        {
            return quantity;
        }

        //Update Information
        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        itemImage.sprite = itemSprite;
        //Update quantity
        this.quantity = 1;
        isFull = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if(thisItemIsSelected)
        {
            EquipGear();
        }
        else{
            inventoryManager.DeselecteAllSlots();
            selectedShader.SetActive(true);
            thisItemIsSelected = true;
        }
    }

    private void EquipGear()
    {
        switch(itemType)
        {
            case ItemTypeEnum.head:
                headSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.body:
                bodySlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.shirt:
                shirtSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.legs:
                legSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.mainHand:
                mainHandSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.offHand:
                offHandSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.relic:
                relicSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
            case ItemTypeEnum.amulet:
                amuletSlot.EquipGear(itemSprite, itemName, itemDescription);
                break;
        }

        //Empty the slot
        EmptySlot();
    }

    private void EmptySlot()
    {
        itemImage.sprite = null;

    }

    public void OnRightClick()
    {
        //Create a new item
        GameObject itemToDrop = new GameObject(itemName);
        ItemScript newItem = itemToDrop.AddComponent<ItemScript>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        //CREATE AND MODIFY THE SRPITE RENDERER
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 1;
        sr.sortingLayerName = "Pickup";
        //add a collider
        itemToDrop.AddComponent<BoxCollider2D>();

        //Set the position of the item
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //Subtract the quantity
        this.quantity -= 1;
        if(this.quantity <= 0)
        {
            EmptySlot();
        }
    }
}
