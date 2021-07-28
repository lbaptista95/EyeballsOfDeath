using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class SkinSelection : MonoBehaviour
{
    public delegate void SkinEvents();
    public static event SkinEvents OnSkinSaved;


    [SerializeField] private SkinSelector[] skinSelectors;

    private void OnEnable()
    {
        SpriteSet spriteSet = GameManager.instance.GetSavedSpriteSet();
        //Load saved sprite set
        foreach (SkinSelector selector in skinSelectors)
        {
            foreach (FieldInfo field in typeof(SpriteSet).GetFields())
            {
                if (string.Equals(field.Name, selector.bodyPartPreview.gameObject.name, StringComparison.OrdinalIgnoreCase))
                {
                    selector.SetCharacterBodyPartSprite((int)field.GetValue(spriteSet));
                }
            }
        }
    }

    public void SaveCharacterSkin()
    {
        SpriteSet sprSet = new SpriteSet();

        foreach (SkinSelector selector in skinSelectors)
        {
            foreach (FieldInfo field in typeof(SpriteSet).GetFields())
            {
                if (string.Equals(field.Name, selector.bodyPartPreview.gameObject.name, 
                    StringComparison.OrdinalIgnoreCase)) //Checking if the game is setting the correct body part
                {
                    field.SetValue(sprSet, selector.spriteIndex);
                }
            }
        }

        string sprSetJson = JsonUtility.ToJson(sprSet);

        GameManager.instance.SaveCharacter(sprSetJson);

        OnSkinSaved?.Invoke();

        gameObject.SetActive(false);
    }
}
