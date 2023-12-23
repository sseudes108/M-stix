using UnityEngine;

[CreateAssetMenu]
public class ArcaneSO : ScriptableObject{
    public enum ArcaneType{
        Magic, Trap,
    }
    public ArcaneType _arcaneType;
    public string _name, _effect;
    public Sprite _front, _ilustration;

    public ArcaneInfo GetInfo(){
        return new ArcaneInfo{
            Name = _name,
            ArcaneType = _arcaneType,
            Effect = _effect,
            Front = _front,
            Ilustration = _ilustration
        };
    }
}
public struct ArcaneInfo{
    public ArcaneSO.ArcaneType ArcaneType;
    public string Name, Effect;
    public Sprite Front, Ilustration;
}
