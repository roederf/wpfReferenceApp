using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Collections;

namespace UIControls.Controls
{
    public class SearchBox : ContentControl
    {
        TextBox _textBox;
        Button _closeButton;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = GetTemplateChild("PART_SearchTextBox") as TextBox;
            _closeButton = GetTemplateChild("PART_CloseButton") as Button;

            if (_textBox != null)
            {
                _textBox.KeyUp += _textBox_KeyUp;
                _textBox.GotFocus += new RoutedEventHandler(_textBox_GotFocus);
                _textBox.LostFocus += new RoutedEventHandler(_textBox_LostFocus);
            }

            if (_closeButton != null)
            {
                _closeButton.Click +=new RoutedEventHandler(_closeButton_Click);
            }
        }

        void _textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_textBox.Text == null || _textBox.Text == "")
                HintVisibility = System.Windows.Visibility.Visible;
            else
                HintVisibility = System.Windows.Visibility.Collapsed;
        }

        void _textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HintVisibility = System.Windows.Visibility.Collapsed;
        }

        void _closeButton_Click(object sender, RoutedEventArgs e)
        {
            _textBox.Text = "";
            Text = null;

            HintVisibility = System.Windows.Visibility.Visible;
        }

        void _textBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (FilterCommand != null)
            {
                FilterCommand.Execute(_textBox.Text);
            }
        }

        #region DependencyProperty 'Text'
        /// <summary>
        /// sets or gets the Text
        /// </summary>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Text
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(SearchBox),
            new PropertyMetadata(null, TextPropertyChangedCallback)
        );
        private static void TextPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SearchBox _this = sender as SearchBox;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'Hint'
        /// <summary>
        /// sets or gets the Hint
        /// </summary>
        public string Hint
        {
            get { return (string)this.GetValue(HintProperty); }
            set { this.SetValue(HintProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Hint
        /// </summary>
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
            "Hint",
            typeof(string),
            typeof(SearchBox),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'HintVisibility'
        /// <summary>
        /// sets or gets the HintVisibility
        /// </summary>
        public Visibility HintVisibility
        {
            get { return (Visibility)this.GetValue(HintVisibilityProperty); }
            private set { this.SetValue(HintVisibilityProperty, value); }
        }
        /// <summary>
        /// DependencyProperty HintVisibility
        /// </summary>
        public static readonly DependencyProperty HintVisibilityProperty = DependencyProperty.Register(
            "HintVisibility",
            typeof(Visibility),
            typeof(SearchBox),
            new PropertyMetadata(Visibility.Visible)
        );
        #endregion

        #region DependencyProperty 'FilterCommand'
        /// <summary>
        /// sets or gets the FilterCommand
        /// </summary>
        public ICommand FilterCommand
        {
            get { return (ICommand)this.GetValue(FilterCommandProperty); }
            set { this.SetValue(FilterCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FilterCommand
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register(
            "FilterCommand",
            typeof(ICommand),
            typeof(SearchBox),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'EnterFilterCommand'
        /// <summary>
        /// sets or gets the EnterFilterCommand
        /// </summary>
        public ICommand EnterFilterCommand
        {
            get { return (ICommand)this.GetValue(EnterFilterCommandProperty); }
            set { this.SetValue(EnterFilterCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty EnterFilterCommand
        /// </summary>
        public static readonly DependencyProperty EnterFilterCommandProperty = DependencyProperty.Register(
            "EnterFilterCommand",
            typeof(ICommand),
            typeof(SearchBox),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'SelectItemCommand'
        /// <summary>
        /// sets or gets the SelectItemCommand
        /// </summary>
        public ICommand SelectItemCommand
        {
            get { return (ICommand)this.GetValue(SelectItemCommandProperty); }
            set { this.SetValue(SelectItemCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectItemCommand
        /// </summary>
        public static readonly DependencyProperty SelectItemCommandProperty = DependencyProperty.Register(
            "SelectItemCommand",
            typeof(ICommand),
            typeof(SearchBox),
            new PropertyMetadata(null)
        );
        #endregion
    }
}
