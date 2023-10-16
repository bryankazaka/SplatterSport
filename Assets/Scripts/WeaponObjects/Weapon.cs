public class Weapon{
    const int GREEN = 1, YELLOW = 2, RED = 3, BLUE = 4;
    protected int colour;
    protected float damage, range, attackSpeed;

    public Weapon(int colour){
        this.colour = colour;
    }

    // accessors 
    public int getColour(){
        return colour;
    }
    
    public float getDamage(){
        return damage;
    }

    public float getRange(){
        return range;
    }

    public float getAttackSpeed(){
        return attackSpeed;
    }

    // mutators
    public void setColour(int colour){
        this.colour = colour;
    }

    public void setDamage(float damage){
        this.damage = damage;
    }

    public void setRange(float range){
        this.range = range;
    }

    public void setAttackSpeed(float attackSpeed){
        this.attackSpeed = attackSpeed;
    }



}