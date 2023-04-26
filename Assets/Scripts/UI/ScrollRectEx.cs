using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ScrollRectEx : ScrollRect
{
    private bool routeToParent = false;
    private ScrollRect mainScrollRect = null;

    /// <summary>
    /// Do action for all parents
    /// </summary>
    private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        if (mainScrollRect == null)
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                foreach (var component in parent.GetComponents<Component>())
                {
                    if (component is T)
                    {
                        mainScrollRect = (ScrollRect)component;
                        action((T)(IEventSystemHandler)component);
                        break;
                    }
                }

                if (mainScrollRect == null)
                {
                    parent = parent.parent;
                }
                else
                {
                    parent = null;
                }
            }
        }
        else
        {
            action((T)(IEventSystemHandler)mainScrollRect);
        }
    }

    /// <summary>
    /// Always route initialize potential drag event to parents
    /// </summary>
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
        base.OnInitializePotentialDrag(eventData);
    }

    /// <summary>
    /// Drag event
    /// </summary>
    public override void OnDrag(PointerEventData eventData)
    {
        if (routeToParent)
            DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        else
            base.OnDrag(eventData);
    }

    /// <summary>
    /// Begin drag event
    /// </summary>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
            routeToParent = true;
        else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
            routeToParent = true;
        else
            routeToParent = false;

        if (routeToParent)
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
        else
            base.OnBeginDrag(eventData);
    }

    /// <summary>
    /// End drag event
    /// </summary>
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (routeToParent)
            DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
        else
            base.OnEndDrag(eventData);
        routeToParent = false;
    }
}
