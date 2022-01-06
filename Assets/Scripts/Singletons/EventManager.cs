using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of " + gameObject.name + " found!");
            return;
        }
        instance = this;
    }

    #endregion

    List<Enemy> pointsAddedInvokers;
    List<UnityAction<int>> pointsAddedListeners;

    List<Player> updateLivesInvokers;
    List<UnityAction<int>> updateLivesListeners;

    private void Start()
    {
        pointsAddedInvokers = new List<Enemy>();
        pointsAddedListeners = new List<UnityAction<int>>();

        updateLivesInvokers = new List<Player>();
        updateLivesListeners = new List<UnityAction<int>>();
    }

    #region Points Added Event
    public void AddPointsAddedInvoker(Enemy invoker)
    {
        pointsAddedInvokers.Add(invoker);
        foreach(UnityAction<int> listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedListeners.Add(listener);
        foreach(Enemy invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedInvoker(Enemy invoker)
    {
        pointsAddedInvokers.Remove(invoker);
        foreach(UnityAction<int> listener in pointsAddedListeners)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedListeners.Remove(listener);
        foreach(Enemy invoker in pointsAddedInvokers)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }
    #endregion

    #region Update Lives Event
    public void AddUpdateLivesInvoker(Player invoker)
    {
        updateLivesInvokers.Add(invoker);
        foreach (UnityAction<int> listener in updateLivesListeners)
        {
            invoker.AddUpdateLivesEventListener(listener);
        }
    }

    public void AddUpdateLivesListener(UnityAction<int> listener)
    {
        updateLivesListeners.Add(listener);
        foreach (Player invoker in updateLivesInvokers)
        {
            invoker.AddUpdateLivesEventListener(listener);
        }
    }

    public void RemoveUpdateLivesInvoker(Player invoker)
    {
        updateLivesInvokers.Remove(invoker);
        foreach (UnityAction<int> listener in updateLivesListeners)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }

    public void RemoveUpdateLivesListener(UnityAction<int> listener)
    {
        updateLivesListeners.Remove(listener);
        foreach (Player invoker in updateLivesInvokers)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }
    #endregion
}
