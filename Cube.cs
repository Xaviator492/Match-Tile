using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    

    //if u don't specity an access modifier, it's access level is private by default
    public float SpeedCamel; //is same as saying "private float speed;"
    //u can change the values of most of the variables from inspector if public

    //rite. lets follow C# coding standards tho. microsoft sez:
    // ok wait.   ThisIsPascalCase, thisIsCamelCase
    //public members of the class are always PascalCase. private members are always camelCase ok
    //class and method names are always PascalCase ok? yeee. nao modify the above declaration to mek it rite coding standard y camel?

    //public variables can be accessed by other scripts too, private can't be. lets mek it public yee. do it

    //I'll write here.

        //deriving from monobehaviour gives us access to Methods that unity provides. Start(), Update(), Awake() are the commonly used one

        //Awake runs first, followed by OnEnable and Start(), then Update() is automatically called at the start of every frame. reeds? y u afkyee


    //things u wanna do once at start, like Initialization etc can be done in Awake or Start 

    // Start is called before the first frame update
    void Start()
    {
        //lets set the position to (0, 2, 0) at start

       // to tell the compiler that it's float and not doubleoic
       //"transform" variable is a default reference to the object's Transform componentok. Mind the lowercase and uppercase. C# is case sensitive

        //so nao u know what "transform" refers to, nao u can use it to access position, rotation and scale parameters of this object

        //transform.position is of type Vector3. Vector3 is a struct. it is similar to classes but cant be extended, and takes up less memory. Don't worry about struct vs class rite nao
        // so u create a new Vector3 object and set transform.position value to that object

        //set scale to 2 2 2 nao, when unsure wat it's called, use intellisens

        //localScale is it's local scale relative to the parent gameobject, since we don't have a parent gameobject rite nao, localScale and lossyScale 
        //would be same here. Ctrl + S to save and go bak 2 Unity

        transform.position = new Vector3(0f, 2f, 0f);
        transform.localScale = new Vector3(2f, 2f, 2f);
        SpeedCamel = 7.5f;
    }

    //nao we kno hao 2 setup things in Start, do u need comments? older comments ok ok


    // Update is called once per frame
    void Update()
    {
        //lets make the cube move up
        //try to type something that will make cube go up (hint: change position) 
        //Update runs everyframe, so don't worry about creating your own loop
        // yee check error and try 2 fix

        //gud for a start. Nao let me rwrite yer code to mek it simpler
        //you can also "read" from the variables 

        //nao this still has a problem. the speed is very high. You can try and make it a smaller number like +0.01

        //but it's bad practice. cuz more fps = more movement cuz we do this every frame like samp lol bai
        //to fix this we need to use Time class

        //Time.deltaTime is the time in seconds that it took to calculate and render the last frame.
        //nao if u want to add 1 unit to the Y position of the object. adding 1f *Time.deltaTime means that the object goes up by 1f in 1 second slow
        //ye

        //nao les create a Speed variable

        //modify the code below so that it goes speed units up instead of 1 unit up ok yeee


        //nao lets mek Keyboard controls
        //unity has Input class to read Input.
        // this will check if UpArrow was pressed, but it returns a bool for result
        //so lets use an if statement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //nao it will only move up if UpArrow key is pressed r u press UP? no
            transform.position = new Vector3(transform.position.x, transform.position.y + SpeedCamel * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //nao it will only move up if DownyeeArrow key is pressed r u press UP? no
            transform.position = new Vector3(transform.position.x, transform.position.y - SpeedCamel * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //nao it will only move up if LeftArrow key is pressed r u press UP? no
            transform.position = new Vector3(transform.position.x - SpeedCamel * Time.deltaTime, transform.position.y, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //nao it will only move up if RightArrow key is pressed r u press UP? no
            transform.position = new Vector3(transform.position.x + SpeedCamel * Time.deltaTime, transform.position.y, 0f);
        }


        if(transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0f);
        }

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, 4f, 0f);
        }


        if (transform.position.x > 4)
        {
            transform.position = new Vector3(-4f, transform.position.y, 0f);
        }

        if (transform.position.x < -4)
        {
            transform.position = new Vector3(4f, transform.position.y, 0f);
        }

        //nao mek it same but opposite for down
        //coool, nao rite the code to Move it down when DownArrow is pressed, lets see if u can write ode for left right

        //you should be able to do it for left and right too. we mek snek yeeee. when is submission deadline for match 3 game?
        //2moz week, nek week? yee wat day yeee
        /*
        hao hahrd is the candy crush gaem? U need 2 lern collision and arrays,yee ok
        hao many days r u free 2 teech? i r no jabs rite nao. boston truedeau might giv me a project this week. so free until then (y); ok
        //nao write code so that if Y position is greater than 4 units, it sets it to -4

        //yeee

        //lets teech u 2d

        mek sure to Ctrl S, the go bak 2 unity andrestart game y u no let me moves? u assthe thing is set to default position.

        can u photoshop? yee. ok create a white 32x32 square. what format? jpg or png. but i need it to be completely hite pixels bai lel
        lel y? cuz we werk on yer game

        //drag and drop to Unity Project. but create a folder in project tab and name it Images or Sprites ok
        //ok

        //r they sez mek it for android? i czech it doesnt say. ok we assume it to be android cuz all games on adnroid or ios. candy crsuh etc
        , bai resize to 16x16 in PS open it, u didn't change the file in Unity
    */
    }


}
