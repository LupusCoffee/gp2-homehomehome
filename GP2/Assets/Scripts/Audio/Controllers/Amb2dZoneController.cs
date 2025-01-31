//Created by Mohammed (the sex god)

using UnityEngine;

public class Amb2dZoneController : AudioZoneController
{
    [SerializeField] private float radius;
    GameObject player;
    bool updatingRadiusVolume;

    protected override void EnterZone(GameObject _player)
    {
        updatingRadiusVolume = true;
        player = _player;
        base.EnterZone(_player);
    }
    protected override void ExitZone(GameObject _player)
    {
        updatingRadiusVolume = false;
        player = _player;
        base.ExitZone(_player);
    }
    private void Update()
    {
        if(updatingRadiusVolume) //update volume along the radius from edge of collider
        {
            //to-do: update radius from edge of collider
        }
    }
}
