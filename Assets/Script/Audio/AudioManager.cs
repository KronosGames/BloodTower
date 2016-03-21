namespace Audio
{
     public enum BGMID
     {
         BGM,  //<BGM
     }

     public enum SEID
     {
         CLOSE,  //<close
         DAMAGE,  //<Damage
         DECISION,  //<Decision
         OPEN,  //<open
     }
}

public class BGMAudioData
{
     public string label;
     public Audio.BGMID id;
}

public class SEAudioData
{
     public string label;
     public Audio.SEID id;
}
public class AudioManager
{
     static public SEAudioData []SEData = new SEAudioData[]
     {
         new SEAudioData(){label ="close",         id = Audio.SEID.CLOSE}, 
         new SEAudioData(){label ="Damage",         id = Audio.SEID.DAMAGE}, 
         new SEAudioData(){label ="Decision",         id = Audio.SEID.DECISION}, 
         new SEAudioData(){label ="open",         id = Audio.SEID.OPEN}, 
     };

     static public BGMAudioData []BGMData = new BGMAudioData[]
     {
         new BGMAudioData(){label ="BGM",          id = Audio.BGMID.BGM}, 
     };
}
