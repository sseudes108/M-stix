
using System;
using System.Collections;
using UnityEngine;

public class FusionArcane : Fusion{
    public void ArcaneFusion(CardArcane arcane1, CardArcane arcane2){
        StartCoroutine(StartArcaneFusionRoutine(arcane1, arcane2));
    }
    private IEnumerator StartArcaneFusionRoutine(CardArcane arcane1, CardArcane arcane2){
        yield return new WaitForSeconds(1);
        Debug.Log("Implement Arcane Fusion");
        BattleManager.Instance.Fusion.FusionFailed(arcane1, arcane2);
    }
}