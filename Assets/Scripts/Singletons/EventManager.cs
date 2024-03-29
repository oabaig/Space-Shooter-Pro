﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton

    public static EventManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of " + gameObject.name + " found!");
            return;
        }
        instance = this;
    }

    #endregion

    List<Enemy> _pointsAddedInvokers = new List<Enemy>();
    List<UnityAction<int>> _pointsAddedListeners = new List<UnityAction<int>>();

    List<Player> _updateLivesInvokers = new List<Player>();
    List<UnityAction<int, int>> _updateLivesListeners = new List<UnityAction<int, int>>();

    #region Points Added Event
    public void AddPointsAddedInvoker(Enemy invoker)
    {
        _pointsAddedInvokers.Add(invoker);
        foreach (UnityAction<int> listener in _pointsAddedListeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void AddPointsAddedListener(UnityAction<int> listener)
    {
        _pointsAddedListeners.Add(listener);
        foreach (Enemy invoker in _pointsAddedInvokers)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedInvoker(Enemy invoker)
    {
        _pointsAddedInvokers.Remove(invoker);
        foreach (UnityAction<int> listener in _pointsAddedListeners)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedListener(UnityAction<int> listener)
    {
        _pointsAddedListeners.Remove(listener);
        foreach (Enemy invoker in _pointsAddedInvokers)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }
    #endregion

    #region Update Lives Event
    public void AddUpdateLivesInvoker(Player invoker)
    {
        _updateLivesInvokers.Add(invoker);
        Debug.Log("Add Update Lives Invoker Method Called");
        foreach (UnityAction<int, int> listener in _updateLivesListeners)
        {
            invoker.AddUpdateLivesEventListener(listener);
        }
    }

    public void AddUpdateLivesListener(UnityAction<int, int> listener)
    {
        _updateLivesListeners.Add(listener);
        foreach (Player invoker in _updateLivesInvokers)
        {
            invoker.AddUpdateLivesEventListener(listener);
        }
    }

    public void RemoveUpdateLivesInvoker(Player invoker)
    {
        _updateLivesInvokers.Remove(invoker);
        foreach (UnityAction<int, int> listener in _updateLivesListeners)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }

    public void RemoveUpdateLivesListener(UnityAction<int, int> listener)
    {
        _updateLivesListeners.Remove(listener);
        foreach (Player invoker in _updateLivesInvokers)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }
    #endregion
}
