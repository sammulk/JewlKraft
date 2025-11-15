using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Inventory_files.Scripts.CustomerScripts
{
    public class CustomerController : MonoBehaviour
    {
        public static event Action OnCustomerNeeded;
        
        public static CustomerController Instance { get; private set; }

        private string _previousScene;
        private string _currentScene;


        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _currentScene = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _previousScene = _currentScene;
            _currentScene = scene.name;

            HandleSceneTransition(_previousScene, _currentScene);
        }

        private void HandleSceneTransition(string fromScene, string toScene)
        {
            Debug.Log($"from {fromScene} to {toScene}");
            // Example logic: spawn customers only if coming *from* dungeon
            if (fromScene == "Dungeon_scene" && toScene == "Shop_scene") OnCustomerNeeded?.Invoke();
        }
    }
}

