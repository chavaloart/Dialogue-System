# New Dialogue System in Unity

![](https://i.imgur.com/1Wupnmm.gif)

The original project belongs to Brackeys! I simply altered it with the goal of expanding upon it and making it easy for begginer programmers or artists.

The project only had one name you can input. Because of that, I added a names array and you can now input a name for each dialogue line there is.
I also added the possibility to run a function on the beggining of each dialogue line. This was done by adding an array of Unity Events.

![](https://i.imgur.com/JVOulgJ.gif)

In addition to that, and most importantly for the goal I had in mind, I changed how the Inspector shows the Dialogue class when it is Serialized.
I did this by using a Custom Editor, and it now shows one dialogue line at a time, with the name coming first, then the sentences and finally the event, instead of all the names, all the sentences and all the events.

If you would rather have the old Editor, you can delete the Editor folder.

The addition of different arrays, in the way it was implemented, required all the arrays to have the same size, and so having a custom inspector helps prevent the use of different sized arrays by applying the same size to all of them.


![](https://i.imgur.com/fhI208X.png)

# Dialogue System in Unity
Project files for a tutorial on creating a Dialogue System in Unity

Everything is made using Unity.

Check out my [YouTube Channel](http://youtube.com/brackeys) for more tutorials.

Everything is free to use also commercially (public domain).
