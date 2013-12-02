using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Touchless.Vision.Camera;

namespace WiredIn.Visualization.View
{
    public partial class MirrorView : AbstractView
    {
        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        List<Camera> lstCameras = new List<Camera>();
        Camera activeCamera;
        
        public MirrorView()
        {
            InitializeComponent();
        }

        public override void SetUp()
        {
                // Refresh the list of available cameras
                lstCameras.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    lstCameras.Add(cam);

                if (lstCameras.Count > 0)
                    activeCamera = lstCameras[0];
            
        }

        public void StartMirror()
        {
            if (_frameSource != null && _frameSource.Camera == activeCamera)
                return;

            thrashOldCamera();
            startCapturing();
        }

        public void StopMirror()
        {
            thrashOldCamera();
        }

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        private void startCapturing()
        {
            try
            {
                Camera c = activeCamera;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 320;
                _frameSource.Camera.CaptureHeight = 240;
                _frameSource.Camera.Fps = 60;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        public override void MoveView(bool goToGood)
        {
            if (goToGood)
            {
                StopMirror();
            }
            else
            {
                StartMirror();
            }
        }
    }
}
