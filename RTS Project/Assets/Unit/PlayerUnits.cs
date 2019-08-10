using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUnits
{
    public static List<GameObject> playerUnits = new List<GameObject>();
    public static List<GameObject> selectedUnits = new List<GameObject>();

    public static void DeselectAll()
    {
        Unit selectable;

        foreach (GameObject obj in selectedUnits)
        {
            selectable = obj.GetComponentInParent<Unit>();
            selectable.Deselect();
        }

        selectedUnits.Clear();
    }

    public static void SingleSelect(GameObject obj)
    {
        DeselectAll();

        Unit selectable;
        selectable = obj.GetComponentInParent<Unit>();
        selectable.Select();

        selectedUnits.Add(obj);
    }

    public static void BoxSelect(Vector3 startingMousePosition)
    {
        DeselectAll();

        Unit selectable;
        foreach (var obj in playerUnits)
        {
            if (IsWithinSelectionBounds(startingMousePosition, obj))
            {
                selectable = obj.GetComponentInParent<Unit>();
                selectable.Select();
            }
        }
    }

    public static void MoveSelected(Vector3 mouse)
    {
        Unit selectable;
        /*foreach (var obj in selectedUnits)
        {
            selectable = obj.GetComponentInParent<Unit>();
            selectable.Move(mouse);
        }
        */

        Vector3[] destinations = new Vector3[selectedUnits.Count];

        int formationWidth = Mathf.CeilToInt(Mathf.Sqrt(selectedUnits.Count));
        int formationHeight = Mathf.RoundToInt(Mathf.Sqrt(selectedUnits.Count));
        Debug.Log(formationWidth + ", " + formationHeight);

        Vector2[,] offsets = new Vector2[formationWidth, formationHeight];
        int i_w = 0;
        int i_h = 0;

        foreach (GameObject unit in selectedUnits)
        {
            float widthOffset = 0f;
            float heightOffset = 0f;

            int leftIndex = i_w - 1 + i_h * formationWidth;
            int upIndex = i_w + (i_h - 1) * formationWidth;

            if (i_w > 0)
                widthOffset =
                    offsets[i_w - 1, i_h].x
                    + (selectedUnits[leftIndex].transform.localScale.x) / 2f
                    + unit.transform.localScale.x / 2f
                    + 1;

            if (i_h > 0)
                heightOffset =
                    offsets[i_w, i_h - 1].y
                    - (selectedUnits[leftIndex].transform.localScale.z / 2f)
                    - unit.transform.localScale.z / 2f
                    - 1;

            offsets[i_w, i_h] = new Vector2(
                widthOffset,
                heightOffset
            );

            destinations[i_w + i_h * formationWidth] = mouse + new Vector3(widthOffset, 0, heightOffset);

            if (++i_w == formationWidth)
            {
                i_w = 0;
                i_h++;
            }
            if (i_w + i_h * formationWidth > selectedUnits.Count)
                break;
        }

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            var obj = selectedUnits[i];
            selectable = obj.GetComponentInParent<Unit>();
            selectable.Move(destinations[i],true);
        }
    }

    public static bool IsWithinSelectionBounds(Vector3 startingMousePosition, GameObject gameObject)
    {
        var camera = Camera.main;
        var viewportBounds =
            Utils.GetViewportBounds(camera, startingMousePosition, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }
}
