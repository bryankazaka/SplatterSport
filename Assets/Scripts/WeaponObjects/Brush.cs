using Unity.VisualScripting;

public class Brush : Weapon {
    public Brush(int colour) : base(colour) {
        this.damage = 10;
        this.range = 1.00f;
        this.attackSpeed = 1.15f;
    }
}