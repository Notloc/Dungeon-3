﻿namespace Adventure.Game.Entities
{
    using UnityEngine;
    using Adventure.Engine.Navigation;

    [RequireComponent(typeof(Rigidbody))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected LocationData locationData;
        public LocationData LocationData
        {
            get
            {
                return locationData;
            }
        }

        private void Reset()
        {
            AutoAcquireLocationData();
        }

        private void AutoAcquireLocationData()
        {
            locationData.navgrid = NavGrid.ClosestNavGrid(this.transform.position);

            if(locationData.navgrid == null)
            {
                return;
            }

            locationData.coordinates = locationData.navgrid.WorldPointToNode(this.transform.position);
        }
    }

    [System.Serializable]
    public struct LocationData
    {
        public NavGrid navgrid;
        public Vector2Int coordinates;
    }
}