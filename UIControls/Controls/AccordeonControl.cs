using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace UIControls.Controls
{
    public class AccordeonControl: ItemsControl
    {
        public AccordeonControl()
        {
            LayoutUpdated += new EventHandler(AccordeonControl_LayoutUpdated);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Items.Count > 1)
            {
                if (SelectedIndex >= 0)
                {
                    AccordeonItem item = Items.GetItemAt(SelectedIndex) as AccordeonItem;
                    updateSelection(item);
                }
            }
        }

        void AccordeonControl_LayoutUpdated(object sender, EventArgs e)
        {
            detectFirstAndLast();
        }

        void detectFirstAndLast()
        {
            if (Items.Count == 1)
            {
                if (IsItemItsOwnContainerOverride(Items[0]))
                {
                    var container = Items[0] as AccordeonItem;
                    container.ItemPosition = AccordeonItem.AccordeonItemPosition.Single;
                    container.IsSelected = true;
                }
            }

            if (Items.Count > 1)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (IsItemItsOwnContainerOverride(Items[i]))
                    {
                        var container = Items[i] as AccordeonItem;
                        if (i == 0)
                        {
                            container.ItemPosition = AccordeonItem.AccordeonItemPosition.Top;
                        }
                        else if (i == Items.Count - 1)
                        {
                            container.ItemPosition = AccordeonItem.AccordeonItemPosition.Bottom;
                        }
                    }
                    else
                    {
                        var container = ItemContainerGenerator.ContainerFromItem(Items[i]) as AccordeonItem;
                        if (i == 0)
                        {
                            container.ItemPosition = AccordeonItem.AccordeonItemPosition.Top;
                        }
                        else if (i == Items.Count - 1)
                        {
                            container.ItemPosition = AccordeonItem.AccordeonItemPosition.Bottom;
                        }
                    }
                }
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            AccordeonItem container = element as AccordeonItem;
            if (container != null)
            {
                container.HeaderClicked += new AccordeonItem.HeaderClickedEventHandler(container_HeaderClicked);
            }
        }

        void container_HeaderClicked(object sender, AccordeonItem.HeaderClickedEventArgs e)
        {
            AccordeonItem item = sender as AccordeonItem;

            updateSelection(item);
        }

        void updateSelection(AccordeonItem ai)
        {
            if (ai.IsSelected)
            {
                ai.IsSelected = false;
                SelectedItem = null;
            }
            else
            {
                if (SelectedItem != null)
                {
                    AccordeonItem selectedContainer;

                    if (IsItemItsOwnContainerOverride(SelectedItem))
                    {
                        selectedContainer = SelectedItem as AccordeonItem;
                    }
                    else
                    {
                        selectedContainer = ItemContainerGenerator.ContainerFromItem(SelectedItem) as AccordeonItem;
                    }

                    selectedContainer.IsSelected = false;
                    SelectedItem = null;
                }

                ai.IsSelected = true;

                if (IsItemItsOwnContainerOverride(ai))
                {
                    SelectedItem = ai;
                }
                else
                {
                    SelectedItem = ai.DataContext;
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AccordeonItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AccordeonItem;
        }

        #region DependencyProperty 'SelectedItem'
        /// <summary>
        /// sets or gets the SelectedItem
        /// </summary>
        public object SelectedItem
        {
        get { return (object)this.GetValue(SelectedItemProperty); }
        set { this.SetValue(SelectedItemProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedItem
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(AccordeonControl),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'SelectedIndex'
        /// <summary>
        /// sets or gets the SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedIndex
        /// </summary>
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex",
            typeof(int),
            typeof(AccordeonControl),
            new PropertyMetadata(-1, SelectedIndexPropertyChangedCallback)
        );
        private static void SelectedIndexPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AccordeonControl _this = sender as AccordeonControl;
            if (_this != null)
            {
                if ((int)e.NewValue >= 0)
                {
                    if (_this.Items.Count > 0)
                    {
                        AccordeonItem item = _this.Items.GetItemAt((int)e.NewValue) as AccordeonItem;
                        _this.updateSelection(item);
                    }
                }
            }
        }

        #endregion
        
    }
}
