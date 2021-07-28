using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.IO;

public class SpriteStorage : MonoBehaviour
{
    public static SpriteStorage instance = null;

    public List<Sprite> head;
    public List<Sprite> body;
    public List<Sprite> rightArmUpper;
    public List<Sprite> rightArmLower;
    public List<Sprite> leftArmUpper;
    public List<Sprite> leftArmLower;
    public List<Sprite> rightLeg;
    public List<Sprite> leftLeg;
    public List<Sprite> sword;
    public List<Sprite> cape;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public Sprite GetSprite(string bodyPart, int spriteIndex)
    {
        List<Sprite> sprites = GetSpritesFromBodyPart(bodyPart);

        return (spriteIndex <= sprites.Count - 1 ? sprites[spriteIndex] : null);
    }

    public int GetSpriteCount(string bodyPart)
    {
        return GetSpritesFromBodyPart(bodyPart).Count;
    }

    private List<Sprite> GetSpritesFromBodyPart(string bodyPart)
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (FieldInfo field in typeof(SpriteStorage).GetFields())
        {
            if (string.Equals(field.Name, bodyPart, StringComparison.OrdinalIgnoreCase))
            {
                sprites = (List<Sprite>)field.GetValue(this);
                break;
            }
        }

        return sprites;
    }
}
