using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenChore.Extensions;
using OpenChore.Models;
using OpenChore.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using XFUtils;

namespace OpenChore.Pages
{
    public partial class ChildChorePage : ContentPage
    {

        SKBitmap resourceBitmap;

        SKPath keyholePath = SKPath.ParseSvgPathData(
            //        "M 300 130 L 250 350 L 450 350 L 400 130 A 70 70 0 1 0 300 130 Z");
            "M445,0H820.632V118.389s-95.085,18.867-188.993,18.867S445,118.389,445,118.389Z");

        public ChildChorePage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.MainViewModel;
            weightSlider.ValueChanged += MySlider_ValueChanged;
            posSlider.ValueChanged += MySlider_ValueChanged;

            string resourceID = "OpenChore.Images.ChoreBackground.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                resourceBitmap = SKBitmap.Decode(stream);
            }

            scrollEffect = new XFUtils.Effects.ScrollReporterEffect();
            ChoreList.Effects.Add(scrollEffect);
            scrollEffect.ScrollChanged += ScrollEffect_ScrollChanged;

        }

        XFUtils.Effects.ScrollReporterEffect scrollEffect;

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        //float curveMin = 200;
        //float curveMax = 250;
        //float curveOffset = 250;

        float curveAttractor;

        double ProfileExpandedX;
        double ProfileExpandedY;
        double ScrollDistance = 100;
        double YStep;
        double XStep;
        double ScaleStep;

        float minHeaderHeight = 50;
        float currentHeaderHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            base.OnSizeAllocated(width, height);
            ProfileExpandedX = width / 2 - ProfileLogo.Width / 2;
            ProfileExpandedY = 50;

            YStep = ProfileExpandedY / ScrollDistance;
            XStep = ProfileExpandedX / ScrollDistance;
            ScaleStep = 1 / ScrollDistance;

        }

        void ScrollEffect_ScrollChanged(object arg1, XFUtils.Effects.ScrollEventArgs arg2)
        {
            PositionElements(arg2.Y);
        }



        private void PositionElements(double yPos)
        {
            // calculate x pos
            ProfileLogo.TranslationX = Math.Min(Math.Max(ProfileExpandedX - yPos * XStep, 10), ProfileExpandedX);
            ProfileLogo.TranslationY = Math.Max(ProfileExpandedY - yPos * YStep, 0);
            ProfileLogo.Scale = Math.Min(Math.Max(2 - yPos * ScaleStep, 1), 2);

            // calculate header pos based on yPos
            currentHeaderHeight = (float)Math.Max(minHeaderHeight, SkiaCanvas.Height - yPos);

            SkiaCanvas.InvalidateSurface();
            // change header curve
            //float calccurveOffset = (float)Math.Max(curveMax - yPos, curveMin);
            //if (calccurveOffset > 250)
            //    calccurveOffset = 250;
            //if (calccurveOffset != curveOffset)
            //{
            //    curveOffset = calccurveOffset;
            //    SkiaCanvas.InvalidateSurface();
            //}
        }


        void MySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SkiaCanvas.InvalidateSurface();
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var choreDefinition = e.Item as ChoreItemViewModel;

            if (choreDefinition != null)
                Navigation.PushAsync(new SwipeChorePage());
                //Navigation.PushAsync(new ChoreDetailsPage(choreDefinition), true);
        }       

        bool showFill = true;
void OnCanvasViewTapped(object sender, EventArgs args)
        {
            showFill ^= true;
            (sender as SKCanvasView).InvalidateSurface();
        }

        private float DensityConversion(float pixel, double size)
        {
            return pixel/ (float)size;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            float densityMultiplier = args.Info.Height / (float)SkiaCanvas.Height;
            float pixelHeight = currentHeaderHeight * densityMultiplier;
            float pullPoint = minHeaderHeight * densityMultiplier;
            canvas.Clear();

            SKPath path = new SKPath();
            SKPoint point0 = new SKPoint(0, pixelHeight);
            SKPoint point1 = new SKPoint(args.Info.Width / 2, pullPoint);
            SKPoint point2 = new SKPoint(args.Info.Width, pixelHeight);
            path.MoveTo(0, 0);
            path.LineTo(point0);
            path.ConicTo(point1, point2, (float)weightSlider.Value);
            path.LineTo(args.Info.Width, 0);
            path.LineTo(0, 0);
            path.Close();
            canvas.ClipPath(path, antialias: true);

            using (SKPaint paint = new SKPaint())
            {
                // Create bitmap tiling
                paint.Shader = SKShader.CreateBitmap(resourceBitmap,
                                                     SKShaderTileMode.Repeat,
                                                     SKShaderTileMode.Repeat);
                // Draw background
                canvas.DrawRect(info.Rect, paint);
            }


            //SKRect dest = new SKRect(0, 0, info.Width, info.Height);
            //canvas.DrawBitmap(resourceBitmap, dest, BitmapStretch.AspectFill, BitmapAlignment.Start, BitmapAlignment.Start);


        }
    }
}
