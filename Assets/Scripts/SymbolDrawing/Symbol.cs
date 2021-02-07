using System.Collections.Generic;
using UnityEngine;

namespace SymbolDrawing
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] protected GameObject particleSystem = null;
        [SerializeField] protected float maxDistance = 0.1f;
        protected SaturationHandler saturationHandler = null;
        
        protected Dictionary<Vector3, GameObject> particleList = new Dictionary<Vector3, GameObject>();
        protected List<SymbolPoint> symbolPoints = new List<SymbolPoint>();
        protected EnemySpawner enemySpawner;

        protected bool activeSymbol;
        public bool destroyed;

        protected internal bool allSymbolPointsHit = false;

        protected StoryController storyController;

        protected int destroyBonus = 50;
        
        public delegate void ScoreMustBeAdded(int scored);
        public static event ScoreMustBeAdded OnSymbolDestroy;

        public bool ActiveSymbol
        {
            get => activeSymbol;
            set => activeSymbol = value;
        }


        private void Start()
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                symbolPoints.Add(transform.GetChild(0).GetChild(i).gameObject.GetComponent<SymbolPoint>());
            }

            //enemySpawner = GameObject.Find("VariousEnemySpawner").GetComponent<EnemySpawner>();
            saturationHandler = GameObject.Find("Environment").GetComponent<SaturationHandler>();
            storyController = GameObject.Find("StoryController").GetComponent<StoryController>();

        }

        private void ResetParticles()
        {
            foreach (KeyValuePair<Vector3, GameObject> keyValuePair in particleList)
            {
                Destroy(keyValuePair.Value.gameObject);
            }

            particleList = new Dictionary<Vector3, GameObject>();
        }

        protected virtual void ResetSymbolPoints()
        {
            foreach (SymbolPoint symbolPoint in symbolPoints)
            {
                symbolPoint.WasHit = false;
            }
        }

        protected bool AllSymbolPointsHit()
        {
            if (allSymbolPointsHit)
            {
                return true;
            }
            foreach (SymbolPoint point in symbolPoints)
            {
                if (!point.WasHit)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool CloseToOtherParticles(Vector3 position)
        {
            foreach (KeyValuePair<Vector3, GameObject> keyValuePair in particleList)
            {
                if (Vector3.Distance(position, keyValuePair.Key) < 0.075f)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void PointedAt(Vector3 endPosition)
        {
            activeSymbol = true;
            if (!CloseToOtherParticles(endPosition))
            {
                GameObject particles = Instantiate(this.particleSystem, this.transform, true) as GameObject;
                particles.transform.position = endPosition;
                particles.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                particleList.Add(endPosition, particles);
            }

            foreach (SymbolPoint symbolPoint in symbolPoints)
            {
                if (Vector3.Distance(symbolPoint.transform.position, endPosition) <= maxDistance)
                {
                    symbolPoint.WasHit = true;
                }
            }

            if (AllSymbolPointsHit())
            {
                if (transform.root.GetComponent<FinalBoss>().isFinalBoss)
                {
                    FinalBoss.symbolWasDestroyed = true;
                }
                destroyed = true;
                OnSymbolDestroy?.Invoke(destroyBonus);
                
                //Maybe add a Death Animation
                // Enemy Dissolves and the Enemy gets Destroyed
                gameObject.transform.parent.gameObject.BroadcastMessage("DissolveEnemy");
                storyController.IncrementCounter();
            
                //enemySpawner.SpawnNextEnemy();    // spawn new enemy with a chosen delay
            
                saturationHandler.IncreaseSaturation();
                Destroy(gameObject.transform.parent.gameObject, 2.8f);
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!activeSymbol)
            {
                ResetSymbolPoints();
                ResetParticles();
            }
        }
    }
}