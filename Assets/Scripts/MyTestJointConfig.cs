using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class MyTestJointConfig : MonoBehaviour
{
    [Tooltip("If the T-pose is not zero rotations, check this on and let the character begin with T-pose (including root rotation)")]
    public bool readZeroPoseOnStart = false;
    [Tooltip("All rotational joints in the root-to-leaf order. The parent of the first joint is assumed a translational joint.")]
    public Transform[] joints;

    public Transform TranslationJoint { get { return joints[0].parent; } }
    public Quaternion[] Tpose { get; set; }
    public Vector3 BeginPosition { get; set; }
    public Quaternion BaseRotation { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        Tpose = new Quaternion[joints.Length];
        for (int i = 0; i < joints.Length; i++)
        {
            Tpose[i] = readZeroPoseOnStart ? joints[i].localRotation : Quaternion.identity;
        }
        BaseRotation = readZeroPoseOnStart ? joints[0].parent.rotation : Quaternion.identity;
        BeginPosition = TranslationJoint.localPosition;
        Vector3 lowerArm = new Vector3(0f, 90f, 0f);
        Quaternion currentQ2 = Quaternion.Euler(lowerArm.x, lowerArm.y, lowerArm.z);
        //joints[6].rotation = currentQ2 * joints[6].rotation;
        //joints[2].localRotation = Quaternion.Inverse(joints[6].rotation)* currentQ2 * joints[2].rotation;
    }
    StreamReader sr = new StreamReader("./data-1655740584615.txt");
    //treamReader sr = new StreamReader("./data-1655740920790.txt");
    //StreamReader sr = new StreamReader("./data-����.txt");

    // Update is called once per frame
    void Update()
    {
        //Tpose[6]= new Quaternion(0, 1, 0, 0);
        //joints[1].localRotation = new Quaternion(0, 1, 0, 0);
        Quaternion[] Curpose = new Quaternion[joints.Length];
        Vector3 lowerArm = new Vector3(0f, 90f, 0f);
        Quaternion currentQ2 = Quaternion.Euler(lowerArm.x, lowerArm.y, lowerArm.z);
        //Quaternion rotU = Quaternion.AngleAxis(200 * Time.deltaTime, Vector3.up);
        //joints[2].localRotation =joints[2].localRotation*rotU;
        try
        {
            // ����һ�� StreamReader ��ʵ������ȡ�ļ� 
            // using ���Ҳ�ܹر� StreamReader
            string line;
            string[] lineArray = new string[28];
            if ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);

                for (int i = 0; i < 24; i++)
                {
                    lineArray[i] = line.Substring(i * 12, 12);
                }
                for (int i = 24; i < 28; i++)
                {
                    lineArray[i] = line.Substring(i * 12 + 96, 12);
                }
            }
            for(int i = 0; i < 7; i++)
            {
                Curpose[i] = new Quaternion(float.Parse(lineArray[4*i]), float.Parse(lineArray[4 * i+1]), float.Parse(lineArray[4 * i+2]), float.Parse(lineArray[4 * i+3]));
            }
            Quaternion a= getQ1ToQ2(joints[6].rotation, Curpose[6]);//��������ת
            Quaternion c = getQ1ToQ2(joints[2].rotation, Curpose[2]);//��������ת
            Quaternion d = getQ1ToQ2(joints[1].rotation, Curpose[1]);//��������ת
            Quaternion f = getQ1ToQ2(joints[0].rotation, Curpose[0]);//������������ת
            //Quaternion e = getRelativeQua(c, d);
            Quaternion c1 = getQ1ToQ2(joints[5].rotation, Curpose[5]);//��������ת
            Quaternion d1 = getQ1ToQ2(joints[4].rotation, Curpose[4]);//��������ת
            Quaternion f1 = getQ1ToQ2(joints[3].rotation, Curpose[3]);//������������ת
            //Quaternion e1 = getRelativeQua(c1, d1);
            Quaternion b = getRelativeQua(a, c);//�ӱ�����ת
            Quaternion b1 = getRelativeQua(a, c1);//�ӱ�����ת
            Quaternion s = Quaternion.Inverse(b) * Quaternion.Inverse(a) * d;//�ﱾ����ת
            Quaternion s1 = Quaternion.Inverse(b1) * Quaternion.Inverse(a) * d1;//�ﱾ����ת
            Quaternion test = getRelativeQua(a, currentQ2);//�ӱ�����ת
            //joints[6].localRotation = a * joints[6].rotation;
            //joints[2].localRotation =test * joints[2].rotation;
            //joints[5].localRotation = Quaternion.Inverse(joints[6].rotation) * currentQ2 * joints[5].rotation;
            /*
            joints[2].localRotation = b * joints[2].rotation;
            joints[1].localRotation = s*joints[1].rotation;
            joints[0].localRotation = Quaternion.Inverse(s)*Quaternion.Inverse(b) * Quaternion.Inverse(a) * f*joints[0].rotation;//�����ӱ�����ת���
            joints[5].localRotation = b1 * joints[5].rotation;
            joints[4].localRotation = s1 * joints[4].rotation;
            joints[3].localRotation = Quaternion.Inverse(s1) * Quaternion.Inverse(b1) * Quaternion.Inverse(a) * f1 * joints[3].rotation;
            */
            joints[2].localRotation = b ;
            joints[1].localRotation = s ;
            joints[0].localRotation = Quaternion.Inverse(s) * Quaternion.Inverse(b) * Quaternion.Inverse(a) * f ;//�����ӱ�����ת���
            joints[5].localRotation = b1 ;
            joints[4].localRotation = s1 ;
            joints[3].localRotation = Quaternion.Inverse(s1) * Quaternion.Inverse(b1) * Quaternion.Inverse(a) * f1;
        }
        catch (Exception e)
        {
            // ���û���ʾ������Ϣ
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        Console.ReadKey();

    }
    Quaternion getQ1ToQ2(Quaternion a, Quaternion b)
    {
        return b * Quaternion.Inverse(a);
    }
    Quaternion getRelativeQua(Quaternion father, Quaternion son)
    {
        return Quaternion.Inverse(father) * son;
    }
}
//�������
/*
                 //string[] lineArray = Regex.Split(line, " ", RegexOptions.IgnoreCase);
                //string[] lineArray = Regex.Split(line, "\\s+");
                float x6 = float.Parse(lineArray[24]), y6 = float.Parse(lineArray[25]), z6 = float.Parse(lineArray[26]), w6 = float.Parse(lineArray[27]);
                //joints[6].rotation = new Quaternion(x6, y6, z6, w6)* currentQ2;
                //Tpose[6] = new Quaternion(x6, y6, z6, w6);
                //����
                float x2 = float.Parse(lineArray[8]), y2 = float.Parse(lineArray[9]), z2 = float.Parse(lineArray[10]), w2 = float.Parse(lineArray[11]);
                //joints[2].localRotation = new Quaternion(x6, -y6, -z6, -w6)*new Quaternion(x2, y2, z2, w2);
                //joints[2].localRotation =joints[6].localRotation*new Quaternion(x2, y2, z2, w2)*Quaternion.Inverse(joints[6].localRotation);
                //joints[2].localRotation = Quaternion.Inverse(joints[6].rotation)  * new Quaternion(x2, y2, z2, w2)* currentQ2;
                joints[2].rotation = new Quaternion(x2, y2, z2, w2);

                float x1 = float.Parse(lineArray[4]), y1 = float.Parse(lineArray[5]), z1 = float.Parse(lineArray[6]), w1 = float.Parse(lineArray[7]);
                //joints[1].localRotation = new Quaternion(x2, y2, z2, w2) * new Quaternion(x1, y1, z1, w1)*Quaternion.Inverse(new Quaternion(x2, y2, z2, w2));
                //joints[1].localRotation = new Quaternion(x2, -y2, -z2, -w2)*new Quaternion(x1, y1, z1, w1);
                //joints[1].localRotation = new Quaternion(x1, y1, z1, w1);
                joints[1].localRotation = Quaternion.Inverse(joints[2].rotation) * new Quaternion(x1, y1, z1, w1)* currentQ2;
                float x0 = float.Parse(lineArray[0]), y0 = float.Parse(lineArray[1]), z0 = float.Parse(lineArray[2]), w0 = float.Parse(lineArray[3]);
                //joints[0].localRotation =new Quaternion(x1, y1, z1, w1) * new Quaternion(x0, y0, z0, w0)* Quaternion.Inverse(new Quaternion(x1, y1, z1, w1));
                //joints[0].localRotation = new Quaternion(x1, -y1, -z1, -w1)*new Quaternion(x0, y0, z0, w0);
                //joints[0].localRotation =new Quaternion(x0, y0, z0, w0);
                joints[0].localRotation = Quaternion.Inverse(joints[1].rotation) *  new Quaternion(x0, y0, z0, w0) * currentQ2;
                //����


                float x5 = float.Parse(lineArray[20]), y5 = float.Parse(lineArray[21]), z5 = float.Parse(lineArray[22]), w5 = float.Parse(lineArray[23]);
                //joints[5].localRotation = new Quaternion(x6, -y6, -z6, -w6) * new Quaternion(x5, y5, z5, w5);
                //joints[5].localRotation = Quaternion.Inverse(joints[6].localRotation) * new Quaternion(x5, y5, z5, w5);
                //joints[5].localRotation = Quaternion.Inverse(joints[6].rotation) *  new Quaternion(x5, y5, z5, w5)* currentQ2;
                joints[5].rotation = new Quaternion(x5, y5, z5, w5);
                float x4 = float.Parse(lineArray[16]), y4 = float.Parse(lineArray[17]), z4 = float.Parse(lineArray[18]), w4 = float.Parse(lineArray[19]);
                //joints[4].localRotation = new Quaternion(x5, -y5, -z5, -w5) * new Quaternion(x4, y4, z4, w4);
                //joints[4].localRotation = Quaternion.Inverse(new Quaternion(x5, y5, z5, w5)) * new Quaternion(x4, y4, z4, w4);
                joints[4].localRotation = Quaternion.Inverse(joints[5].rotation) * new Quaternion(x4, y4, z4, w4) * currentQ2;
                float x3 = float.Parse(lineArray[12]), y3 = float.Parse(lineArray[13]), z3 = float.Parse(lineArray[14]), w3 = float.Parse(lineArray[15]);
                joints[3].localRotation = Quaternion.Inverse(joints[4].rotation) * new Quaternion(x3, y3, z3, w3) * currentQ2;
 */
