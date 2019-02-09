﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Tag
{
    Player,
    Enemy
}

public class TagHandler : MonoBehaviour
{

    public List<Tag> tagList = new List<Tag>();

    public void Add(Tag tag)
    {
        tagList.Add(tag);
    }

    public void Remove(Tag tag)
    {
        tagList.Remove(tag);
    }


    public bool IsInEnum(Tag tag)
    {
        return tagList.Contains(tag);
    }
}
