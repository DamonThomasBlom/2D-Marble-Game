using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoGenerator : MonoBehaviour
{
    [Header("Peg Settings")]
    public GameObject pegPrefab;
    public int rows = 6;
    public float pegSpacing = 1.0f;

    [Header("Offset Settings")]
    public float rowVerticalSpacing = 1.0f;

    [Button]
    void GeneratePlinkoBoard()
    {
        ClearChildren();
        for (int row = 0; row < rows; row++)
        {
            int pegsInRow = row + 1;
            float startX = transform.position.x - (pegSpacing * (pegsInRow - 1) / 2f);
            float y = transform.position.y - (row * rowVerticalSpacing);

            for (int i = 0; i < pegsInRow; i++)
            {
                float x = startX + (i * pegSpacing);
                Vector3 pegPos = new Vector3(x, y, 0f);
                Instantiate(pegPrefab, pegPos, Quaternion.identity, this.transform);
            }
        }
    }

    void ClearChildren()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
