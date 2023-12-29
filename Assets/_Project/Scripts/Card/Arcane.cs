using UnityEngine;

public struct Arcane{

    public enum ArcaneType{
        Arcane, Trap
    }
    public readonly ArcaneType Type;
    public readonly Sprite Illustration, Frame;
    public readonly string Name, Effect;

    public Arcane(ArcaneType type, Sprite illustration, Sprite frame, string name, string effect){
        Type = type;
        Frame = frame;
        Illustration = illustration;
        Name = name;
        Effect = effect;
    }
}
