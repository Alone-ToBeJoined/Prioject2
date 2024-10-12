using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackingObject : MonoBehaviour
{
    [SerializeField] Transform playerVisual;
    [SerializeField] private GameObject BrickPrefabs;
    [SerializeField] private Transform brickListParent;

    private List<GameObject> bricks = new List<GameObject>();

    public void AddBrick(GameObject brick)
    {
        bricks.Add(brick);
        brick.transform.localPosition = Vector3.up * 0.2f * (bricks.Count - 1);
    }

    public void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            playerVisual.transform.position -= Vector3.up * 2f;
            GameObject removeBrick = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);  
            Destroy(removeBrick);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoundBricks"))
        {
            Destroy(other.gameObject);
            GameObject newBricks = Instantiate(BrickPrefabs, brickListParent);
            AddBrick(newBricks);
        }
    }


}
