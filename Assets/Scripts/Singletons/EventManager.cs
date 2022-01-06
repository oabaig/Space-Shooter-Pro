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

    List<Enemy> _pointsAddedInvokers;
    List<UnityAction<int>> _pointsAddedListeners;

    List<Player> _updateLivesInvokers;
    List<UnityAction<int>> _updateLivesListeners;

    List<Player> _gameOverInvokers;
    List<UnityAction<bool>> _gameOverListeners;

    private void Start()
    {
        _pointsAddedInvokers = new List<Enemy>();
        _pointsAddedListeners = new List<UnityAction<int>>();

        _updateLivesInvokers = new List<Player>();
        _updateLivesListeners = new List<UnityAction<int>>();

        _gameOverInvokers = new List<Player>();
        _gameOverListeners = new List<UnityAction<bool>>();
    }

    #region Points Added Event
    public void AddPointsAddedInvoker(Enemy invoker)
    {
        _pointsAddedInvokers.Add(invoker);
        foreach(UnityAction<int> listener in _pointsAddedListeners)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void AddPointsAddedListener(UnityAction<int> listener)
    {
        _pointsAddedListeners.Add(listener);
        foreach(Enemy invoker in _pointsAddedInvokers)
        {
            invoker.AddPointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedInvoker(Enemy invoker)
    {
        _pointsAddedInvokers.Remove(invoker);
        foreach(UnityAction<int> listener in _pointsAddedListeners)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }

    public void RemovePointsAddedListener(UnityAction<int> listener)
    {
        _pointsAddedListeners.Remove(listener);
        foreach(Enemy invoker in _pointsAddedInvokers)
        {
            invoker.RemovePointsAddedEventListener(listener);
        }
    }
    #endregion

    #region Update Lives Event
    public void AddUpdateLivesInvoker(Player invoker)
    {
        _updateLivesInvokers.Add(invoker);
        foreach (UnityAction<int> listener in _updateLivesListeners)
        {
            invoker.AddUpdateLivesEventListener(listener);
        }
    }

    public void AddUpdateLivesListener(UnityAction<int> listener)
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
        foreach (UnityAction<int> listener in _updateLivesListeners)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }

    public void RemoveUpdateLivesListener(UnityAction<int> listener)
    {
        _updateLivesListeners.Remove(listener);
        foreach (Player invoker in _updateLivesInvokers)
        {
            invoker.RemoveUpdateLivesEventListener(listener);
        }
    }
    #endregion

    public void AddGameOverEventInvoker(Player invoker)
    {
        _gameOverInvokers.Add(invoker);
        foreach(UnityAction<bool> listener in _gameOverListeners)
        {
            invoker.AddGameOverEventListener(listener);
        }
    }

    public void AddGameOverEventListener(UnityAction<bool> listener)
    {
        _gameOverListeners.Add(listener);
        foreach(Player invoker in _gameOverInvokers)
        {
            invoker.AddGameOverEventListener(listener);
        }
    }

    public void RemoveGameOverEventInvoker(Player invoker)
    {
        _gameOverInvokers.Remove(invoker);
        foreach(UnityAction<bool> listener in _gameOverListeners)
        {
            invoker.RemoveGameOverEventListener(listener);
        }
    }

    public void RemoveGameOverEventListener(UnityAction<bool> listener)
    {
        _gameOverListeners.Remove(listener);
        foreach(Player invoker in _gameOverInvokers)
        {
            invoker.RemoveGameOverEventListener(listener);
        }
    }
}
