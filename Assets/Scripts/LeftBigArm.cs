using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class LeftBigArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = new Quaternion((float)-0.97490001, (float)-0.06170000, (float)-0.18370000, (float)-0.10890000);
        Vector3 a = new Quaternion(-0.65020001f, 0.16520000f, -0.18380000f, -0.71829998f).eulerAngles;
        List<Vector3> action1 = new List<Vector3>();
        action1.Add(a);
    }
    Vector3 axis = new Vector3(1, 1, 1);
    //Quaternion q = Quaternion.AngleAxis(float angle,axis);
    //Quaternion q = new Quaternion(1, 1, 1, 1);
    //List<Vector3>action1 = new List<Vector3>();
    // Update is called once per frame
    StreamReader sr = new StreamReader("./data-1655740584615.txt");
    void Update()
    {
        try
        {
            // 创建一个 StreamReader 的实例来读取文件 
            // using 语句也能关闭 StreamReader
                string line;
                if((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] lineArray = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                    //float speed = 1f;
                    //Vector3 a = new Quaternion(-0.81870002f, -0.00850000f, 0.10660000f, -0.56400001f).eulerAngles;
                    //transform.Rotate(a * speed * Time.deltaTime);
                    //transform.Rotate
                    //Vector3.up * Time.deltaTime * speed);
                    //float x = -0.81870002f, y = -0.00850000f, z = 0.10660000f, w = -0.56400001f;
                    float x = float.Parse(lineArray[8]), y = float.Parse(lineArray[9]), z = float.Parse(lineArray[10]), w = float.Parse(lineArray[11]);
                    transform.rotation = new Quaternion(x, y, z, w);
                    //float x1 = float.Parse(lineArray[4]), y1 = float.Parse(lineArray[5]), z1 = float.Parse(lineArray[6]), w1 = float.Parse(lineArray[7]);
                    Transform[] child = transform.GetComponentsInChildren<Transform>();
                    child[1].rotation = new Quaternion(x, y, z, w);
            }
        }
        catch (Exception e)
        {
            // 向用户显示出错消息
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        Console.ReadKey();
    }
    
}

