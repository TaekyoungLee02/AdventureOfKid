using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase<T> where T : DataTypeBase
{
    private Dictionary<int, T> _datas = new();

    public DataBase() { }

    public void Initalize(List<T> datas)
    {
        if (datas == null) return;

        for (int i = 0; i < datas.Count; i++)
        {
            _datas.Add(datas[i].id, datas[i]);
        }
    }

    public T GetData(int id)
    {
        return _datas[id];
    }
}
