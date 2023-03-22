using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
public class RightBigArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    StreamReader sr = new StreamReader("./data-1655740584615.txt");
    // Update is called once per frame
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
                float x = float.Parse(lineArray[20]), y = float.Parse(lineArray[21]), z = float.Parse(lineArray[22]), w = float.Parse(lineArray[23]);
                transform.rotation = new Quaternion(x, y, z, w);
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
