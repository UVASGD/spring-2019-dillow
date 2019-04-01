using System.Collections.Generic;
using UnityEngine;
public enum Tag
{
    Player,
    Enemy,
    Damage,
    SuperDamage,
    Water,
    Attacking,
    Dead,
    Marine,
    Dashing,
    Invincible,
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

    public void RemoveAll(Tag tag)
    {
        tagList.RemoveAll(t => t == tag);
    }

    public bool HasTag(Tag tag)
    {
        return tagList.Contains(tag);
    }
}