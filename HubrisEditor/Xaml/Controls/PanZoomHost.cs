using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using HubrisEditor.Utility;

namespace HubrisEditor.Xaml.Controls
{
    public class PanZoomHost : ContentControl, IScrollInfo
    {
        #region Dependency Property Definitions
        public static readonly DependencyProperty ContentScaleProperty = DependencyProperty.Register("ContentScale", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(1.0, ContentScale_PropertyChanged, ContentScale_Coerce));
        public static readonly DependencyProperty MinContentScaleProperty = DependencyProperty.Register("MinContentScale", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.01, MinOrMaxContentScale_PropertyChanged));
        public static readonly DependencyProperty MaxContentScaleProperty = DependencyProperty.Register("MaxContentScale", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(10.0, MinOrMaxContentScale_PropertyChanged));
        public static readonly DependencyProperty ContentOffsetXProperty = DependencyProperty.Register("ContentOffsetX", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0, ContentOffsetX_PropertyChanged, ContentOffsetX_Coerce));
        public static readonly DependencyProperty ContentOffsetYProperty = DependencyProperty.Register("ContentOffsetY", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0, ContentOffsetY_PropertyChanged, ContentOffsetY_Coerce));
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register("AnimationDuration", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.4));
        public static readonly DependencyProperty ContentZoomFocusXProperty = DependencyProperty.Register("ContentZoomFocusX", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty ContentZoomFocusYProperty = DependencyProperty.Register("ContentZoomFocusY", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty ViewportZoomFocusXProperty = DependencyProperty.Register("ViewportZoomFocusX", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty ViewportZoomFocusYProperty = DependencyProperty.Register("ViewportZoomFocusY", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty ContentViewportWidthProperty = DependencyProperty.Register("ContentViewportWidth", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty ContentViewportHeightProperty = DependencyProperty.Register("ContentViewportHeight", typeof(double), typeof(PanZoomHost), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty IsMouseWheelScrollingEnabledProperty = DependencyProperty.Register("IsMouseWheelScrollingEnabled", typeof(bool), typeof(PanZoomHost), new FrameworkPropertyMetadata(false));
        #endregion Dependency Property Definitions

        #region Dependency Property Accessors
        public double ContentOffsetX
        {
            get
            {
                return (double)GetValue(ContentOffsetXProperty);
            }
            set
            {
                SetValue(ContentOffsetXProperty, value);
            }
        }

        public event EventHandler ContentOffsetXChanged;

        public double ContentOffsetY
        {
            get
            {
                return (double)GetValue(ContentOffsetYProperty);
            }
            set
            {
                SetValue(ContentOffsetYProperty, value);
            }
        }

        public event EventHandler ContentOffsetYChanged;

        public double ContentScale
        {
            get
            {
                return (double)GetValue(ContentScaleProperty);
            }
            set
            {
                SetValue(ContentScaleProperty, value);
            }
        }

        public event EventHandler ContentScaleChanged;

        public double MinContentScale
        {
            get
            {
                return (double)GetValue(MinContentScaleProperty);
            }
            set
            {
                SetValue(MinContentScaleProperty, value);
            }
        }

        public double MaxContentScale
        {
            get
            {
                return (double)GetValue(MaxContentScaleProperty);
            }
            set
            {
                SetValue(MaxContentScaleProperty, value);
            }
        }

        public double ContentZoomFocusX
        {
            get
            {
                return (double)GetValue(ContentZoomFocusXProperty);
            }
            set
            {
                SetValue(ContentZoomFocusXProperty, value);
            }
        }

        public double ContentZoomFocusY
        {
            get
            {
                return (double)GetValue(ContentZoomFocusYProperty);
            }
            set
            {
                SetValue(ContentZoomFocusYProperty, value);
            }
        }

        public double ViewportZoomFocusX
        {
            get
            {
                return (double)GetValue(ViewportZoomFocusXProperty);
            }
            set
            {
                SetValue(ViewportZoomFocusXProperty, value);
            }
        }

        public double ViewportZoomFocusY
        {
            get
            {
                return (double)GetValue(ViewportZoomFocusYProperty);
            }
            set
            {
                SetValue(ViewportZoomFocusYProperty, value);
            }
        }

        public double AnimationDuration
        {
            get
            {
                return (double)GetValue(AnimationDurationProperty);
            }
            set
            {
                SetValue(AnimationDurationProperty, value);
            }
        }

        public double ContentViewportWidth
        {
            get
            {
                return (double)GetValue(ContentViewportWidthProperty);
            }
            set
            {
                SetValue(ContentViewportWidthProperty, value);
            }
        }

        public double ContentViewportHeight
        {
            get
            {
                return (double)GetValue(ContentViewportHeightProperty);
            }
            set
            {
                SetValue(ContentViewportHeightProperty, value);
            }
        }

        public bool IsMouseWheelScrollingEnabled
        {
            get
            {
                return (bool)GetValue(IsMouseWheelScrollingEnabledProperty);
            }
            set
            {
                SetValue(IsMouseWheelScrollingEnabledProperty, value);
            }
        }
        #endregion

        #region Zoom Pan Methods
        public void AnimatedZoomTo(double newScale, Rect contentRect)
        {
            AnimatedZoomPointToViewportCenter(newScale, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)),
                delegate(object sender, EventArgs e)
                {
                    this.ContentOffsetX = contentRect.X;
                    this.ContentOffsetY = contentRect.Y;
                });
        }

        public void AnimatedZoomTo(Rect contentRect)
        {
            double scaleX = this.ContentViewportWidth / contentRect.Width;
            double scaleY = this.ContentViewportHeight / contentRect.Height;
            double newScale = this.ContentScale * Math.Min(scaleX, scaleY);

            AnimatedZoomPointToViewportCenter(newScale, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)), null);
        }

        public void ZoomTo(Rect contentRect)
        {
            double scaleX = this.ContentViewportWidth / contentRect.Width;
            double scaleY = this.ContentViewportHeight / contentRect.Height;
            double newScale = this.ContentScale * Math.Min(scaleX, scaleY);

            ZoomPointToViewportCenter(newScale, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)));
        }

        public void SnapContentOffsetTo(Point contentOffset)
        {
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentOffsetX = contentOffset.X;
            this.ContentOffsetY = contentOffset.Y;
        }

        public void SnapTo(Point contentPoint)
        {
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentOffsetX = contentPoint.X - (this.ContentViewportWidth / 2);
            this.ContentOffsetY = contentPoint.Y - (this.ContentViewportHeight / 2);
        }

        public void AnimatedSnapTo(Point contentPoint)
        {
            double newX = contentPoint.X - (this.ContentViewportWidth / 2);
            double newY = contentPoint.Y - (this.ContentViewportHeight / 2);

            AnimationHelper.StartAnimation(this, ContentOffsetXProperty, newX, AnimationDuration);
            AnimationHelper.StartAnimation(this, ContentOffsetYProperty, newY, AnimationDuration);
        }

        public void AnimatedZoomAboutPoint(double newContentScale, Point contentZoomFocus)
        {
            newContentScale = Math.Min(Math.Max(newContentScale, MinContentScale), MaxContentScale);

            AnimationHelper.CancelAnimation(this, ContentZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ContentZoomFocusYProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusYProperty);

            ContentZoomFocusX = contentZoomFocus.X;
            ContentZoomFocusY = contentZoomFocus.Y;
            ViewportZoomFocusX = (ContentZoomFocusX - ContentOffsetX) * ContentScale;
            ViewportZoomFocusY = (ContentZoomFocusY - ContentOffsetY) * ContentScale;

            m_enableContentOffsetUpdateFromScale = true;

            AnimationHelper.StartAnimation(this, ContentScaleProperty, newContentScale, AnimationDuration,
                delegate(object sender, EventArgs e)
                {
                    m_enableContentOffsetUpdateFromScale = false;

                    ResetViewportZoomFocus();
                });
        }

        public void ZoomAboutPoint(double newContentScale, Point contentZoomFocus)
        {
            newContentScale = Math.Min(Math.Max(newContentScale, MinContentScale), MaxContentScale);

            double screenSpaceZoomOffsetX = (contentZoomFocus.X - ContentOffsetX) * ContentScale;
            double screenSpaceZoomOffsetY = (contentZoomFocus.Y - ContentOffsetY) * ContentScale;
            double contentSpaceZoomOffsetX = screenSpaceZoomOffsetX / newContentScale;
            double contentSpaceZoomOffsetY = screenSpaceZoomOffsetY / newContentScale;
            double newContentOffsetX = contentZoomFocus.X - contentSpaceZoomOffsetX;
            double newContentOffsetY = contentZoomFocus.Y - contentSpaceZoomOffsetY;

            AnimationHelper.CancelAnimation(this, ContentScaleProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentScale = newContentScale;
            this.ContentOffsetX = newContentOffsetX;
            this.ContentOffsetY = newContentOffsetY;
        }

        public void AnimatedZoomTo(double contentScale)
        {
            Point zoomCenter = new Point(ContentOffsetX + (ContentViewportWidth / 2), ContentOffsetY + (ContentViewportHeight / 2));
            AnimatedZoomAboutPoint(contentScale, zoomCenter);
        }

        public void ZoomTo(double contentScale)
        {
            Point zoomCenter = new Point(ContentOffsetX + (ContentViewportWidth / 2), ContentOffsetY + (ContentViewportHeight / 2));
            ZoomAboutPoint(contentScale, zoomCenter);
        }

        public void AnimatedScaleToFit()
        {
            if (m_content == null)
            {
                throw new ApplicationException("PART_Content was not found in the ZoomAndPanControl visual template!");
            }

            AnimatedZoomTo(new Rect(0, 0, m_content.ActualWidth, m_content.ActualHeight));
        }

        public void ScaleToFit()
        {
            if (m_content == null)
            {
                throw new ApplicationException("PART_Content was not found in the ZoomAndPanControl visual template!");
            }

            ZoomTo(new Rect(0, 0, m_content.ActualWidth, m_content.ActualHeight));
        }
        #endregion

        #region Internal Methods
        static PanZoomHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PanZoomHost), new FrameworkPropertyMetadata(typeof(PanZoomHost)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_content = this.Template.FindName("PART_Content", this) as FrameworkElement;
            if (m_content != null)
            {
                this.m_contentScaleTransform = new ScaleTransform(this.ContentScale, this.ContentScale);

                this.m_contentOffsetTransform = new TranslateTransform();
                UpdateTranslationX();
                UpdateTranslationY();

                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(this.m_contentOffsetTransform);
                transformGroup.Children.Add(this.m_contentScaleTransform);
                m_content.RenderTransform = transformGroup;
            }
        }

        private void AnimatedZoomPointToViewportCenter(double newContentScale, Point contentZoomFocus, EventHandler callback)
        {
            newContentScale = Math.Min(Math.Max(newContentScale, MinContentScale), MaxContentScale);

            AnimationHelper.CancelAnimation(this, ContentZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ContentZoomFocusYProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusYProperty);

            ContentZoomFocusX = contentZoomFocus.X;
            ContentZoomFocusY = contentZoomFocus.Y;
            ViewportZoomFocusX = (ContentZoomFocusX - ContentOffsetX) * ContentScale;
            ViewportZoomFocusY = (ContentZoomFocusY - ContentOffsetY) * ContentScale;

            m_enableContentOffsetUpdateFromScale = true;

            AnimationHelper.StartAnimation(this, ContentScaleProperty, newContentScale, AnimationDuration,
                delegate(object sender, EventArgs e)
                {
                    m_enableContentOffsetUpdateFromScale = false;

                    if (callback != null)
                    {
                        callback(this, EventArgs.Empty);
                    }
                });

            AnimationHelper.StartAnimation(this, ViewportZoomFocusXProperty, ViewportWidth / 2, AnimationDuration);
            AnimationHelper.StartAnimation(this, ViewportZoomFocusYProperty, ViewportHeight / 2, AnimationDuration);
        }

        private void ZoomPointToViewportCenter(double newContentScale, Point contentZoomFocus)
        {
            newContentScale = Math.Min(Math.Max(newContentScale, MinContentScale), MaxContentScale);

            AnimationHelper.CancelAnimation(this, ContentScaleProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentScale = newContentScale;
            this.ContentOffsetX = contentZoomFocus.X - (ContentViewportWidth / 2);
            this.ContentOffsetY = contentZoomFocus.Y - (ContentViewportHeight / 2);
        }

        private static void ContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomHost c = (PanZoomHost)o;

            if (c.m_contentScaleTransform != null)
            {
                c.m_contentScaleTransform.ScaleX = c.ContentScale;
                c.m_contentScaleTransform.ScaleY = c.ContentScale;
            }
            c.UpdateContentViewportSize();

            if (c.m_enableContentOffsetUpdateFromScale)
            {
                try
                {
                    c.m_disableContentFocusSync = true;
                    double viewportOffsetX = c.ViewportZoomFocusX - (c.ViewportWidth / 2);
                    double viewportOffsetY = c.ViewportZoomFocusY - (c.ViewportHeight / 2);
                    double contentOffsetX = viewportOffsetX / c.ContentScale;
                    double contentOffsetY = viewportOffsetY / c.ContentScale;
                    c.ContentOffsetX = (c.ContentZoomFocusX - (c.ContentViewportWidth / 2)) - contentOffsetX;
                    c.ContentOffsetY = (c.ContentZoomFocusY - (c.ContentViewportHeight / 2)) - contentOffsetY;
                }
                finally
                {
                    c.m_disableContentFocusSync = false;
                }
            }

            if (c.ContentScaleChanged != null)
            {
                c.ContentScaleChanged(c, EventArgs.Empty);
            }

            if (c.m_scrollOwner != null)
            {
                c.m_scrollOwner.InvalidateScrollInfo();
            }
        }

        private static object ContentScale_Coerce(DependencyObject d, object baseValue)
        {
            PanZoomHost c = (PanZoomHost)d;
            double value = (double)baseValue;
            value = Math.Min(Math.Max(value, c.MinContentScale), c.MaxContentScale);
            return value;
        }

        private static void MinOrMaxContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomHost c = (PanZoomHost)o;
            c.ContentScale = Math.Min(Math.Max(c.ContentScale, c.MinContentScale), c.MaxContentScale);
        }

        private static void ContentOffsetX_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomHost c = (PanZoomHost)o;

            c.UpdateTranslationX();

            if (!c.m_disableContentFocusSync)
            {
                c.UpdateContentZoomFocusX();
            }

            if (c.ContentOffsetXChanged != null)
            {
                c.ContentOffsetXChanged(c, EventArgs.Empty);
            }

            if (!c.m_disableScrollOffsetSync && c.m_scrollOwner != null)
            {
                c.m_scrollOwner.InvalidateScrollInfo();
            }
        }

        private static object ContentOffsetX_Coerce(DependencyObject d, object baseValue)
        {
            PanZoomHost c = (PanZoomHost)d;
            double value = (double)baseValue;
            double minOffsetX = 0.0;
            double maxOffsetX = Math.Max(0.0, c.m_unScaledExtent.Width - c.m_constrainedContentViewportWidth);
            value = Math.Min(Math.Max(value, minOffsetX), maxOffsetX);
            return value;
        }

        private static void ContentOffsetY_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomHost c = (PanZoomHost)o;

            c.UpdateTranslationY();

            if (!c.m_disableContentFocusSync)
            {
                c.UpdateContentZoomFocusY();
            }

            if (c.ContentOffsetYChanged != null)
            {
                c.ContentOffsetYChanged(c, EventArgs.Empty);
            }

            if (!c.m_disableScrollOffsetSync && c.m_scrollOwner != null)
            {
                c.m_scrollOwner.InvalidateScrollInfo();
            }

        }

        private static object ContentOffsetY_Coerce(DependencyObject d, object baseValue)
        {
            PanZoomHost c = (PanZoomHost)d;
            double value = (double)baseValue;
            double minOffsetY = 0.0;
            double maxOffsetY = Math.Max(0.0, c.m_unScaledExtent.Height - c.m_constrainedContentViewportHeight);
            value = Math.Min(Math.Max(value, minOffsetY), maxOffsetY);
            return value;
        }

        private void ResetViewportZoomFocus()
        {
            ViewportZoomFocusX = ViewportWidth / 2;
            ViewportZoomFocusY = ViewportHeight / 2;
        }

        private void UpdateViewportSize(Size newSize)
        {
            if (m_viewport == newSize)
            {
                return;
            }
            m_viewport = newSize;
            UpdateContentViewportSize();
            UpdateContentZoomFocusX();
            UpdateContentZoomFocusY();
            ResetViewportZoomFocus();
            this.ContentOffsetX = this.ContentOffsetX;
            this.ContentOffsetY = this.ContentOffsetY;

            if (m_scrollOwner != null)
            {
                m_scrollOwner.InvalidateScrollInfo();
            }
        }

        private void UpdateContentViewportSize()
        {
            ContentViewportWidth = ViewportWidth / ContentScale;
            ContentViewportHeight = ViewportHeight / ContentScale;

            m_constrainedContentViewportWidth = Math.Min(ContentViewportWidth, m_unScaledExtent.Width);
            m_constrainedContentViewportHeight = Math.Min(ContentViewportHeight, m_unScaledExtent.Height);

            UpdateTranslationX();
            UpdateTranslationY();
        }

        private void UpdateTranslationX()
        {
            if (this.m_contentOffsetTransform != null)
            {
                double scaledContentWidth = this.m_unScaledExtent.Width * this.ContentScale;
                if (scaledContentWidth < this.ViewportWidth)
                {
                    this.m_contentOffsetTransform.X = (this.ContentViewportWidth - this.m_unScaledExtent.Width) / 2;
                }
                else
                {
                    this.m_contentOffsetTransform.X = -this.ContentOffsetX;
                }
            }
        }

        private void UpdateTranslationY()
        {
            if (this.m_contentOffsetTransform != null)
            {
                double scaledContentHeight = this.m_unScaledExtent.Height * this.ContentScale;
                if (scaledContentHeight < this.ViewportHeight)
                {
                    this.m_contentOffsetTransform.Y = (this.ContentViewportHeight - this.m_unScaledExtent.Height) / 2;
                }
                else
                {
                    this.m_contentOffsetTransform.Y = -this.ContentOffsetY;
                }
            }
        }

        private void UpdateContentZoomFocusX()
        {
            ContentZoomFocusX = ContentOffsetX + (m_constrainedContentViewportWidth / 2);
        }

        private void UpdateContentZoomFocusY()
        {
            ContentZoomFocusY = ContentOffsetY + (m_constrainedContentViewportHeight / 2);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size childSize = base.MeasureOverride(infiniteSize);

            if (childSize != m_unScaledExtent)
            {
                m_unScaledExtent = childSize;

                if (m_scrollOwner != null)
                {
                    m_scrollOwner.InvalidateScrollInfo();
                }
            }

            UpdateViewportSize(constraint);

            double width = constraint.Width;
            double height = constraint.Height;

            if (double.IsInfinity(width))
            {
                width = childSize.Width;
            }

            if (double.IsInfinity(height))
            {
                height = childSize.Height;
            }

            UpdateTranslationX();
            UpdateTranslationY();

            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(this.DesiredSize);

            if (m_content.DesiredSize != m_unScaledExtent)
            {
                m_unScaledExtent = m_content.DesiredSize;

                if (m_scrollOwner != null)
                {
                    m_scrollOwner.InvalidateScrollInfo();
                }
            }
            UpdateViewportSize(arrangeBounds);

            return size;
        }
        #endregion Internal Methods

        #region IScrollInfo Interface
        public bool CanVerticallyScroll
        {
            get
            {
                return m_canVerticallyScroll;
            }
            set
            {
                m_canVerticallyScroll = value;
            }
        }

        public bool CanHorizontallyScroll
        {
            get
            {
                return m_canHorizontallyScroll;
            }
            set
            {
                m_canHorizontallyScroll = value;
            }
        }

        public double ExtentWidth
        {
            get
            {
                return m_unScaledExtent.Width * ContentScale;
            }
        }

        public double ExtentHeight
        {
            get
            {
                return m_unScaledExtent.Height * ContentScale;
            }
        }

        public double ViewportWidth
        {
            get
            {
                return m_viewport.Width;
            }
        }

        public double ViewportHeight
        {
            get
            {
                return m_viewport.Height;
            }
        }

        public ScrollViewer ScrollOwner
        {
            get
            {
                return m_scrollOwner;
            }
            set
            {
                m_scrollOwner = value;
            }
        }

        public double HorizontalOffset
        {
            get
            {
                return ContentOffsetX * ContentScale;
            }
        }

        public double VerticalOffset
        {
            get
            {
                return ContentOffsetY * ContentScale;
            }
        }

        public void SetHorizontalOffset(double offset)
        {
            if (m_disableScrollOffsetSync)
            {
                return;
            }

            try
            {
                m_disableScrollOffsetSync = true;

                ContentOffsetX = offset / ContentScale;
            }
            finally
            {
                m_disableScrollOffsetSync = false;
            }
        }

        public void SetVerticalOffset(double offset)
        {
            if (m_disableScrollOffsetSync)
            {
                return;
            }

            try
            {
                m_disableScrollOffsetSync = true;

                ContentOffsetY = offset / ContentScale;
            }
            finally
            {
                m_disableScrollOffsetSync = false;
            }
        }

        public void LineUp()
        {
            ContentOffsetY -= (ContentViewportHeight / 10);
        }

        public void LineDown()
        {
            ContentOffsetY += (ContentViewportHeight / 10);
        }

        public void LineLeft()
        {
            ContentOffsetX -= (ContentViewportWidth / 10);
        }

        public void LineRight()
        {
            ContentOffsetX += (ContentViewportWidth / 10);
        }

        public void PageUp()
        {
            ContentOffsetY -= ContentViewportHeight;
        }

        public void PageDown()
        {
            ContentOffsetY += ContentViewportHeight;
        }

        public void PageLeft()
        {
            ContentOffsetX -= ContentViewportWidth;
        }

        public void PageRight()
        {
            ContentOffsetX += ContentViewportWidth;
        }

        public void MouseWheelDown()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineDown();
            }
        }

        public void MouseWheelLeft()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineLeft();
            }
        }

        public void MouseWheelRight()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineRight();
            }
        }

        public void MouseWheelUp()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineUp();
            }
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (m_content.IsAncestorOf(visual))
            {
                Rect transformedRect = visual.TransformToAncestor(m_content).TransformBounds(rectangle);
                Rect viewportRect = new Rect(ContentOffsetX, ContentOffsetY, ContentViewportWidth, ContentViewportHeight);
                if (!transformedRect.Contains(viewportRect))
                {
                    double horizOffset = 0;
                    double vertOffset = 0;

                    if (transformedRect.Left < viewportRect.Left)
                    {
                        horizOffset = transformedRect.Left - viewportRect.Left;
                    }
                    else if (transformedRect.Right > viewportRect.Right)
                    {
                        horizOffset = transformedRect.Right - viewportRect.Right;
                    }

                    if (transformedRect.Top < viewportRect.Top)
                    {
                        vertOffset = transformedRect.Top - viewportRect.Top;
                    }
                    else if (transformedRect.Bottom > viewportRect.Bottom)
                    {
                        vertOffset = transformedRect.Bottom - viewportRect.Bottom;
                    }

                    SnapContentOffsetTo(new Point(ContentOffsetX + horizOffset, ContentOffsetY + vertOffset));
                }
            }
            return rectangle;
        }
        #endregion

        #region Accessors
        public double CoerceWidthMax
        {
            get
            {
                return m_unScaledExtent.Width - m_constrainedContentViewportWidth;
            }
        }

        public double CoerceHeightMax
        {
            get
            {
                return m_unScaledExtent.Height - m_constrainedContentViewportHeight;
            }
        }
        #endregion

        #region Internal Data Members
        private FrameworkElement m_content = null;
        private ScaleTransform m_contentScaleTransform = null;
        private TranslateTransform m_contentOffsetTransform = null;
        private bool m_enableContentOffsetUpdateFromScale = false;
        private bool m_disableScrollOffsetSync = false;
        private bool m_disableContentFocusSync = false;
        private double m_constrainedContentViewportWidth = 0.0;
        private double m_constrainedContentViewportHeight = 0.0;
        #endregion Internal Data Members

        #region IScrollInfo Data Members
        private bool m_canVerticallyScroll = false;
        private bool m_canHorizontallyScroll = false;
        private Size m_unScaledExtent = new Size(0, 0);
        private Size m_viewport = new Size(0, 0);
        private ScrollViewer m_scrollOwner = null;
        #endregion IScrollInfo Data Members
    }
}
