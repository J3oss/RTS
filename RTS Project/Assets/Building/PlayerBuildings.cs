using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBuildings
{
    static GameObject selectedBuilding = null;
    public static void Deselect()
    {
        if (selectedBuilding !=null)
        {
            Building selectable;
            selectable = selectedBuilding.GetComponentInParent<Building>();
            selectable.Deselect();

            selectedBuilding = null;
        }
    }

    public static void Select(GameObject obj)
    {
        Deselect();

        Building selectable = obj.GetComponentInParent<Building>();
        selectable.Select();

        selectedBuilding = obj;
    }
}
