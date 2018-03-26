using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Keep this script. Use this to learn how to use the state machine Shahryar has created.

[RequireComponent(typeof(NavMeshAgent))]

public class CubeBoyScript : MonoBehaviour {

    private StateMachine stateMachine = new StateMachine();
    [SerializeField]
    // Serialized field can be private and at the same time be able to set in Unity
    private LayerMask foodItemLayer;
    [SerializeField]
    private float viewRange;
    [SerializeField]
    private string foodItemsTag;
    private NavMeshAgent navMeshAgent;
    
    private void Start()
    {
        // this plugs in the state or cartridge into the state machine
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.stateMachine.ChangeState(new SearchFor(this.foodItemLayer, this.gameObject, this.viewRange, this.foodItemsTag , FoodFound));
    }

    private void Update()
    {
        // this makes the state machine use the state
        this.stateMachine.ExecuteStateUpdate();
    }
    
    // this is the receiving end of the information we've asked the cartridge to find
    public void FoodFound (SearchResults searchResults)
    {
        var foundFoodItems = searchResults.AllHitObjectsWithRequiredTag;
        print(foundFoodItems.Count);
        // Decide what to eath
        // Trigger the eating cartridge 
    }
    

}
