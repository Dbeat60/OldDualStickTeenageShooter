using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeTransform : MonoBehaviour {

    public class ShakeEvent
    {
        float duration;
        float timeRemaining;

        CameraShakeData data;
        //get style of shake(pos/rot)
        public CameraShakeData.Target target
        {
            get
            {
                return data.target;
            }
        }
        Vector3 noiseOffset;

        public Vector3 noise;


        public ShakeEvent(CameraShakeData data)
        {
            this.data = data;
            duration = data.duration;
            timeRemaining = duration;
            float rand = 32f;
            noiseOffset.x = Random.Range(0.0f, rand);
            noiseOffset.y = Random.Range(0.0f, rand);
            noiseOffset.z = Random.Range(0.0f, rand);

        }
       public void Update()
        {

            float deltaTime = Time.deltaTime;

            timeRemaining -= deltaTime;

            float noideOfssetDelta = deltaTime*data.frequency;

            noiseOffset.x += noideOfssetDelta;
            noiseOffset.y += noideOfssetDelta;
            noiseOffset.z += noideOfssetDelta;

            noise.x = Mathf.PerlinNoise(noiseOffset.x,0.0f);
            noise.y = Mathf.PerlinNoise(noiseOffset.y, 1.0f);
            noise.z = Mathf.PerlinNoise(noiseOffset.z, 2.0f);

            noiseOffset -= Vector3.one * 0.5f;
            noiseOffset *= data.amplitude;

            float agePercent = 1.0f-(timeRemaining / duration);

            noise *= data.blendOverLifetime.Evaluate(agePercent);
        }
        public bool isAlive()
        {
            return timeRemaining > 0.0f;
        }
    }
    List<ShakeEvent> shakeEvents = new List<ShakeEvent>();
    	
	public void AddShakeEvent(CameraShakeData data)
    {
        shakeEvents.Add(new ShakeEvent(data));
    }

    public void addShakeEvent(float amplitude, float frequency, float duration, AnimationCurve blendOverLifeTime, CameraShakeData.Target target)
    {
        CameraShakeData data = ScriptableObject.CreateInstance<CameraShakeData>();
        data.Init(amplitude, frequency, duration, blendOverLifeTime, target);
    }

    private void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;
        Vector3 rotationOffset = Vector3.zero;

        for (int i = shakeEvents.Count-1; i !=-1; i--)
        {
            ShakeEvent se = shakeEvents[i]; se.Update();

            if (se.target == CameraShakeData.Target.Position)
            {
                positionOffset += se.noise;
            }
            else
                rotationOffset += se.noise;

            if(!se.isAlive())
            {
                shakeEvents.RemoveAt(i);
            }
        }
        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotationOffset;


    }
}
