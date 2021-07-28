using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SkinSelector : MonoBehaviour
{
    public Image bodyPartPreview;
    public int spriteIndex;

    [SerializeField] private string bodyPartName;

    private int spriteCount;

    private void Start()
    {
        spriteCount = SpriteStorage.instance.GetSpriteCount(bodyPartName);
    }

    //Go to a specific sprite
    public void SetCharacterBodyPartSprite(int index)
    {
        spriteIndex = index;
        bodyPartPreview.sprite = SpriteStorage.instance.GetSprite(bodyPartName, spriteIndex);
    }

    public void IncreaseSpriteIndex()
    {
        spriteIndex++;

        if (spriteIndex >= spriteCount) //If index reached its maximum limit, go back to zero
            spriteIndex = 0;

        bodyPartPreview.sprite = SpriteStorage.instance.GetSprite(bodyPartName, spriteIndex);
    }

    public void DecreaseSpriteIndex()
    {
        spriteIndex--;

        if (spriteIndex < 0) //If it's the first sprite, then go to the last one
            spriteIndex = spriteCount - 1;

        bodyPartPreview.sprite = SpriteStorage.instance.GetSprite(bodyPartName, spriteIndex);
    }


}
