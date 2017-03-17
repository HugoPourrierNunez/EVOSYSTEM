using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Fourmis
{
    int initEnergy = 30;
    int foodEnergy = 6;
    int energyConsum =  1;
    int limitEnergy = 60;
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
    private List<string> listAction = new List<string>();
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
        return score+((float)score)/getDNAsize() ;
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
        
        a.getDNA().createRandom(5);

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
                listAction.Add(a.getData()); 
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
        if (energy >= limitEnergy)
        {
            energy = limitEnergy;
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

    public List<string> getListAction()
    {
        return listAction;
    }
    
    public float getBetterScore()
    {
        return score +((float)score) / getDNAsize() ;
    }
}



public class pgen : MonoBehaviour {
    // Use this for initialization

        // La map que parcourt la fourmi
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
    public GameObject fourmiQueue1;
    public GameObject fourmiQueue2;
    public Material Tile1Text;

    int nbPopulation = 500;
    int nbSelect =  50;
    float mutateLuck = 90;
    int nbGeneration = 10;
    int i = 0;
    int frame = 0;
    Vector3 lastPos1;
    Vector3 lastPos2;

    GameObject[,] Tiles = new GameObject[32,32];

    Fourmis[] pF= new Fourmis[500];

     
          
    
    Fourmis[] initiatePopulation()
    {
        Fourmis[] populationFourmis = new Fourmis[nbPopulation];

        for (int i = 0; i < nbPopulation; i++)
        {

            Fourmis a = new Fourmis(new DNA(label[Random.Range(0, 6)]));

            a.getDNA().createRandom(5);
            a.fullProg();
            populationFourmis[i]=a;

        }

        return populationFourmis;
    }

    void sortList(Fourmis[] populationFourmis)
    {
        for(int i= nbPopulation - 1; i>0;i--)
        {
            for(int j=0;j<i;j++)
            {
                if(populationFourmis[j].getBetterScore() < populationFourmis[j+1].getBetterScore())
                {
                    Fourmis tmp = populationFourmis[j+1];
                    populationFourmis[j+1] = populationFourmis[j];
                    populationFourmis[j] = tmp;
                }
            }
        }
    }

    void sortLastList(Fourmis[] populationFourmis)
    {
        for (int i = nbPopulation - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (populationFourmis[j].getScore() < populationFourmis[j + 1].getScore())
                {
                    Fourmis tmp = populationFourmis[j + 1];
                    populationFourmis[j + 1] = populationFourmis[j];
                    populationFourmis[j] = tmp;
                }
            }
        }
    }

    Fourmis[] selection(Fourmis[] populationFourmis)
    {
        Fourmis[] f = new Fourmis[nbSelect];
        for(int i=0;i<nbSelect;i++)
        {
            f[i] = populationFourmis[i];
        }
        return f;
    }

    Fourmis[] crossOver(Fourmis[] populationFourmis,Fourmis[] selectedList)
    {
        Fourmis [] l = new Fourmis[nbPopulation];
        

        for (int i = 0; i < 10; i++)
        {
            l[i]=(new Fourmis(selectedList[i].getDNA().clone()));
        }
        for (int i = 10; i < nbPopulation; i++)
        {
            l[i] = (selectedList[Random.Range(0, nbSelect)].crossOver(selectedList[Random.Range(0, nbSelect)]));
        }

        //sortList(l);

        return l;
    }

    void runPopulation(Fourmis[] populationFourmis)
    {
        for (int i = 0; i < nbPopulation; i++)
        {

            populationFourmis[i].fullProg();
        }
    }

    void mutate(Fourmis[] l)
    {

        for (int i = 10; i < nbPopulation; i++)
        {

            if (Random.Range(1, 100) < mutateLuck)
            {
                l[i] = l[i].mutate();
            }
        }
    }

    void Start () {

        //Random.InitState(3);        // Seed fonctionne pour 9 gene

        //Random.InitState(1);

        for(int i=0;i<32;i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (map[i, j] == 0)
                {
                    GameObject newTile = Instantiate(mapTile);
                    newTile.transform.position = new Vector3(i, j);
                    Tiles[i, j] = newTile;
                }
                else
                {
                    GameObject newTile = Instantiate(mapTile1);
                    newTile.transform.position = new Vector3(i, j);
                    Tiles[i, j] = newTile;
                }
            }

        }
        //List<Fourmis> populationFourmis = new List<Fourmis>();

        Fourmis[] populationFourmis = new Fourmis[nbPopulation];
         populationFourmis = initiatePopulation();
        sortList(populationFourmis);

        
         for (int i = 0; i < nbGeneration; i++)
         {
             sortList(populationFourmis);
            populationFourmis =crossOver(populationFourmis, selection(populationFourmis));
             mutate(populationFourmis);
             runPopulation(populationFourmis);
            
        }

         sortLastList(populationFourmis);
         for(int i=0;i<nbPopulation;i++)
         {

          //  Debug.Log(populationFourmis[i].getScore());
         }
         

        


       pF= populationFourmis;
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

        //pF.Add(new Fourmis(f214));
        //pF[0].fullProg();
        DNA f228 = new DNA("if");
        f228.createRandom(3);

        Fourmis f1 = new Fourmis(f214);
        Fourmis f2 = new Fourmis(f228);
        f1.crossOver(f2);
        */

    }

    void Update()
    {

        //pF est la population de fourmis a recuperer !
        // pF[0] correspond au meilleur des fourmis

        // getListAction = liste des actions realise par la fourmi

        // move => On bouge d'une unite, direction l'orientation de la fourmi
        // right => On rotate de la droite
        // left => On rotate de la gauche


        if (i < pF[0].getPos().Count && frame%1==0 )
        {
            if(i == 0)
            {
                lastPos1 = new Vector3(pF[0].getPos()[i].x, pF[0].getPos()[i].y, -1);
                lastPos2 = new Vector3(pF[0].getPos()[i].x, pF[0].getPos()[i].y, -1);
            }
            else if(i == 1)
            {
                lastPos1 = new Vector3(pF[0].getPos()[i-1].x, pF[0].getPos()[i-1].y, -1);
                lastPos2 = new Vector3(pF[0].getPos()[i-1].x, pF[0].getPos()[i-1].y, -1);
            }
            else
            {
                lastPos1 = new Vector3(pF[0].getPos()[i - 1].x, pF[0].getPos()[i - 1].y, -1);
                lastPos2 = new Vector3(pF[0].getPos()[i - 2].x, pF[0].getPos()[i - 2].y, -1);
            }

            fourmi.transform.position = new Vector3(pF[0].getPos()[i].x, pF[0].getPos()[i].y, -1);
            fourmiQueue1.transform.position = lastPos1;
            fourmiQueue2.transform.position = lastPos2;

            if (map[(int)fourmi.transform.position.x, (int)fourmi.transform.position.y] == 1)
            {
               // Tiles[(int)fourmi.transform.position.x, (int)fourmi.transform.position.y].GetComponent<Renderer>().material.color = Color.blue;
                Tiles[(int)fourmi.transform.position.x, (int)fourmi.transform.position.y].GetComponent<Renderer>().material = Tile1Text;
            }

            if(i == 0)
            {
                fourmi.SetActive(true);
                fourmiQueue1.SetActive(true);
                fourmiQueue2.SetActive(true);
            }
            //Instantiate(fourmi);
            //Debug.Log("asa");
            i++;
        }
        frame++;
    }
  
}
