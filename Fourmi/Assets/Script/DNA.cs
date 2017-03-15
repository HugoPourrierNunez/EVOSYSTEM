using UnityEngine;
using System.Collections;



public class DNA
{
    string[] label = { "if", "move", "right", "left", "prog2", "prog3" };
    private string data;
    private DNA[] childDNA;

    public DNA()
    {
        data = "test";
        childDNA = null;
    }

    public DNA(string s)
    {
        data = s;
        childDNA = null;
    }

    public DNA(string s, DNA[] c)
    {
        data = s;
        childDNA = c;
    }
    
    public int getSize()
    {
        if (childDNA == null)
            return 1;
        else
        {

            int a = 0;
            for (int i = 0; i < childDNA.Length; i++)
            {
                a += childDNA[i].getSize();
            }

            return 1 + a;

        }
    }

    public DNA getDnaAt(int i)
    {
        if (i == 0) return this;
        else
        {
            int a = 0;
            int b = 0;

            for (int j = 0; j < childDNA.Length; j++)
            {
                if (childDNA[j].getSize() + b + 1 > i)
                {
                    a = j;
                    break;
                }

                b += childDNA[j].getSize();
            }

            i = i - b - 1;
            return childDNA[a].getDnaAt(i);
        }
    }

    public DNA getDNA()
    {
        return this;
    }

    public void setDnaAt(int i, DNA d)
    {
        DNA r = getDnaAt(i);
        r.data = d.data;
        r.childDNA = d.childDNA;
    }

    public void setChild(DNA[] d)
    {
        childDNA = d;
    }

    public void setData(string s)
    {
        data = s;
    }

    public string getData()
    {
        return data;
    }

    public void createRandom(int depth)
    {
        if (depth > 1)
        {
            depth--;
            switch (data)
            {
                case "if":

                    DNA []c = { new DNA(label[Random.Range(0, 6)]), new DNA(label[Random.Range(0, 6)]) };
                    c[0].createRandom(depth);
                    c[1].createRandom(depth);
                    setChild(c);
                    break;
                case "prog2":
                    DNA[] c1 = { new DNA(label[Random.Range(0, 6)]), new DNA(label[Random.Range(0, 6)]) };
                    c1[0].createRandom(depth);
                    c1[1].createRandom(depth);
                    setChild(c1);
                    break;
                case "prog3":
                    DNA[] c2 = { new DNA(label[Random.Range(0, 6)]), new DNA(label[Random.Range(0, 6)]), new DNA(label[Random.Range(0, 6)]) };
                    c2[0].createRandom(depth);
                    c2[1].createRandom(depth);
                    c2[2].createRandom(depth);
                    setChild(c2);
                    break;
            }
        }
        else
        {
            switch (data)
            {
                case "if":

                    DNA[] c = { new DNA(label[Random.Range(1, 4)]), new DNA(label[Random.Range(1, 4)]) };
                    
                    setChild(c);
                    break;
                
                case "prog2":
                    DNA[] c1 = { new DNA(label[Random.Range(1, 4)]), new DNA(label[Random.Range(1, 4)]) };
                    
                    setChild(c1);
                    break;
                case "prog3":
                    DNA[] c2 = { new DNA(label[Random.Range(1, 4)]), new DNA(label[Random.Range(1, 4)]), new DNA(label[Random.Range(1, 4)]) };
                    
                    setChild(c2);
                    break;
            }

        }
        
    }

    public DNA[] getChild()
    {
        return childDNA;
    }

    public DNA clone()
    {
        
        DNA c = new DNA(data);

        if (childDNA != null)
        {
            c.childDNA=new DNA[getChild().Length];
            for (int i = 0; i < getChild().Length; i++)
            {
                c.childDNA[i] = childDNA[i].clone();
            }

        }

        return c;
    }


}