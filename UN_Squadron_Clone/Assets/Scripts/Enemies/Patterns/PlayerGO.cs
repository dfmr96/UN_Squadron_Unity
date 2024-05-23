using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGameObject", menuName = "Player", order = 0)]
public class PlayerGO : ScriptableObject
{
    [field:SerializeField] public GameObject Player { get; private set; }
}
