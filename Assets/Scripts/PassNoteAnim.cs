using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassNoteAnim : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject bethPaper;
    public GameObject alliePaper;
    
    private void Start() {
        PassNote(GameManager.Instance.currentNoteData.author);
    }

    void PassNote(Enums.Character from) {
        if (from == Enums.Character.Beth)
        {
            transform.position = new Vector3(-950f, -920f, 0f);
            leftHand.transform.parent = transform;
            bethPaper.transform.parent = transform;
            StartCoroutine(LerpArm(30f, 2f));
        }
        else
        {
            transform.position = new Vector3(950f, -920f, 0f);
            rightHand.transform.parent = transform;
            alliePaper.transform.parent = transform;
            StartCoroutine(LerpArm(-30f, 2f));
        }
        
    }

    
    public IEnumerator LerpArm(float newZRot, float totalTime, System.Action callback = null) {
        float elapsedTime = 0;
        float startZRot = 0;
        while (elapsedTime < totalTime)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, startZRot + (elapsedTime / totalTime) * (newZRot - startZRot)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (elapsedTime >= totalTime)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newZRot));
            if (callback != null) callback();
        }
    }
    

}
