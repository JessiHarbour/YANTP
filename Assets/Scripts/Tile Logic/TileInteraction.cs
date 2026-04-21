using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


namespace Tile_Logic
{
    public class TileInteraction : MonoBehaviour
    {
        public Tilemap breakableTilemap;
        public Tilemap groundTilemap;

        public TileBase playerBlockTile;

        public int maxBlocks = 3;
        private int currentBlocks = 0;

        public GameObject ghostTile;
        private SpriteRenderer ghostRenderer;

        public Transform player;
        public float editRange = 3f;

        public BlockUI blockUI;

        public ParticleSystem particlePrefab;

        public LayerMask spikeLayer;

        // state of game
        public bool gameStarted = false;
        
        public GameObject startButtonUI;
        public GameObject restartButtonUI;

        void Awake()
        {
            Time.timeScale = 0f;
            gameStarted = false;
        }

        void Start()
        {
            ghostRenderer = ghostTile.GetComponent<SpriteRenderer>();

            if (blockUI != null)
                blockUI.UpdateUI(currentBlocks);
            
            if (restartButtonUI != null)
                restartButtonUI.SetActive(false);
        }

        void Update()
        {
            // restart
            if (gameStarted && Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

            if (!gameStarted)
                return;

            UpdateGhost();

            if (Input.GetMouseButtonDown(0))
            {
                HandleClick();
            }
        }

        // start game
        public void StartGame()
        {
            gameStarted = true;
            Time.timeScale = 1f;

            if (startButtonUI != null)
                startButtonUI.SetActive(false);

            if (restartButtonUI != null)
                restartButtonUI.SetActive(true);
        }

        // restart game
        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        bool IsInRange(Vector3 worldPos)
        {
            return Vector2.Distance(player.position, worldPos) <= editRange;
        }

        void UpdateGhost()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            Vector3Int cellPos = breakableTilemap.WorldToCell(worldPos);
            Vector3 cellWorld = breakableTilemap.GetCellCenterWorld(cellPos);

            ghostTile.transform.position = cellWorld;

            bool inRange = IsInRange(cellWorld);

            if (!inRange)
            {
                ghostRenderer.color = new Color(1f, 1f, 1f, 0.05f);
                return;
            }

            if (breakableTilemap.HasTile(cellPos))
            {
                ghostRenderer.color = new Color(1f, 0.3f, 0.3f, 0.4f);
            }
            else if (!groundTilemap.HasTile(cellPos) && currentBlocks < maxBlocks)
            {
                ghostRenderer.color = new Color(1f, 1f, 1f, 0.4f);
            }
            else
            {
                ghostRenderer.color = new Color(1f, 1f, 1f, 0.1f);
            }
        }

        void HandleClick()
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            if (!IsInRange(worldPos))
                return;

            Vector3Int cellPos = breakableTilemap.WorldToCell(worldPos);

            // breack tile
            if (breakableTilemap.HasTile(cellPos))
            {
                Vector3 pos = breakableTilemap.GetCellCenterWorld(cellPos);

                DestroySpikeAt(pos);
                SpawnParticles(pos, new Color(0.3f, 0.6f, 1f));

                breakableTilemap.SetTile(cellPos, null);
                return;
            }

            // place block
            if (!groundTilemap.HasTile(cellPos) && currentBlocks < maxBlocks)
            {
                Vector3 pos = groundTilemap.GetCellCenterWorld(cellPos);

                DestroySpikeAt(pos);
                SpawnParticles(pos, new Color(1f, 0.9f, 0.3f));

                groundTilemap.SetTile(cellPos, playerBlockTile);
                currentBlocks++;

                if (blockUI != null)
                    blockUI.UpdateUI(currentBlocks);
            }
        }

        void SpawnParticles(Vector3 position, Color color)
        {
            if (particlePrefab == null) return;

            ParticleSystem ps = Instantiate(particlePrefab, position, Quaternion.identity);

            var main = ps.main;
            main.startColor = color;

            ps.Play();
            Destroy(ps.gameObject, 1f);
        }

        void DestroySpikeAt(Vector3 worldPosition)
        {
            Vector3Int cellPos = groundTilemap.WorldToCell(worldPosition);
            Vector3 cellCenter = groundTilemap.GetCellCenterWorld(cellPos);

            Collider2D[] hits = Physics2D.OverlapBoxAll(cellCenter, new Vector2(1.1f, 1.1f), 0f);

            foreach (Collider2D hit in hits)
            {
                Spike spike = hit.GetComponent<Spike>();

                if (spike != null)
                {
                    spike.BreakSpike();
                }
            }
        }

        void OnDrawGizmos()
        {
            if (player == null) return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(player.position, editRange);
        }
    }
}