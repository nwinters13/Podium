
/*
/   The interview window handles all of the during interview programming
/       It contains a lot of code including pause functionality, the gesture detections, and audio
*/

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.ComponentModel;
using Microsoft.Kinect;
using System.Windows.Threading;
    

namespace WpfApplication2
{
        /// <summary>
        /// Interaction logic for Window1.xaml
        /// </summary>
        public partial class InterviewWindow : Window, INotifyPropertyChanged
        {

            public string user_email;

            /// <summary> Active Kinect sensor </summary>
            private KinectSensor kinectSensor = null;

            /// <summary> Array for the bodies (Kinect will track up to 6 people simultaneously) </summary>
            private Body[] bodies = null;

            /// <summary> Reader for body frames </summary>
            private BodyFrameReader bodyFrameReader = null;

            /// <summary> Current status text to display </summary>
            private string statusText = null;

            // Questions array
            //  TODO: Retrieve from a database
            private string[] questions = new string[] { "Tell me a bit about yourself.", "Tell me about your educational background.", "What are your weaknesses?", "Describe yourself using 3 words.", "How would your friends describe you?" };
            private int question_index;

            /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
            private GestureDetector gestureDetector = null;

            private MediaPlayer mplayer;

            // Pause functionality
            private DispatcherTimer timer;
            private int counter = 0;
            private bool paused = false;

            // values for feedback shading
            public int step = 2;
            public int current_val = 0;
            public int next_val = 0;

            // Settings variables
            public bool isPauseEnabled = true;
            public bool isFeedbackEnabled = true;

            public InterviewWindow()
            {
                question_index = 0;

                mplayer = new MediaPlayer();
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

                user_email = "";

                // only one sensor is currently supported
                this.kinectSensor = KinectSensor.GetDefault();
                // set IsAvailableChanged event notifier
                this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;
                // open the sensor
                this.kinectSensor.Open();
                System.Diagnostics.Debug.WriteLine(this.kinectSensor.IsAvailable);
                // set the status text
                this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                                : Properties.Resources.NoSensorStatusText;

                // open the reader for the body frames
                this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

                // set the BodyFramedArrived event notifier
                this.bodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;
            
                // initialize the MainWindow
                this.InitializeComponent();

                // set our data context objects for display in UI

                // create a gesture detector for each body (6 bodies => 6 detectors) and create content controls to display results in the UI
                GestureResultView result = new GestureResultView(0, false, false, 0.0f);
                GestureDetector detector = new GestureDetector(this.kinectSensor, result);
                this.gestureDetector = detector;
                play_audio(question_index);
            }

            // This listener handles all of the pause functionality
            private void Window_KeyDown(object sender, KeyEventArgs e)
            {
                if(e.Key == Key.Space && this.isPauseEnabled)
                {
                    if (!paused)
                    {
                        // Pause code
                        var blur = new BlurEffect();
                        var current = this.Background;
                        if (this.isFeedbackEnabled)
                        {
                            this.Background = new SolidColorBrush(Colors.DarkGray);
                        }
                        this.Effect = blur;
                        popup.Visibility = System.Windows.Visibility.Visible;
                        popup.IsOpen = true;
                        timer.IsEnabled = false;
                        paused = true;
                    }
                    else
                    {
                        // Unpause code
                        this.Effect = null;
                        popup.IsOpen = false;
                        timer.IsEnabled = true;
                        paused = false;
                        this.Background = new SolidColorBrush(Colors.White);
                    }
                }
            }

            // The timer tickets in order to keep track of pausing
            void timer_Tick(object sender, EventArgs e)
            {
                counter++;
            }

            /// <summary>
            /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Gets or sets the current status text to display
            /// </summary>
            public string StatusText
            {
                get
                {
                    return this.statusText;
                }

                set
                {
                    if (this.statusText != value)
                    {
                        this.statusText = value;

                        // notify any bound elements that the text has changed
                        if (this.PropertyChanged != null)
                        {
                            this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                        }
                    }
                }
            }

            /// <summary>
            /// Execute shutdown tasks
            /// </summary>
            /// <param name="sender">object sending the event</param>
            /// <param name="e">event arguments</param>
            private void MainWindow_Closing(object sender, CancelEventArgs e)
            {
                if (this.bodyFrameReader != null)
                {
                    // BodyFrameReader is IDisposable
                    this.bodyFrameReader.FrameArrived -= this.Reader_BodyFrameArrived;
                    this.bodyFrameReader.Dispose();
                    this.bodyFrameReader = null;
                }

                if (this.gestureDetector != null)
                {
                    // The GestureDetector contains disposable members (VisualGestureBuilderFrameSource and VisualGestureBuilderFrameReader)
                    this.gestureDetector.Dispose();
                }

                if (this.kinectSensor != null)
                {
                    System.Diagnostics.Debug.WriteLine("wabba");
                    this.kinectSensor.IsAvailableChanged -= this.Sensor_IsAvailableChanged;
                    System.Diagnostics.Debug.WriteLine("daw");
                    this.kinectSensor.Close();
                    this.kinectSensor = null;
                }
            }

            /// <summary>
            /// Handles the event when the sensor becomes unavailable (e.g. paused, closed, unplugged).
            /// </summary>
            /// <param name="sender">object sending the event</param>
            /// <param name="e">event arguments</param>
            private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine(this.kinectSensor.IsAvailable);
                // on failure, set the status text
                this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                                : Properties.Resources.SensorNotAvailableStatusText;
            }

            /// <summary>
            /// Handles the body frame data arriving from the sensor and updates the associated gesture detector object for each body
            /// </summary>
            /// <param name="sender">object sending the event</param>
            /// <param name="e">event arguments</param>
            private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
            {
                bool dataReceived = false;

                using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        if (this.bodies == null)
                        {
                            // creates an array of 6 bodies, which is the max number of bodies that Kinect can track simultaneously
                            this.bodies = new Body[bodyFrame.BodyCount];
                        }

                        // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                        // As long as those body objects are not disposed and not set to null in the array,
                        // those body objects will be re-used.
                        bodyFrame.GetAndRefreshBodyData(this.bodies);
                        dataReceived = true;
                    }
                }

                if (dataReceived)
                {

                    // we may have lost/acquired bodies, so update the corresponding gesture detectors
                    if (this.bodies != null)
                    {
                        // loop through all bodies to see if any of the gesture detectors need to be updated
                        int maxBodies = this.kinectSensor.BodyFrameSource.BodyCount;
                        for (int i = 0; i < maxBodies; ++i)
                        {
                            Body body = this.bodies[i];
                            ulong trackingId = body.TrackingId;

                            // if the current body TrackingId changed, update the corresponding gesture detector with the new value
                            if (trackingId != this.gestureDetector.TrackingId && body.IsTracked)
                            {
                                this.gestureDetector.TrackingId = trackingId;

                                // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                                // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                                this.gestureDetector.IsPaused = trackingId == 0;
                            }
                        }
                    }
                    
                    // This code sets the shading for the feedback
                    int num_detected = this.gestureDetector.num_gestures_detected;
                    float ratio = (float)this.gestureDetector.num_gestures_detected / (float)this.gestureDetector.num_gestures;
                    System.Diagnostics.Debug.WriteLine("Num detected: " + this.gestureDetector.num_gestures_detected);
                    System.Diagnostics.Debug.WriteLine("Num total: " + this.gestureDetector.num_gestures);
                    int new_val = ((int)(ratio * 255.0));
                    next_val = new_val;
                    // Calculates what the change in shading should be
                    if (next_val >= current_val + step)
                    {
                        current_val += step;   
                    }
                    else if (next_val > current_val)
                    {
                        current_val = next_val;
                    }
                    else if (next_val <= current_val - step)
                    {
                        current_val -= step;
                    }
                    else if (next_val < current_val)
                    {
                        current_val = next_val;
                    }
                
                    Brush brush = new SolidColorBrush(Color.FromArgb((byte)current_val, 255, 0, 0));
                    if (this.isFeedbackEnabled)
                    {
                        // Only changes the fill if feedback is enabled
                        this.rect_feedback.Fill = brush;
                    }
                }
            }


        // This function acts to end the interviews and pass the necessary values over to the score screen
        // It initializes the next windows and sets it to the necessary size
        private void button_end_interview_click(object sender, RoutedEventArgs e)
        {
            SelfscoreWindow window = new SelfscoreWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.user_email = this.user_email;

            string gesture_name = "";

            int[] gestureArray = { this.gestureDetector.num_frames_leaning, this.gestureDetector.num_frames_touchingface, this.gestureDetector.num_frames_crossing, this.gestureDetector.num_frames_pointing, this.gestureDetector.num_frames_tilting };

            int maxValue = gestureArray.Max();
            int maxIndex = gestureArray.ToList().IndexOf(maxValue);

            // Provides the text for the proper worst gesture
            switch (maxIndex)
            {
                case 0:
                    gesture_name = "lean forward!";
                    break;
                case 1:
                    gesture_name = "touch your face!";
                    break;
                case 2:
                    gesture_name = "cross your arms!";
                    break;
                case 3:
                    gesture_name = "point at something!";
                    break;
                case 4:
                    gesture_name = "tilt your head!";
                    break;
                default:
                    gesture_name = "default";
                    break;
            }

            // Sets the score and the worst gesture in the next window
            window.setScore(this.gestureDetector.GestureResultView.Overall_Score);
            window.setWorstGesture(gesture_name);

            
            window.Show();
            // Test code to try to fix the weird Kinect issue with resetting
            //this.MainWindow_Closing(this, null);
            //this.kinectSensor.IsAvailableChanged =;
            this.Close();
        }

        // Handles moving to the next question, including playing the audio file of the quesiton
        private void button_next_question_click(object sender, RoutedEventArgs e)
        {
            question_index++;
            if (question_index == questions.Length)
            {
                // Automatically ends the interview (by programmatically clicking end interview to properly reuse code
                this.button_end_interview_click(this, null);
            }
            else
            {
                // We could also overlap if desired
                //question_index = question_index % questions.Length;
                this.label.Content = questions[question_index];
                play_audio(question_index);
            }

        }

        // Code to choose the proper audio file to play based on which question
        //  TODO: Make an array and programmatically set the text path to allow for more questions
        private void play_audio(int question_index)
        {
            if(question_index == 0)
            {
                mplayer.Open(new Uri(@"../../Audio/hobbies.mp3", UriKind.Relative));
                mplayer.Play();
            }
            else if(question_index == 1)
            {
                mplayer.Open(new Uri(@"../../Audio/education.mp3", UriKind.Relative));
                mplayer.Play();
            }
            else if (question_index == 2)
            {
                mplayer.Open(new Uri(@"../../Audio/weakness.mp3", UriKind.Relative));
                mplayer.Play();
            }
            else if (question_index == 3)
            {
                mplayer.Open(new Uri(@"../../Audio/three.mp3", UriKind.Relative));
                mplayer.Play();
            }
            else if (question_index == 4)
            {
                mplayer.Open(new Uri(@"../../Audio/friends.mp3", UriKind.Relative));
                mplayer.Play();
            }
        }
    }
}
