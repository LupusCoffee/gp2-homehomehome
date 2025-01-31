using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static MultiTag;

public class Puzzle_PressurePlate : PuzzleEvent
{
    [SerializeField] GameObject visuals;
    Vector3 startPos;
    float sinkDepth = 0.199f;

    List<Collider> objectsInRange = new List<Collider>();

    bool hasObjectsInRange;

    private void Awake()
    {
        startPos = transform.position;

        // might be expensive idk but we need a fix for if colliders are disabled WHILE pressing on smth...
        // ontriggerexit doesnt trigger if collider is disabled
        InvokeRepeating("ClearDisabledColliders", 0, 1F);
    }

    void ClearDisabledColliders()
    {
        foreach (Collider obj in objectsInRange)
        {
            if (obj.enabled == false)
            {
                objectsInRange.Remove(obj);
            }
            else if(Vector3.Distance(obj.transform.position, transform.position) > 1) {
                objectsInRange.Remove(obj);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || HasTag(other.gameObject, MultiTags.Heavy))
        {
            if (!objectsInRange.Contains(other))
            {
                objectsInRange.Add(other);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pressure Plate Triggered");

        if (other.CompareTag("Player") || HasTag(other.gameObject, MultiTags.Heavy))
        {
            if (!objectsInRange.Contains(other))
                objectsInRange.Add(other);

            LeanTween.moveLocalY(visuals, -sinkDepth, 0.25f).setOnComplete(() =>
            {
                if (objectsInRange.Count >= 1)
                    TriggerOnButtonPressed();
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || HasTag(other.gameObject, MultiTags.Heavy))
        {
            if (objectsInRange.Contains(other))
                objectsInRange.Remove(other);

            LeanTween.moveLocalY(visuals, 0, 0.25f).setOnComplete(() =>
            {
                objectsInRange.Remove(other);

                if (objectsInRange.Count == 0)
                    TriggerOnButtonReleased();
            });
        }
    }
}
