using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace PoonGaloreECS
{
    public class FixedTimestepUpdater : MonoBehaviour
    {
        private PlayerMovementSystem _playerMovementSystem;
        public Slider fixedTimestepSlider;
        public Text sliderText;
        
        private void FixedUpdate()
        {
            if (_playerMovementSystem == null)
                _playerMovementSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<PlayerMovementSystem>();

            sliderText.text = "FixedTimeStepSlider: " + fixedTimestepSlider.value.ToString("F2");
            
            Time.fixedDeltaTime = fixedTimestepSlider.value;
            _playerMovementSystem.Update();
        }
    }
}