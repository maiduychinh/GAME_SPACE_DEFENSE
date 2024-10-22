
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform bulletSpawnPoint; // Điểm phát đạn
    public GameObject bulletPrefab; // Prefab của viên đạn
    public float bulletSpeed = 10f; // Tốc độ viên đạn
    public float shootingRadius = 15f; // Bán kính bắn
    public float shootingInterval = 1f; // Thời gian giữa các lần bắn

    private Transform closestEnemy; // Kẻ thù gần nhất
    private float lastShotTime = 0f; // Thời gian bắn cuối cùng
    private PlayerLevelManager levelManager; // Quản lý cấp độ của người chơi
    public GameObject orbitingPrefab; // Prefab của các đối tượng quay quanh
    public List<GameObject> orbitingObjects = new List<GameObject>(); // Danh sách các đối tượng quay quanh
    public float orbitRadius = 2f; // Bán kính quỹ đạo
    public float orbitSpeed = 50f; // Tốc độ quay

    public int currentSkillLevel =0; // Cấp độ hiện tại

    void Start()
    {
        levelManager = GetComponent<PlayerLevelManager>(); // Lấy component quản lý cấp độ
    }

    void Update()
    {
        closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.position);

            if (distanceToEnemy <= shootingRadius)
            {
                Vector3 direction = (closestEnemy.position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

                if (Time.time - lastShotTime > shootingInterval) // Chỉ bắn một lần trong khoảng thời gian nhất định
                {
                    Shoot(); // Gọi phương thức bắn
                    lastShotTime = Time.time; // Cập nhật thời gian bắn cuối
                }
            }
        }

        // Cập nhật vị trí của các prefab bay quanh player
        UpdateOrbitingObjects();
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemi"); // Lấy tất cả kẻ thù
        float minDistance = Mathf.Infinity; // Khoảng cách tối thiểu
        Transform nearestEnemy = null; // Kẻ thù gần nhất

        foreach (GameObject enemy in enemies) // Duyệt qua tất cả kẻ thù
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position); // Tính khoảng cách
            if (distance < minDistance) // Nếu khoảng cách nhỏ hơn khoảng cách tối thiểu
            {
                minDistance = distance; // Cập nhật khoảng cách tối thiểu
                nearestEnemy = enemy.transform; // Cập nhật kẻ thù gần nhất
            }
        }

        return nearestEnemy; // Trả về kẻ thù gần nhất
    }
    public void Shoot()
    {
        // Kiểm tra chỉ số currentSkillLevel
        if (currentSkillLevel < 0 || currentSkillLevel >= UiController.instance.PauseLevel.skill3Levels.Length)
        {
            Debug.Log(currentSkillLevel);
            Debug.Log(UiController.instance.PauseLevel.skill3Levels.Length);
            return; // Thoát nếu không hợp lệ
        }

        // Lấy số lượng đạn từ Skill hiện tại
        int quantityBullet = UiController.instance.PauseLevel.skill3Levels[currentSkillLevel].quantityBullet;

        float spacing = 1f; // Khoảng cách giữa các viên đạn
        Vector3 direction = (closestEnemy.position - bulletSpawnPoint.position).normalized; // Hướng đến kẻ thù

        for (int i = 0; i < quantityBullet; i++) // Duyệt qua số lượng viên đạn
        {
            // Tính toán vị trí bắn cho từng viên đạn dựa trên khoảng cách
            Vector3 spawnPosition = bulletSpawnPoint.position + direction * (i * spacing); // Vị trí phát đạn
            var bullet = Instantiate(bulletPrefab, spawnPosition, bulletSpawnPoint.rotation); // Tạo viên đạn
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed; // Thiết lập vận tốc viên đạn
        }
    }
    public void OnEnemyKilled(EnemyData enemyData)
    {
        levelManager.GainExperience(enemyData.exp); // Cộng kinh nghiệm cho người chơi từ EnemyData
    }

    public void TrySpawnOrbitingPrefab()
    {
        if (orbitingObjects.Count < 5) // Nếu số lượng đối tượng quay quanh ít hơn 5
        {
            SpawnOrbitingPrefab(); // Spawn prefab quay quanh
        }
    }

    public void SpawnOrbitingPrefab()
    {
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // Góc quay
        GameObject newOrbitingObject = Instantiate(orbitingPrefab, transform.position, spawnRotation, this.transform); // Tạo đối tượng quay quanh
        orbitingObjects.Add(newOrbitingObject); // Thêm vào danh sách
    }

    private void UpdateOrbitingObjects()
    {
        for (int i = 0; i < orbitingObjects.Count; i++) // Duyệt qua danh sách các đối tượng quay quanh
        {
            if (orbitingObjects[i] != null) // Nếu đối tượng không null
            {
                float angle = Time.time * orbitSpeed + i * (360f / orbitingObjects.Count); // Tính góc quay
                float x = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius; // Tính vị trí x
                float z = transform.position.z + Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius; // Tính vị trí z
                orbitingObjects[i].transform.position = new Vector3(x, 0.5f, z); // Cập nhật vị trí đối tượng quay
            }
        }
    }
}

