using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace UIControls.Helper
{
    public class TextBoxHelper : DependencyObject
    {
        #region AttachedDependencyProperty 'TextChangedCommand'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetTextChangedCommand(UIElement element, ICommand value)
        {
            element.SetValue(TextChangedCommandProperty, value);
        }
        public static ICommand GetTextChangedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(TextChangedCommandProperty);
        }

        /// <summary>
        /// DependencyProperty TextChangedCommand
        /// </summary>
        public static readonly DependencyProperty TextChangedCommandProperty = DependencyProperty.RegisterAttached(
            "TextChangedCommand",
            typeof(ICommand),
            typeof(TextBoxHelper),
            new PropertyMetadata(null, TextChangedCommandPropertyChangedCallback)
        );
        private static void TextChangedCommandPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                if (e.NewValue != null)
                {
                    box.KeyUp += textChanged_PreviewKeyUp;
                }
                else
                {
                    box.KeyUp -= textChanged_PreviewKeyUp;
                }
            }
        }

        static void textChanged_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
                ICommand cmd = GetTextChangedCommand(box);
                if (cmd != null)
                    cmd.Execute(box.Text);
            }
        }

        #endregion

        #region AttachedDependencyProperty 'Command'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetCommand(UIElement element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }
        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        /// <summary>
        /// DependencyProperty Command
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(TextBoxHelper),
            new PropertyMetadata(null, CommandPropertyChangedCallback)
        );

        private static void CommandPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //TextBox box = sender as TextBox;
            //if (box != null)
            //{
            //    if (e.NewValue != null)
            //    {
            //        box.GotKeyboardFocus += box_GotKeyboardFocus;
            //        box.PreviewLostKeyboardFocus += box_PreviewLostKeyboardFocus;
            //        box.PreviewKeyDown += box_PreviewKeyDown;
            //    }
            //    else
            //    {
            //        box.GotKeyboardFocus -= box_GotKeyboardFocus;
            //        box.PreviewLostKeyboardFocus -= box_PreviewLostKeyboardFocus;
            //        box.PreviewKeyDown -= box_PreviewKeyDown;
            //    }
            //}
        }
        
        /*
        static void box_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

            TextBox box = sender as TextBox;
            if (box != null)
            {
                TextBoxHelper.SetText_GotFocus(box, box.Text);
            }
        }

        static void box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var box = sender as TextBox;
            if (box != null)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        triggerCommand(box);
                        break;
                }
            }
        }
        
        static void box_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text != TextBoxHelper.GetText_GotFocus(box))
            {
                triggerCommand(sender as TextBox);
            }
        }

        static void triggerCommand(TextBox box)
        {
            ICommand cmd = TextBoxHelper.GetCommand(box);
            if (cmd != null)
            {
                cmd.Execute(TextBoxHelper.GetCommandParameter(box));
            }
            TextBoxHelper.SetText_GotFocus(box, box.Text);
        }
         * */
        #endregion
        
        #region AttachedDependencyProperty 'Text_GotFocus'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        private static void SetText_GotFocus(TextBox element, string value)
        {
            element.SetValue(Text_GotFocusProperty, value);
        }
        private static string GetText_GotFocus(TextBox element)
        {
            return (string)element.GetValue(Text_GotFocusProperty);
        }

        /// <summary>
        /// DependencyProperty Text_GotFocus
        /// </summary>
        public static readonly DependencyProperty Text_GotFocusProperty = DependencyProperty.RegisterAttached(
            "Text_GotFocus",
            typeof(string),
            typeof(TextBoxHelper),
            new PropertyMetadata(null, Text_GotFocusPropertyChangedCallback)
        );
        private static void Text_GotFocusPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box != null)
            {
            }
        }
        #endregion

        #region AttachedDependencyProperty 'CommandParameter'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetCommandParameter(UIElement element, object value)
        {
            element.SetValue(CommandParameterProperty, value);
        }
        public static object GetCommandParameter(UIElement element)
        {
            return (object)element.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// DependencyProperty CommandParameter
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(TextBoxHelper),
            new PropertyMetadata(null, CommandParameterPropertyChangedCallback)
        );
        private static void CommandParameterPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion
    }
}
