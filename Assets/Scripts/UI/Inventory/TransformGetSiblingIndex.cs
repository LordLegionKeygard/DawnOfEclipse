using UnityEngine;

public class TransformGetSiblingIndex : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
 
    public void ChangeSiblingIndex(int number)
    {
        _panels[number].transform.SetAsLastSibling();
    }
}