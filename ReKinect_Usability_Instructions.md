Before trying to run our application, make sure that your computer is
running 64-bit Windows 8 or higher, and that you have downloaded the
[Kinect SDK](https://www.microsoft.com/en-us/download/details.aspx?id=44561)
to ensure you have all the required Kinect software for the application
to run properly.

To download our application, download this repository and extract it anywhere
in your computer. Open the folder, then navigate into 
`Podium/WpfApplication2/bin/Debug`. The application is in there under the name
`WpfApplication2`. You can run the program from there, or right-click on the
application, click 'Create Shortcut', and place the shortcut anywhere else
(like your Desktop) to run it. We recommend renaming the shortcut to "Podium for
Kinect"!

To learn how to use our application, follow the instructions below. We included
links to screenshots of our application throughout the instructions to help
ensure that you do not get lost following the instructions. You can also [follow
this link]
(https://drive.google.com/file/d/0ByQn2pncUkPhWFNia2tBcGRkV2c/view?usp=sharing) 
to see a video demonstration of the application. It’s worth noting that the 
screenshots and video are from an older version of the app, but it only differs 
aesthetically from our current program.

To use our application, the Kinect should be set up [behind and above the
computer]
(https://drive.google.com/file/d/0ByQn2pncUkPhaGRZcEFNaXZHWUk/view?usp=sharing), 
and the user should be seated at eye level around 5 feet away from the Kinect.

When the user starts the application, they will be presented with the [login 
screen]
(https://drive.google.com/file/d/0ByQn2pncUkPhS05ObDZRZzZNcEk/view?usp=sharing), 
and then after logging in, the [title screen]
(https://drive.google.com/file/d/0ByQn2pncUkPhVzlNbW1vWU1WM1E/view?usp=sharing). 
From the title screen, the user can toggle usability options, change the 
questions asked during the interview, view past results from previous uses of the 
application, and more importantly, start the program.

When the program is running, the user will see the [application screen]
(https://drive.google.com/file/d/0ByQn2pncUkPhdFlNTkVkR0NWN00/view?usp=sharing). 
The question will be presented in the middle of the screen, and the user answers
them as though they were in an actual interview. When finished answering a 
question, the user clicks the ‘Next Question’ button. The application will end 
when the user goes through all the questions or clicks the ‘End Interview’ 
button.

The Kinect will track the user movements and continuously generate a score based 
on the user's body language. The border around the screen will stay white when 
the user displays good body language, and fade slowly more red as the user 
displays bad body language (in the video, the border will turn yellow when Matt 
covers his face, and then red when Matt covers his face and leans too far over - 
our previous iteration featured a green-yellow-red transition instead of a 
white-faint red-darker red one implemented now). The [score value presented at the 
end]
(https://drive.google.com/file/d/0ByQn2pncUkPhNTR5UUVMSWJRTGM/view?usp=sharing) 
is calculated as the app runs, and is calculated based on weights given to 
the different bad gestures detected. Higher scores are better, with 100 being the 
best score possible.

