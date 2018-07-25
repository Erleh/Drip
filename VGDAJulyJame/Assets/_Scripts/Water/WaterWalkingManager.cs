using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterWalkingManager : MonoBehaviour
{
    Queue<OnWaterRequest> waterQueue = new Queue<OnWaterRequest>();
    Queue<OnWaterRequest> offQueue = new Queue<OnWaterRequest>();

    static WaterWalkingManager instance;

    OnWaterRequest currRequest;

    [SerializeField]
    WaterWalking waterWalking;
    [SerializeField]
    ParticleManager particleReference;

    //bool processingRequest = false;

    #region RequestQueueSystem

    private void Awake()
    {
        instance = this;
    }

    // create request for response to walking on water
    public static void CreateWaterResponseRequest
        (GameObject self, Action<bool, bool, bool, Collider2D> reply, bool onWater)
    {
        OnWaterRequest newRequest = new OnWaterRequest(reply, self);

        if (onWater)
        {
            instance.waterQueue.Enqueue(newRequest);
        }
        else if (!onWater)
        {
            instance.offQueue.Enqueue(newRequest);
        }

        instance.TryRequest(onWater);
    }

    // attempt to perform check on request before successful response
    void TryRequest(bool onWater)
    {
        if (onWater)
        {
            currRequest = waterQueue.Dequeue();
        }
        else if(!onWater)
        {
            currRequest = offQueue.Dequeue();
        }

        waterWalking.StartWaterAffect(currRequest.self, onWater);
    }

    // send results to apply appropriate response
    public void FinishedProcess(bool enemy, bool player, bool onWater, Collider2D col)
    {
        currRequest.response(enemy, player, onWater, col);
    }

    // make request to have response from walking on water
    struct OnWaterRequest
    {
        public Action<bool, bool, bool, Collider2D> response;
        public GameObject self;

        public OnWaterRequest(Action<bool, bool, bool, Collider2D> _response, GameObject _self)
        {
            response = _response;
            self = _self;
        }
    }
    #endregion

    #region ParticleRequest

    public static void ActivatePlayerWaterParticles()
    {
        instance.particleReference.TurnOnPlayerWaterParticles();
    }

    public static void DisablePlayerWaterParticles()
    {
        instance.particleReference.TurnOffPlayerWaterParticles();
    }

    public static void ActivateEnemyWaterParticles()
    {
        instance.particleReference.TurnOnEnemyWaterParticles();
    }

    public static void DisableEnemyWaterParticles()
    {
        instance.particleReference.TurnOffEnemyWaterParticles();
    }

    #endregion
}
