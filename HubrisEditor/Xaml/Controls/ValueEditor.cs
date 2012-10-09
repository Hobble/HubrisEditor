using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HubrisEditor.Xaml.Controls
{
    [TemplatePart(Name = "PART_EditorField", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_DisplayField", Type = typeof(TextBlock))]
    public class ValueEditor : Control
    {
        static ValueEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueEditor), new FrameworkPropertyMetadata(typeof(ValueEditor)));
        }

        #region Control Method Overrides
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_ellipsisTextBlock = GetTemplateChild("PART_DisplayField") as TextBlock;
            m_editorTextBox = GetTemplateChild("PART_EditorField") as TextBox;
            InitializeParts();
        }
        #endregion

        #region Internal Methods
        public void InitializeParts()
        {
            m_ellipsisTextBlock.Visibility = Visibility.Visible;
            m_editorTextBox.Visibility = Visibility.Collapsed;

            m_ellipsisTextBlock.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(EllipsisTextBlock_PreviewMouseLeftButtonDown);
            m_editorTextBox.PreviewKeyDown += new KeyEventHandler(EditorTextBox_PreviewKeyDown);
            m_editorTextBox.LostFocus += new RoutedEventHandler(EditorTextBox_LostFocus);
        }

        private void UpdateTextBoxSource()
        {
            BindingExpression expression = BindingOperations.GetBindingExpression(m_editorTextBox, TextBox.TextProperty);
            expression.UpdateSource();
            IsInEditMode = false;
        }
        #endregion

        #region Internal Event Handlers
        protected virtual void EllipsisTextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && !IsReadOnly)
            {
                IsInEditMode = true;
            }
        }

        protected virtual void EditorTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateTextBoxSource();
            }
        }

        protected virtual void EditorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateTextBoxSource();
        }
        #endregion

        #region Dependency Properties
        public string LongFormText
        {
            get { return (string)GetValue(LongFormTextProperty); }
            set { SetValue(LongFormTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LongFormText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongFormTextProperty = DependencyProperty.Register("LongFormText", typeof(string), typeof(ValueEditor), new UIPropertyMetadata(string.Empty, LongFormTextProperty_DependencyPropertyChanged));

        public string TruncatedText
        {
            get { return (string)GetValue(TruncatedTextProperty); }
            set { SetValue(TruncatedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TruncatedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TruncatedTextProperty = DependencyProperty.Register("TruncatedText", typeof(string), typeof(ValueEditor), new UIPropertyMetadata(string.Empty));

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsInEditMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(ValueEditor), new UIPropertyMetadata(false, IsInEditModeProperty_DependencyPropertyChanged, IsInEditModeProperty_CoerceValue));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ValueEditor), new UIPropertyMetadata(false, IsReadOnlyProperty_DependencyPropertyChanged));
        #endregion

        #region Dependency Property Changed Event Handlers
        private static void LongFormTextProperty_DependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueEditor editor = sender as ValueEditor;
            Typeface typeface = new Typeface(editor.FontFamily, editor.FontStyle, editor.FontWeight, editor.FontStretch);
            FormattedText text = new FormattedText(e.NewValue.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, editor.FontSize, editor.Foreground);
            if (text.Width > editor.MaxWidth)
            {
                string truncated = e.NewValue.ToString();
                string ellipsis = "...";
                bool running = true;
                while (running)
                {
                    truncated = truncated.Remove(truncated.Length - 1);
                    FormattedText runningText = new FormattedText(truncated + ellipsis, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, editor.FontSize, editor.Foreground);
                    if (runningText.Width <= editor.MaxWidth || truncated == string.Empty)
                    {
                        running = false;
                        if (truncated != string.Empty)
                        {
                            editor.TruncatedText = truncated + ellipsis;
                            editor.RaiseLongFormTextChangedEvent();
                        }
                    }
                }
            }
            else
            {
                editor.TruncatedText = e.NewValue.ToString();
                editor.RaiseLongFormTextChangedEvent();
            }
        }

        private static void IsInEditModeProperty_DependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueEditor editor = sender as ValueEditor;
            if (e.NewValue.Equals(true))
            {
                editor.m_ellipsisTextBlock.Visibility = Visibility.Collapsed;
                editor.m_editorTextBox.Visibility = Visibility.Visible;
            }
            else if (e.NewValue.Equals(false))
            {
                editor.m_ellipsisTextBlock.Visibility = Visibility.Visible;
                editor.m_editorTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private static void IsReadOnlyProperty_DependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValueEditor editor = sender as ValueEditor;
            if (e.NewValue.Equals(true))
            {
                editor.IsInEditMode = false;
            }
        }
        #endregion

        #region Coerce Methods
        private static object IsInEditModeProperty_CoerceValue(DependencyObject sender, object value)
        {
            ValueEditor editor = sender as ValueEditor;
            if (editor.IsReadOnly)
            {
                return false;
            }
            return value;
        }
        #endregion

        #region Events
        public static readonly RoutedEvent LongFormTextChangedEvent = EventManager.RegisterRoutedEvent("LongFormTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ValueEditor));

        public event RoutedEventHandler LongFormTextChanged
        {
            add { AddHandler(LongFormTextChangedEvent, value); }
            remove { RemoveHandler(LongFormTextChangedEvent, value); }
        }

        private void RaiseLongFormTextChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(LongFormTextChangedEvent);
            RaiseEvent(newEventArgs);
        }
        #endregion

        #region Members
        private TextBlock m_ellipsisTextBlock;
        private TextBox m_editorTextBox;
        #endregion
    }
}
