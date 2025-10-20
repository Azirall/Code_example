using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building System", fileName = "New building")]
public class BuildData : ScriptableObject
{
    public List<ItemStack> Requirements => requirements;
    public BuildingType GetBuildingType => type;
    public Sprite GetBuiltSprite => builtSprite;
    public Sprite GetUnbuiltSprite => unbuiltSprite;
    public string GetDescription => description;
    public  int Health => health;

    [SerializeField] private List<ItemStack> requirements;

    [SerializeField] private BuildingType type;
    [SerializeField] private int health;
    [SerializeField] private Sprite builtSprite;
    [SerializeField] private Sprite unbuiltSprite;
    [SerializeField] private string description;
    

}
