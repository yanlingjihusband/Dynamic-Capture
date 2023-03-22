using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class LeftSmallArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    StreamReader sr = new StreamReader("./data-1655740584615-1.txt");
    void Update()
    {
        try
        {
            // 创建一个 StreamReader 的实例来读取文件 
            // using 语句也能关闭 StreamReader
            string line;
            if ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
                string[] lineArray = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                float x = float.Parse(lineArray[8]), y = float.Parse(lineArray[9]), z = float.Parse(lineArray[10]), w = float.Parse(lineArray[11]);
                float x1 = float.Parse(lineArray[4]), y1 = float.Parse(lineArray[5]), z1 = float.Parse(lineArray[6]), w1 = float.Parse(lineArray[7]);
                transform.rotation = new Quaternion(x1, y1, z1, w1);
            }
        }
        catch (Exception e)
        {
            // 向用户显示出错消息
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        Console.ReadKey();
        //float speed = 1f;
        //Vector3 a = (new Quaternion(-0.81870002f, - 0.00850000f,  0.10660000f, - 0.56400001f)* new Quaternion(0.75749999f, 0.04080000f, -0.06790000f, 0.64789999f)).eulerAngles;
        //Vector3 a = new Quaternion(0.75749999f, 0.04080000f, -0.06790000f, 0.64789999f).eulerAngles;
        //transform.Rotate(a * speed * Time.deltaTime);
    }
}
