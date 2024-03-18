using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{

        [SerializeField] private Slider masterVol, musicVol, soundVol, sensSlider;
        private GameObject soundManagerObj;
        private GameObject player;
        private PlayerControllerPhysics playerController;
        private float currentSens;
        private SoundManager sm;
        private void Start() {
            soundManagerObj = GameObject.Find("SoundManager");
            sm = soundManagerObj.GetComponent<SoundManager>();
            Debug.Log(sm);
            masterVol.value = sm.GetMasterSliderVal();
            musicVol.value = sm.GetMusicSliderVal();
            soundVol.value = sm.GetSoundSliderVal();
            masterVol.onValueChanged.AddListener(sm.ChangeMasterVol);
            musicVol.onValueChanged.AddListener(sm.ChangeMusicVol);
            soundVol.onValueChanged.AddListener(sm.ChangeSoundVol);

            player = GameObject.Find("Player");
            currentSens = sm.getSens();
            if (player) {
                playerController = player.GetComponent<PlayerControllerPhysics>();
                playerController.updateSensitivitySlider(currentSens);
                playerController.updateSensitivity(currentSens);
            } else {
                sensSlider.value = currentSens;
                sensSlider.onValueChanged.AddListener(updateCurrSens);
            }
        }

        public void updateCurrSens(float val) {
            currentSens = val;
            sm.updateSens(val);
        }
}
