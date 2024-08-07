using PlatformGame.Character;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FormationManager : MonoBehaviour
{
    public List<Role> Roles = new();
    public List<Transform> Transforms = new();
    public UnityEvent<Role> OnAddRoleEvent;
    public UnityEvent<Role> OnRemoveRoleEvent;

    public void AddRole(Role role)
    {
        if (Transforms.Count <= Roles.Count)
        {
            return;
        }
        var index = Roles.Count;
        role.SetTransform(Transforms[index]);
        Roles.Add(role);
        OnAddRoleEvent.Invoke(role);
    }

    public void RemoveRole(Role role)
    {
        var index = Roles.IndexOf(role);
        if (index == -1)
        {
            return;
        }
        Roles.RemoveAt(index);

        for (var i = index + 1; i < Roles.Count; i++)
        {
            Roles[i].SetTransform(Transforms[i - 1]);
        }
        OnRemoveRoleEvent.Invoke(role);
    }

    public void Comback(Role role)
    {
        Debug.Assert(Roles.Contains(role), $"Not found : {role.ID.Name}");
        var index = Roles.IndexOf(role);
        role.SetTransform(Transforms[index]);
    }

    public void InitFormation()
    {
        Debug.Assert(Roles.Count <= Transforms.Count, $"Roles many than Transfomrs");
        var list = Roles.ToList();
        Roles.Clear();
        foreach (var item in list)
        {
            AddRole(item);
        }
    }

    void Awake()
    {
        InitFormation();
    }

}
