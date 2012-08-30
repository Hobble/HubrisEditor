using System;
using System.Collections.Generic;
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_editorField = GetTemplateChild("PART_EditorField") as TextBox;
            m_displayField = GetTemplateChild("PART_DisplayField") as TextBlock;

            m_editorField.Visibility = System.Windows.Visibility.Collapsed;
            m_displayField.Visibility = System.Windows.Visibility.Visible;

            Binding editorBinding = new Binding("Text");
            editorBinding.Source = this;
            editorBinding.Mode = BindingMode.TwoWay;
            editorBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(m_editorField, TextBox.TextProperty, editorBinding);

            Binding displayBinding = new Binding("Text");
            displayBinding.Source = this;
            displayBinding.Mode = BindingMode.OneWay;
            displayBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(m_displayField, TextBlock.TextProperty, displayBinding);

            PreviewMouseDoubleClick += ValueEditor_PreviewMouseDoubleClick;
            PreviewKeyDown += ValueEditor_PreviewKeyDown;
        }

        void ValueEditor_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!m_editMode)
            {
                m_editMode = true;
                e.Handled = true;
                m_editorField.Visibility = System.Windows.Visibility.Visible;
                m_displayField.Visibility = System.Windows.Visibility.Collapsed;
                m_editorField.Dispatcher.BeginInvoke(new Action(() =>
                {
                    m_editorField.RaiseEvent(new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, e.ChangedButton, e.StylusDevice) { RoutedEvent = TextBox.MouseDownEvent });
                }), System.Windows.Threading.DispatcherPriority.Background, null);
            }
        }

        private void ValueEditor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (m_editMode)
            {
                if (e.Key == Key.Enter)
                {
                    m_editMode = false;
                    e.Handled = true;
                    m_editorField.Visibility = System.Windows.Visibility.Collapsed;
                    m_displayField.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ValueEditor), new PropertyMetadata(string.Empty));

        private TextBox m_editorField;
        private TextBlock m_displayField;
        private bool m_editMode;
    }
}
