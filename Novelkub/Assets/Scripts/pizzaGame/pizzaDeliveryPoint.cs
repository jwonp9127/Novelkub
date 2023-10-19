//using system.collections;
//using system.collections.generic;
//using unityengine;
//using unityengine.ui;
//public tmpro;

//public class pizzadeliverypoint : monobehaviour
//{
//    audiosource audio;
//    public pizzagamemanager manager;
//    private float gameduration = 180.0f;
//    public tmp_text timertext;

//    void awake()
//    {
//        audio = getcomponent<audiosource>();
//    }

//    private void start()
//    {
//        updatetimertext();
//    }

//    private void update()
//    {
//        if (gameduration > 0.0f)
//        {
//            gameduration -= time.deltatime;
//            if (gameduration <= 0.0f)
//            {
//                //restartquest();
//            }
//            updatetimertext();
//        }
//    }

//    private void ontriggerenter(collider other)
//    {
//        if(other.tag == "point")
//        {
//            pointcount++;
//            audio.play();
//            other.gameobject.setactive(false);
//            if (pointcount == manager.totalpointcount)
//            {
//                //game clear
//            }
//            else 
//            { 
//                //restartqeuest
//            }
//        }
//    }

//    private void updatetimertext()
//    {
//        int minutes = mathf.floortoint(gameduration / 60);
//        int seconds = mathf.floortoint(gameduration - minutes * 60);
//        timertext.text = string.format.format("{0:00}:{1:00}", minutes, seconds);
//    }
//}
