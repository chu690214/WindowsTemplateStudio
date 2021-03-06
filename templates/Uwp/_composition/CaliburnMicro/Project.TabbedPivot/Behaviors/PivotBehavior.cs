﻿using System;
using System.Linq;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Param_ItemNamespace.Helpers;

namespace Param_ItemNamespace.Behaviors
{
    public class PivotBehavior : Behavior<Pivot>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var removedItem = e.RemovedItems.Cast<Screen>()
                .Select(i => i.GetView() as IPivotPage).FirstOrDefault();

            var addedItem = e.AddedItems.Cast<Screen>()
                .Select(i => i.GetView() as IPivotPage).FirstOrDefault();

            if (removedItem != null)
            {
                await removedItem.OnPivotUnselectedAsync();
            }

            if (addedItem != null)
            {
                await addedItem?.OnPivotSelectedAsync();
            }
        }
    }
}
