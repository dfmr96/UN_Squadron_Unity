using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VulkanPOWType
{
    Orange,
    Blue
}
[RequireComponent(typeof(BoxCollider2D))]
public class VulkanPOW : MonoBehaviour
{
    [SerializeField] VulkanPOWType _type;
    [SerializeField] int _vulkanPOWPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (_type)
        {
            case VulkanPOWType.Orange:
                _vulkanPOWPoints= 1;
                break;
            case VulkanPOWType.Blue:
                _vulkanPOWPoints= 3;
                break;
            default:
                break;
        }
    }

    public int IncreaseVulkanPOWPoints()
    {
        return _vulkanPOWPoints;
    }
}
