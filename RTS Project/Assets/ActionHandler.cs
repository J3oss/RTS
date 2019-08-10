using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    const int enemyLayer = 8;
    const int unitLayer = 9;
    const int buildingLayer = 10;
    const int movableLayer = 11;

    public static void LeftClick(Vector3 startingMousePosition,Vector3 endingMousePosition,RaycastHit hit)
    {
        //click
        if (startingMousePosition == endingMousePosition)
        {
            switch (hit.transform.gameObject.layer)
            {
                case unitLayer:
                    PlayerUnits.DeselectAll();
                    PlayerUnits.SingleSelect(hit.collider.gameObject);
                    break;

                case movableLayer:
                    PlayerUnits.MoveSelected(hit.point);
                    break;

                case buildingLayer:
                    PlayerBuildings.Select(hit.collider.gameObject);
                    break;

                default:
                    break;
            }
        }
        //drag
        else
        {
            PlayerUnits.BoxSelect(startingMousePosition);
        }
    }

    public static void RightClick()
    {
        PlayerUnits.DeselectAll();
        PlayerBuildings.Deselect();
    }
}
