﻿namespace Adventure.Engine.User
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Adventure.Engine.Navigation;
    using Adventure.Game;
    using Adventure.Game.Entities;

    public class MouseHandler : MonoBehaviour
    {
        [SerializeField] LayerMask entityLayerMask;
        [SerializeField] LayerMask navGridLayerMask;

        public void HandleClick(iControllable character)
        {
            if (EntityClick(character))
                return;

            NavGridClick(character);
        }

        private bool EntityClick(iControllable character)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 500f, entityLayerMask))
            {
                Entity entity = hit.collider.GetComponentInParent<Entity>();
                if (entity == null)
                    return false;

                character.InteractWith(entity.DefaultInteraction);

            }
            return false;
        }

        private bool NavGridClick(iControllable character)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 500f, navGridLayerMask))
            {
                if (hit.collider.tag == "NavGrid")
                {
                    NavGrid navgrid = hit.collider.GetComponent<NavGrid>();
                    Vector2Int nodePosition = navgrid.WorldPointToNode(hit.point);

                    if (nodePosition != NavGrid.NO_NODE)
                    {
                        character.MoveTo(nodePosition, navgrid);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}