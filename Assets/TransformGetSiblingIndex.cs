using UnityEngine;

public class TransformGetSiblingIndex : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    public int IndexNumber = 6;

    public void ChangeSiblingIndex(int number)
    {
        _panels[number].transform.SetSiblingIndex(6);
    }
}