Shader "HyperCard/Card (Face)"{
    Properties{
        [HideInInspector] _CardFrame("_CardFrame", 2D) = "black" {}
        [HideInInspector] _CardFrameColor("_CardFrameColor", Color) = (1,1,1,1)
        [HideInInspector] _CardMask("_CardMask", 2D) = "black" {}
        [HideInInspector] _CardPicture("_CardPicture", 2D) = "black" {}
        [HideInInspector] _CardAlpha("_CardAlpha", 2D) = "black" {}
    
        [HideInInspector] _GemColor("_GemColor", Color) = (0,0,0,0)
        [HideInInspector] _Dist0_Enabled("_Dist0_Enabled", Int) = 0
        [HideInInspector] _CardDist0Mask("Card Dist 0 Mask", 2D) = "black" {}
        [HideInInspector] _Dist0Freq_Red("Dist Freq", Float) = 100
        [HideInInspector] _Dist0Amp_Red("Amp. Mult", Float) = 1
        [HideInInspector] _Dist0Speed_Red("Dist Speed", Float) = 1
        [HideInInspector] _Dist0Pos_Red("Dist strength (x,y)", Vector) = (0,0,0,0)
        [HideInInspector] _Dist0Freq_Green("Dist Freq", Float) = 100
        [HideInInspector] _Dist0Amp_Green("Amp. Mult", Float) = 1
        [HideInInspector] _Dist0Speed_Green("Dist Speed", Float) = 1
        [HideInInspector] _Dist0Pos_Green("Dist strength (x,y)", Vector) = (0,0,0,0)
        [HideInInspector] _Dist0Freq_Blue("Dist Freq", Float) = 100
        [HideInInspector] _Dist0Amp_Blue("Amp. Mult", Float) = 1
        [HideInInspector] _Dist0Speed_Blue("Dist Speed", Float) = 1
        [HideInInspector] _Dist0Pos_Blue("Dist strength (x,y)", Vector) = (0,0,0,0)
        [HideInInspector] _CardDist1Mask("Card Dist Mask", 2D) = "black" {}
        [HideInInspector] _CardDist1Tex("Card Dist Tex", 2D) = "black" {}
        [HideInInspector] _Dist1SpeedX("Dist Speed X", Float) = 0
        [HideInInspector] _Dist1SpeedY("Dist Speed Y", Float) = 0
        [HideInInspector] _Dist1AlphaMult("Dist Alpha", Float) = 0
        [HideInInspector] _Dist1Color("Dist Color", Color) = (0,0,0,0)
        [HideInInspector] _Dist1Freq("Dist Freq", Float) = 100
        [HideInInspector] _Dist1Amp("Dist Amplitude", Float) = 0.003
        [HideInInspector] _Dist1Speed("Dist Speed", Float) = 1
        [HideInInspector] _Dist1Pos("Dist Vector Pos (x,y)", Vector) = (0,0,0,0)
        
        [HideInInspector] _BlackAndWhite("_BlackAndWhite", Int) = 0
    
        [HideInInspector] _SpriteSheet_Enabled("_SpriteSheet_Enabled", Int) = 0
        [HideInInspector] _SpriteSheetTex("_SpriteSheetTex", 2D) = "black" {}
        [HideInInspector] _SpriteSheetOffset("_SpriteSheetOffset", Vector) = (1,1,0,0)
        [HideInInspector] _SpriteSheetScale("_SpriteSheetScale", Vector) = (1,1,0,0)
        [HideInInspector] _SpriteSheetIndex("_SpriteSheetIndex", Int) = 0
        [HideInInspector] _SpriteSheetCols("_SpriteSheetCols", Float) = 1
        [HideInInspector] _SpriteSheetRows("_SpriteSheetRows", Float) = 1
    
        [HideInInspector] _MixTexture_Enabled("_MixTexture_Enabled", Int) = 0
        [HideInInspector] _OverlayTexture("_OverlayTexture", 2D) = "black" {}
        [HideInInspector] _Stencil("Stencil ID", Int) = 0
        [HideInInspector] _OutlineTrimOffset("_OutlineTrimOffset", Float) = 1
        [HideInInspector] _CardOpacity("_CardOpacity", Float) = 1
        [HideInInspector] _CanvasOffsetX("_CanvasOffsetX", Float) = 1
        [HideInInspector] _CanvasOffsetY("_CanvasOffsetY", Float) = 1
        [HideInInspector] _Seed("_Seed", Float) = 1
    }
}