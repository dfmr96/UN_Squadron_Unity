using UnityEngine;

[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
public class PlayerGO : ScriptableObject
{
    [field:SerializeField] public GameObject Player { get; private set; }
}
