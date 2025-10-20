using UnityEngine;
using UnityEngine.Serialization;

public class ArcherTower : BaseBuilding
{
  [SerializeField] private ArcherTowerAttack archerTowerAttack;

  private void Awake()
  {
    archerTowerAttack = GetComponentInChildren<ArcherTowerAttack>();
  }

  public override void RestoreHealth()
  {
    base.RestoreHealth();
    archerTowerAttack.CanShoot(true);
  }

  public override void DestroyBuilding()
  {
    base.DestroyBuilding();
    archerTowerAttack.CanShoot(false);
  }
}
