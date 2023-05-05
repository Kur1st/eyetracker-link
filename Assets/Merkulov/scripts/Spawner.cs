using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEditor.PlayerSettings;

public class Spawner : MonoBehaviour
{
    public GameObject VegeterianFood;
    public GameObject FoodCluser;
    public GameObject Enemy;
    public GameObject Player;
    public float percent = 80f;
    public float ticks = 0;
    void Start()
    {
    }
    void Update()
    {
        Vector3 camera_pos = Camera.main.transform.position;
        float camera_size = Camera.main.orthographicSize;
        float ratio = (float) Screen.height / Screen.width;
        camera_pos.z = 0;
        if (ticks == 0)
        {
            /* 
             Когда прошло определённое количество секунд, мы создаём новую еду, раскиданных по 4 возможным направлениям
             Сверху камеры, снизу, справа и слева
             Появиться могут как кусочки еды, так и противники
            */
            float x = camera_size / ratio;
            float y = camera_size;
            GameObject obj;
            int generation = (int)(Random.Range(10, 15) * (3.5 - percent * 3.4f / 100));
            // Сверху
            for (int i=0; i<=generation; ++i)
            {
                if(98.5f < Random.Range(1, 100))
                    obj = FoodCluser;
                else
                    obj = VegeterianFood;
                Creater.CreateFoodObject(obj, camera_pos + new Vector3(Random.Range(-x, x), Random.Range(y, y * 2.5f), 0));
            }
            // Справа
            for (int i = 0; i <= generation; ++i)
            {
                if (98.5f < Random.Range(1, 100))
                {
                    obj = FoodCluser;
                }
                else
                {
                    obj = VegeterianFood;
                }
                Creater.CreateFoodObject(obj, camera_pos + new Vector3(Random.Range(x, x * 2.5f), Random.Range(-y, y), 0));
            }
            // Снизу
            for (int i = 0; i <= generation; ++i)
            {
                if (98.5f < Random.Range(1, 100))
                {
                    obj = FoodCluser;
                }
                else
                {
                    obj = VegeterianFood;
                }
                Creater.CreateFoodObject(obj, camera_pos + new Vector3(Random.Range(-x, x), Random.Range(-y * 2.5f, -y), 0));
            }
            // Слева
            for (int i = 0; i <= generation; ++i)
            {
                if (98.5f < Random.Range(1, 100))
                {
                    obj = FoodCluser;
                }
                else
                {
                    obj = VegeterianFood;
                }
                Creater.CreateFoodObject(obj, camera_pos + new Vector3(Random.Range(-x * 2.5f, -x), Random.Range(-y, y), 0));
            }
            CreateEnemy(x, y, camera_pos);
            if (percent < Random.Range(1f, 100f))
            {
                if (percent < -50)
                {
                    percent -= 5;
                }
                if (percent < -10)
                {
                    percent -= 3f;
                }
                if (percent < 10)
                {
                    percent -= 2f;
                }
                if (percent < 30)
                {
                    percent -= 2f;
                }
                if (percent < 50)
                {
                    percent -= 1f;
                }
                percent -= 1f;
                for (int i=0; i<Random.Range(1, (int)(0.8 * (1.6 - percent))); ++i) {
                    CreateEnemy(x, y, camera_pos);
                }
            }
            else
            { 
                percent -= 5f;
            }
        }
        ticks += 1 * Time.deltaTime;
        if (ticks > 10)
        {
            ticks = 0;
        }
    }

    void CreateEnemy(float x, float y, Vector3 camera_pos)
    {
        float rand_x = 0;
        float rand_y = 0;
        while (rand_x < x && rand_x > -x && rand_y > -y && rand_y < y)
        {
            rand_x = Random.Range(-x * 2.5f, x * 2.5f);
            rand_y = Random.Range(-y * 2.5f, y * 2.5f);
        }
        GameObject tmp = Creater.CreateEnemyObject(Enemy, Player);
        tmp.GetComponent<EnemyLogic>().transform.position = camera_pos + new Vector3(rand_x, rand_y, 0f);
    }
}