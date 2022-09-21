using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float[] speeds; 
    public float[] times; 
    WheelJoint2D _blockWheel;
    JointMotor2D _turnBlock;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        _blockWheel = GetComponent<WheelJoint2D>();
        _turnBlock = new JointMotor2D();
        StartCoroutine("TurnProcess");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator TurnProcess()
    {
        index = 0;
        while (true)
        {
            _blockWheel.motor = _turnBlock; 
            _turnBlock.maxMotorTorque = 1000;
            _turnBlock.motorSpeed = speeds[index];
            times[0] = 0;
            yield return new WaitForSecondsRealtime(times[index]);
            speeds[index] = Random.Range(speeds[index] - 20, speeds[index] + 20);
            times[index] = Random.Range(times[index] - 1.5f, times[index] + 1.5f);
            index++;

            if (index == speeds.Length)
            {
                index = 0;
            }
        }
    }
}