using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    private SpriteSet currentSet;

    private void Start()
    {
        LoadSkin();
    }

    private void OnEnable()
    {
        SkinSelection.OnSkinSaved += LoadSkin;
    }
    private void OnDisable()
    {
        SkinSelection.OnSkinSaved -= LoadSkin;
    }

    public void LoadSkin()
    {
        if (GameManager.instance == null)
            return;

        currentSet = GameManager.instance.GetSavedSpriteSet();

        foreach (FieldInfo field in typeof(SpriteSet).GetFields())
        {
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (string.Equals(field.Name, renderer.gameObject.name, StringComparison.OrdinalIgnoreCase))
                {
                    renderer.sprite = SpriteStorage.instance.GetSprite(field.Name, (int)field.GetValue(currentSet));
                }
            }

        }
    }
}
