using Unity.VisualScripting;

public class Pencil : Weapon {
    public Pencil(int colour) : base(colour) {
        this.damage = 5;
        this.range = 0.5f;
        this.attackSpeed = 0.5f;
    }
}