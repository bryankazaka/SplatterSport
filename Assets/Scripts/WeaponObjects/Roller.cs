using Unity.VisualScripting;

public class Roller : Weapon {
    public Roller(int colour) : base(colour) {
        this.damage = 15;
        this.range = 1.27f;
        this.attackSpeed = 1f;
    }
}