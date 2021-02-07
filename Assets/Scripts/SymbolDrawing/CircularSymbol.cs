using UnityEngine;

namespace SymbolDrawing
{
    public class CircularSymbol : Symbol
    {
        private int pointsHit = 0;
        private SymbolPoint firstPointHit;
        
        public static event ScoreMustBeAdded OnCircularSymbolDestroy;

        public override void PointedAt(Vector3 endPosition)
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
                    if (!symbolPoint.WasHit)
                    {
                        pointsHit += 1;
                        symbolPoint.WasHit = true;
                    }

                    if (pointsHit == 1)
                    {
                        firstPointHit = symbolPoint;
                    }
                }
            }

            if (pointsHit == 2)
            {
                firstPointHit.WasHit = false;
            }

            if (AllSymbolPointsHit())
            {
                if (transform.root.GetComponent<FinalBoss>().isFinalBoss)
                {
                    FinalBoss.symbolWasDestroyed = true;
                }
                destroyed = true;
                
                OnCircularSymbolDestroy?.Invoke(destroyBonus);
                
                
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

        protected override void ResetSymbolPoints()
        {
            base.ResetSymbolPoints();
            pointsHit = 0;
        }
    }
}