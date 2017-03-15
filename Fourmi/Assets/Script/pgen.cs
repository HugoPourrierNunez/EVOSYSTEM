﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Fourmis
{
    int initEnergy = 1;
    int foodEnergy = 6;
    int energyConsum =  1;
    int w = 32;
    int h = 32;
    int[,] map =
        {
            {0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
            {0,0,0,1,1,1,1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
            {0,0,0,1,1,0,0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
      
    private DNA fDNA;
    private int energy, posX, posY, orientation,score;
    private List<Vector2> pos = new List<Vector2>();
    private int cpt=0;

    public int getDNAsize()
    {
        return fDNA.getSize();
    }

    public List<Vector2> getPos()
    {
        return pos;
    }

    public int getEnergy()
    {
        return energy;
    }

    public float getSco()
    {
        return score+((float)score) / fDNA.getSize();
    }

    public List<Vector2> getList(){
        List<Vector2> food = new List<Vector2>();

        food.Add(new Vector2(0, 1));
        food.Add(new Vector2(0, 2));
        food.Add(new Vector2(0, 3));
        food.Add(new Vector2(1, 3));
        food.Add(new Vector2(2, 3));
        food.Add(new Vector2(3, 3));
        food.Add(new Vector2(4, 3));
        food.Add(new Vector2(5, 3));
        food.Add(new Vector2(5, 4));
        food.Add(new Vector2(5, 5));
        food.Add(new Vector2(5, 6));
        food.Add(new Vector2(5, 8));
        food.Add(new Vector2(5, 9));
        food.Add(new Vector2(5, 10));
        food.Add(new Vector2(5, 11));
        food.Add(new Vector2(5, 12));
        food.Add(new Vector2(6, 12));
        food.Add(new Vector2(7, 12));
        food.Add(new Vector2(8, 12));
        food.Add(new Vector2(9, 12));
        food.Add(new Vector2(10, 12));
        food.Add(new Vector2(12, 12));
        food.Add(new Vector2(13, 12));
        food.Add(new Vector2(14, 12));
        food.Add(new Vector2(15, 12));
        food.Add(new Vector2(18, 12));
        food.Add(new Vector2(19, 12));
        food.Add(new Vector2(20, 12));
        food.Add(new Vector2(21, 12));
        food.Add(new Vector2(22, 12));
        food.Add(new Vector2(23, 12));
        food.Add(new Vector2(24, 11));
        food.Add(new Vector2(24, 10));
        food.Add(new Vector2(24, 9));
        food.Add(new Vector2(24, 8));
        food.Add(new Vector2(24, 7));
        food.Add(new Vector2(24, 4));
        food.Add(new Vector2(24, 3));
        food.Add(new Vector2(25, 1));
        food.Add(new Vector2(26, 1));
        food.Add(new Vector2(27, 1));
        food.Add(new Vector2(28, 1));
        food.Add(new Vector2(30, 2));
        food.Add(new Vector2(30, 3));
        food.Add(new Vector2(30, 4));
        food.Add(new Vector2(30, 5));
        food.Add(new Vector2(29, 7));
        food.Add(new Vector2(28, 7));
        food.Add(new Vector2(27, 8));
        food.Add(new Vector2(27, 9));
        food.Add(new Vector2(27, 10));
        food.Add(new Vector2(27, 11));
        food.Add(new Vector2(27, 12));
        food.Add(new Vector2(27, 13));
        food.Add(new Vector2(27, 14));
        food.Add(new Vector2(26, 16));
        food.Add(new Vector2(25, 16));
        food.Add(new Vector2(24, 16));
        food.Add(new Vector2(21, 16));
        food.Add(new Vector2(19, 16));
        food.Add(new Vector2(18, 16));
        food.Add(new Vector2(17, 16));
        food.Add(new Vector2(16, 17));
        food.Add(new Vector2(15, 20));
        food.Add(new Vector2(14, 20));
        food.Add(new Vector2(11, 20));
        food.Add(new Vector2(10, 20));
        food.Add(new Vector2(9, 20));
        food.Add(new Vector2(8, 20));
        food.Add(new Vector2(5, 21));
        food.Add(new Vector2(5, 22));
        food.Add(new Vector2(4, 24));
        food.Add(new Vector2(3, 24));
        food.Add(new Vector2(2, 25));
        food.Add(new Vector2(2, 26));
        food.Add(new Vector2(2, 27));
        food.Add(new Vector2(3, 29));
        food.Add(new Vector2(4, 29));
        food.Add(new Vector2(6, 29));
        food.Add(new Vector2(9, 29));
        food.Add(new Vector2(12, 29));
        food.Add(new Vector2(14, 28));
        food.Add(new Vector2(14, 27));
        food.Add(new Vector2(14, 26));
        food.Add(new Vector2(15, 23));
        food.Add(new Vector2(18, 24));
        food.Add(new Vector2(19, 27));
        food.Add(new Vector2(22, 26));
        food.Add(new Vector2(23, 23));
        return food;
        }

    public Fourmis()
    {
        fDNA = null;
        posX = 0;
        posY = 0;
        energy = initEnergy;
        orientation = 0;
    }

    public Fourmis(DNA a)
    {
        posX = 0;
        posY = 0;
        fDNA = a;
        energy = initEnergy;
        pos.Add(new Vector2(0, 0));
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int s)
    {
        score = s;
    }

    public void setOrientation(int o)
    {
        orientation = o;
    }

    public int getOrientation()
    {
        return orientation;
    }

    public void setDNA(DNA a)
    {
        fDNA = a;
    }

    public DNA getDNA()
    {
        return fDNA;
    }

    public void setPosX(int x)
    {
        posX = x;
    }

    public void setPosY(int y)
    {
        posY = y;
    }

    public int getX()
    {
        return posX;
    }

    public int getY()
    {
        return posY;
    }

    public Fourmis crossOver(Fourmis a)
    {
        
        
        Fourmis f=new Fourmis(fDNA.clone());

        Fourmis f1 = new Fourmis(a.getDNA().clone());

        int ind1 = (Random.Range(0, f.fDNA.getSize()));
        //int ind1 = (Random.Range(f.fDNA.getSize()/2, f.fDNA.getSize()));
        //int ind1 = (f.fDNA.getSize()-1);
        // int ind2 = (Random.Range(0, f1.getDNA().getSize()));
        int ind2 = (Random.Range(0, f1.getDNA().getSize()));
        f.fDNA.setDnaAt(ind1,(f1.fDNA.getDnaAt(ind2).clone()));
        f.energy=initEnergy;

        return f;
    }

    public Fourmis mutate()
    {
        string[] label = { "if", "move", "right", "left", "prog2", "prog3" };
        Fourmis f = new Fourmis(fDNA.clone());
        Fourmis a = new Fourmis(new DNA(label[Random.Range(0,6)]));
        
        a.getDNA().createRandom(6);

        f.fDNA.setDnaAt((Random.Range(0, f.fDNA.getSize())), (a.getDNA().clone()));
        f.energy = initEnergy;
        return f;
    }

    public bool foodInFront()
    {
        switch(orientation)
        {
            case 0:
                
                if (map[Mod(posX,h),Mod((posY+1),w)]==1)
                {
                    return true;
                }
                break;
            case 1:
               
                if (map[Mod((posX+1), h),Mod( posY , w)] == 1)
                {
                    return true;
                }
                break;
            case 2:
                
                if (map[Mod(posX , h), Mod((posY-1 ), w)] == 1)
                {
                    return true;
                }
                break;
            case 3:
                if (map[Mod((posX -1),h), Mod( posY , w)] == 1)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public void runDNA(DNA a)
    {
        
        if (a.getChild()!=null)
        {
            switch (a.getData())
            {
                
                case "if":
                    if(foodInFront())
                    {
                        runDNA(a.getChild()[0]);
                    }
                    else
                    {
                        runDNA(a.getChild()[1]);
                    }
                    break;
                case "prog2":

                    runDNA(a.getChild()[0]);
                    runDNA(a.getChild()[1]);

                    break;

                case "prog3":

                    runDNA(a.getChild()[0]);
                    runDNA(a.getChild()[1]);
                    runDNA(a.getChild()[2]);

                    break;
                   
            }
        }
        else
        {
            if (energy > 0)
            {
                
                exec(a.getData());
            }
        }
    }
    
    public void exec(string ac)
    {
        switch (ac)
        {
            case "move":
                move();
                break;

            case "right":
                right();
                break;
            case "left":
                left();
                break;
        }
    }

    public void move()
    {
            switch (orientation)
            {
                case 0:
                    if (map[Mod(posX, h), Mod((posY + 1), w)] == 1)
                    {

                        energy += foodEnergy;
                        

                        score++;
                        map[Mod(posX, h), Mod((posY + 1), w)] = 0;
                        cpt++;
                    }
                    posX = Mod(posX, h);
                    posY = Mod((posY + 1), w);
                    break;
                case 1:
                    if (map[Mod((posX + 1), h), Mod(posY, w)] == 1)
                    {

                        energy += foodEnergy;
                        score++;
                        map[Mod((posX + 1), h), Mod(posY, w)] = 0;
                        cpt++;
                    }
                    posX = Mod((posX + 1), h);
                    posY = Mod(posY, w);
                    break;
                case 2:
                    if (map[Mod(posX, h), Mod((posY - 1), w)] == 1)
                    {
                        energy += foodEnergy;
                        score++;
                        map[Mod(posX, h), Mod((posY - 1), w)] = 0;
                        cpt++;
                    }


                    posX = Mod(posX, h);
                    posY = Mod((posY - 1), w);
                    break;
                case 3:

                    if (map[Mod((posX - 1), h), Mod(posY, w)] == 1)
                    {
                        energy += foodEnergy;
                        score++;
                        map[Mod((posX - 1), h), Mod(posY, w)] = 0;
                        cpt++;
                    }

                    posX = Mod((posX - 1), h);
                    posY = Mod(posY, w);
                    break;
            }
        
        energy = energy - energyConsum;


        pos.Add(new Vector2(posX, posY));
    }

    public void right()
    {
        orientation = Mod(orientation+1,4);
        energy = energy - energyConsum;
    }

    public void left()
    {
        orientation = Mod(orientation - 1, 4);
        energy = energy - energyConsum;

    }
    
    public int Mod(int a, int b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }

    public void fullProg()
    {
        while (energy > 0)
        {
            if(score==89)
            {
                break;
            }
            else{

                runDNA(fDNA);
            }
        }
    }
    
}



public class pgen : MonoBehaviour {
    // Use this for initialization
    int[,] map =
        {
            {0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0},
            {0,0,0,1,1,1,1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
            {0,0,0,1,1,0,0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
    string[] label = { "if", "move", "right", "left", "prog2", "prog3"};
    public GameObject mapTile;
    public GameObject mapTile1;
    public GameObject fourmi;
    int nbPopulation = 500;
    int nbSelect = 50;
    float mutateLuck = 70;
    int nbGeneration = 40;
    int i = 0;
    int frame = 0;


    List<Fourmis> pF = new List<Fourmis>();

    List<Fourmis> initiatePopulation()
    {
        List<Fourmis> populationFourmis = new List<Fourmis>();

        for (int i = 0; i < nbPopulation; i++)
        {

            Fourmis a = new Fourmis(new DNA(label[Random.Range(0, 6)]));

            a.getDNA().createRandom(6);
            a.fullProg();
            populationFourmis.Add(a);

        }

        return populationFourmis;

    }

    void sortList(ref List<Fourmis> populationFourmis)
    {
        populationFourmis.Sort((x, y) => y.getSco().CompareTo(x.getSco()));
        
    }

    List<Fourmis> selection(List<Fourmis> populationFourmis)
    {
        
        List<Fourmis> f = new List<Fourmis>();
        for(int i=0; i<nbSelect; i++)
        {
            f.Add(new Fourmis(populationFourmis[i].getDNA().clone()));
        }
          

            return f;


    }

    List<Fourmis> crossOver(List<Fourmis> l1,List<Fourmis> selectedList)
    {
        
        List<Fourmis> l = new List<Fourmis>();
       

        for(int i=0;i<0;i++)
        {
            l.Add(new Fourmis(selectedList[i].getDNA().clone()));
        }
        for (int i = 0; i < nbPopulation; i++)
        {
            l.Add(selectedList[Random.Range(0, nbSelect)].crossOver(selectedList[Random.Range(0, nbSelect)]));
            
        }

        sortList(ref l);

        return l;

    }

    void mutate(ref List<Fourmis> l)
    {
        
        for(int i=0;i<nbPopulation;i++)
        {

            if (Random.Range(1, 100) < mutateLuck)
            {
                l[i] = l[i].mutate();
            }
        }
    }

    void runPopulation(List<Fourmis> populationFourmis)
    {
        for(int i =0; i<nbPopulation;i++)
        {
            
            populationFourmis[i].fullProg();
        }
    }


    void Start () {

        //Random.InitState(3);        // Seed fonctionne pour 9 gene

        //Random.InitState(2);

        for(int i=0;i<32;i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (map[i, j] == 0)
                {
                    GameObject newTile = Instantiate(mapTile);
                    newTile.transform.position = new Vector3(i, j);
                }
                else
                {
                    GameObject newTile = Instantiate(mapTile1);
                    newTile.transform.position = new Vector3(i, j);
                }
            }

        }
         List<Fourmis> populationFourmis = new List<Fourmis>();
         populationFourmis = initiatePopulation();
        sortList(ref populationFourmis);

        
         for (int i = 0; i < nbGeneration; i++)
         {
             populationFourmis=crossOver(populationFourmis, selection(populationFourmis));
             mutate(ref populationFourmis);
             runPopulation(populationFourmis);
            sortList(ref populationFourmis);
        }

         sortList(ref populationFourmis);
         for(int i=0;i<nbPopulation;i++)
         {

             //Debug.Log(populationFourmis[i].getScore());
         }
         

        


       pF = populationFourmis;
        /*
        DNA f214 = new DNA("if");
        DNA f215 = new DNA("move");
        DNA f216 = new DNA("prog2");

        DNA f217 = new DNA("right");
        DNA f218 = new DNA("if");
        DNA f219 = new DNA("move");
        DNA f220 = new DNA("prog3");
        DNA f221 = new DNA("left");
        DNA f222 = new DNA("left");
        DNA f223 = new DNA("if");
        DNA f224 = new DNA("move");
        DNA f225 = new DNA("prog2");
        DNA f226 = new DNA("right");
        DNA f227 = new DNA("move");

        DNA[] tab = new DNA[2];
        tab[0] = f215;
        tab[1] = f216;

        f214.setChild(tab);

        DNA[] tab1 = new DNA[2];
        tab1[0] = f217;
        tab1[1] = f218;


        f216.setChild(tab1);

        DNA[] tab2 = new DNA[2];
        tab2[0] = f219;
        tab2[1] = f220;

        f218.setChild(tab2);

        DNA[] tab3 = new DNA[3];
        tab3[0] = f221;
        tab3[1] = f222;
        tab3[2] = f223;


        f220.setChild(tab3);

        DNA[] tab4 = new DNA[2];
        tab4[0] = f224;
        tab4[1] = f225;


        f223.setChild(tab4);

        DNA[] tab5 = new DNA[2];
        tab5[0] = f226;
        tab5[1] = f227;


        f225.setChild(tab5);

        pF.Add(new Fourmis(f214));
        pF[0].fullProg();

        */
       



    }

    void Update()
    {
        if (i < pF[0].getPos().Count && frame%1==0 )
        {
            fourmi.transform.position = new Vector3(pF[0].getPos()[i].x, pF[0].getPos()[i].y, -2);

            Instantiate(fourmi);
            //Debug.Log("asa");
            i++;
        }
        frame++;
    }
    

    
	
}
