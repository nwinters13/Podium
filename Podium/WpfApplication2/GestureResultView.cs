//------------------------------------------------------------------------------
// <copyright file="GestureResultView.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WpfApplication2
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Stores discrete gesture results for the GestureDetector.
    /// Properties are stored/updated for display in the UI.
    /// </summary>
    public sealed class GestureResultView : INotifyPropertyChanged
    {
        /// <summary> The body index (0-5) associated with the current gesture detector </summary>
        private int bodyIndex = 0;

        /// <summary> Current confidence value reported by the discrete gesture </summary>
        private float confidence = 0.0f;

        /// <summary> True, if the discrete gesture is currently being detected </summary>
        private bool detected = false;
        private int num_touchingface = 0;
        private int num_leaning = 0;
        private int num_crossing = 0;
        private int num_pointing = 0;
        private int num_tilting = 0;
        private int num_frames = 0;
        private float percent_touchingface = 0.0f;
        private float percent_leaning = 0.0f;
        private float percent_crossing = 0.0f;
        private float percent_pointing = 0.0f;
        private float percent_tilting = 0.0f;
        private float overall_score = 0.0f;
        public int num_gestures = 0;
        public int num_current_gestures = 0;

        /// <summary> True, if the body is currently being tracked </summary>
        private bool isTracked = false;

        /// <summary>
        /// Initializes a new instance of the GestureResultView class and sets initial property values
        /// </summary>
        /// <param name="bodyIndex">Body Index associated with the current gesture detector</param>
        /// <param name="isTracked">True, if the body is currently tracked</param>
        /// <param name="detected">True, if the gesture is currently detected for the associated body</param>
        /// <param name="confidence">Confidence value for detection of the 'Seated' gesture</param>
        public GestureResultView(int bodyIndex, bool isTracked, bool detected, float confidence)
        {
            this.BodyIndex = bodyIndex;
            this.IsTracked = isTracked;
            this.Detected = detected;
            this.Num_TouchingFace = 0;
            this.Num_Leaning = 0;
            this.Num_Crossing = 0;
            this.Num_Pointing = 0;
            this.Num_Tilting = 0;
            this.Percent_Crossing = 0;
            this.Percent_Tilting = 0;
            this.Percent_Leaning = 0;
            this.Percent_Pointing = 0;
            this.Percent_TouchingFace = 0;
            this.Num_Frames = 0;
            this.Confidence = confidence;
            this.Overall_Score = 0;
            this.num_gestures = 0;
            this.num_current_gestures = 0;
        }

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> 
        /// Gets the body index associated with the current gesture detector result 
        /// </summary>
        public int BodyIndex
        {
            get
            {
                return this.bodyIndex;
            }

            private set
            {
                if (this.bodyIndex != value)
                {
                    this.bodyIndex = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        /// <summary> 
        /// Gets a value indicating whether or not the body associated with the gesture detector is currently being tracked 
        /// </summary>
        public bool IsTracked 
        {
            get
            {
                return this.isTracked;
            }

            private set
            {
                if (this.IsTracked != value)
                {
                    this.isTracked = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets a value indicating whether or not the discrete gesture has been detected
        /// </summary>
        public bool Detected 
        {
            get
            {
                return this.detected;
            }

            private set
            {
                if (this.detected != value)
                {
                    this.detected = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_TouchingFace
        {
            get
            {
                return this.num_touchingface;
            }

            private set
            {
                if (this.num_touchingface != value)
                {
                    this.num_touchingface = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_Leaning
        {
            get
            {
                return this.num_leaning;
            }

            private set
            {
                if (this.num_leaning != value)
                {
                    this.num_leaning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_Crossing
        {
            get
            {
                return this.num_crossing;
            }

            private set
            {
                if (this.num_crossing != value)
                {
                    this.num_crossing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_Pointing
        {
            get
            {
                return this.num_pointing;
            }

            private set
            {
                if (this.num_pointing != value)
                {
                    this.num_pointing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_Tilting
        {
            get
            {
                return this.num_tilting;
            }

            private set
            {
                if (this.num_tilting != value)
                {
                    this.num_tilting = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Percent_TouchingFace
        {
            get
            {
                return (int)this.percent_touchingface;
            }

            private set
            {
                if (this.percent_touchingface != value)
                {
                    this.percent_touchingface = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Percent_Leaning
        {
            get
            {
                return (int)this.percent_leaning;
            }

            private set
            {
                if (this.percent_leaning != value)
                {
                    this.percent_leaning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Percent_Crossing
        {
            get
            {
                return (int)this.percent_crossing;
            }

            private set
            {
                if (this.percent_crossing != value)
                {
                    this.percent_crossing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Percent_Pointing
        {
            get
            {
                return (int)this.percent_pointing;
            }

            private set
            {
                if (this.percent_pointing != value)
                {
                    this.percent_pointing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Percent_Tilting
        {
            get
            {
                return (int)this.percent_tilting;
            }

            private set
            {
                if (this.percent_tilting != value)
                {
                    this.percent_tilting = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public float Overall_Score
        {
            get
            {
                return (int)this.overall_score;
            }

            private set
            {
                if (this.overall_score != value)
                {
                    this.overall_score = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Num_Frames
        {
            get
            {
                return this.num_frames;
            }

            private set
            {
                if (this.num_frames != value)
                {
                    this.num_frames = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets a float value which indicates the detector's confidence that the gesture is occurring for the associated body 
        /// </summary>
        public float Confidence
        {
            get
            {
                return this.confidence;
            }

            private set
            {
                if (this.confidence != value)
                {
                    this.confidence = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Updates the values associated with the discrete gesture detection result
        /// </summary>
        /// <param name="isBodyTrackingIdValid">True, if the body associated with the GestureResultView object is still being tracked</param>
        /// <param name="isGestureDetected">True, if the discrete gesture is currently detected for the associated body</param>
        /// <param name="detectionConfidence">Confidence value for detection of the discrete gesture</param>
        public void UpdateGestureResult(bool isBodyTrackingIdValid, bool isGestureDetected, float detectionConfidence, int frames, int touchingface, int leaning, int crossing, int tilting, int pointing)
        {
            this.IsTracked = isBodyTrackingIdValid;
            this.Confidence = 0.0f;

            if (!this.IsTracked)
            {
                this.Detected = false;
            }
            else
            {
                this.Detected = isGestureDetected;
                this.Num_Frames = frames;
                this.Num_TouchingFace = touchingface;
                this.Num_Leaning = leaning;
                this.Num_Crossing = crossing;
                this.Num_Pointing = pointing;
                if (this.Num_Frames != 0)
                {
                    this.Percent_TouchingFace = 100 * ((float)this.Num_TouchingFace / (float)this.Num_Frames);
                    this.Percent_Leaning = 100 * ((float)this.Num_Leaning / (float)this.Num_Frames);
                    this.Percent_Crossing = 100 * ((float)this.Num_Crossing / (float)this.Num_Frames);
                    this.Percent_Tilting = 100 * ((float)this.Num_Tilting / (float)this.Num_Frames);
                    this.Percent_Pointing = 100 * ((float)this.Num_Pointing / (float)this.Num_Frames);

                    this.Overall_Score = 100 - ((2 * this.Percent_Leaning + 3 * this.Percent_TouchingFace + 1 * this.Percent_Tilting + 2 * this.Percent_Crossing + 3 * this.Percent_Pointing) / 2);
                }

            }
        }

        /// <summary>
        /// Notifies UI that a property has changed
        /// </summary>
        /// <param name="propertyName">Name of property that has changed</param> 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void setNumGestures(int num)
        {
            this.num_gestures = num;
        }

        public void UpdateCurrentGestureCount(int num)
        {
           // this.numCurrentGestures = num;
        }
    }
}

