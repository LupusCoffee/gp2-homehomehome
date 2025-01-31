using UnityEngine;

public class MultiTag : MonoBehaviour
{
    [System.Flags]
    public enum MultiTags
    {
        None = 0,
        Heavy = 1,
        Floating = 2,
        MindControllable = 4,
        Pushable = 8,
        Corruption = 16,
        MovableObject = 32,
        Lightable = 64,
        Nature = 128,
        Artifact = 256,
        Player = 512,
        GrabbableObject = 1024,
    }

    public MultiTags tags;

    //check if the object has a specific tag
    public bool HasTag(MultiTags tag)
    {
        return (tags & tag) == tag;
    }

    public static MultiTag TryGetMultitag(GameObject obj)
    {
        return obj.GetComponent<MultiTag>();
    }

    public static bool HasTag(GameObject obj, MultiTags tag)
    {
        MultiTag mt = TryGetMultitag(obj);
        if (mt == null)
        {
            return false;
        }
        return mt.HasTag(tag);
    }

    public static bool HasTag(GameObject obj, MultiTags[] tags)
    {
        MultiTag mt = TryGetMultitag(obj);
        if (mt == null)
        {
            return false;
        }
        foreach (MultiTags tag in tags)
        {
            if (mt.HasTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
