#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Script = CitizenFX.Core.BaseScript;
#elif SHVDN2
using GTA;
using GTA.Native;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
#elif SHVDN3
using GTA;
using GTA.UI;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
#endif
using LemonUI.Elements;
using LemonUI.Menus;
using LemonUI.Scaleform;
using System;

namespace LemonUI.Example
{
    /// <summary>
    /// An example script showing the basics of each item in LemonUI.
    /// </summary>
    public class Basics : Script
    {
        /// <summary>
        /// The Object Pool stores all of your LemonUI Elements so they can be processed every tick.
        /// </summary>
        private static readonly ObjectPool pool = new ObjectPool();

        /// <summary>
        /// The Native Menus are the same as NativeUI's UIMenu.
        /// </summary>
        private static readonly NativeMenu menu = new NativeMenu("LemonUI", "LemonUI Demo/Examples", "The main menu with examples.")
        {
            ItemCount = CountVisibility.Always // This will show the Index of the Current Item and Total Number of Items at all times
        };
        /// <summary>
        /// Ditto, but this one is going to be added as a submenu.
        /// </summary>
        private static readonly NativeMenu submenu = new NativeMenu("LemonUI", "LemonUI Submenu", "A submenu that shows how you can add and remove items during runtime.")
        {
            MaxItems = 17 // This will set the maximum number of items shown at once to 17
        };
        /// <summary>
        /// This is a checkbox item. Is an item that contains a checkbox that users can turn on or off.
        /// For this example, this checkbox is going to allow you to keep the menu open.
        /// </summary>
        private static readonly NativeCheckboxItem keepOpen = new NativeCheckboxItem("Keep Menu Open", "Keeps this menu open. The value of this checkbox is going to be checked by the Closing event of the menu.")
        {
            LeftBadge = new ScaledTexture("commonmenu", "shop_new_star") // This is a Badge, shown on the left side of the item
        };
        /// <summary>
        /// This is a basic menu item. It only has a title.
        /// </summary>
        private static readonly NativeItem showLoadingScreen = new NativeItem("Show Loading Screen", "Shows the GTA: Online like loading screen");
        /// <summary>
        /// This is a list of items that you can select between them.
        /// Here, we are going to use them for changing and showing the Big Message Scaleform.
        /// </summary>
        private static readonly NativeListItem<MessageType> showBigMessage = new NativeListItem<MessageType>("Show Big Message", "Shows a custom Big Message. A coupe of examples of the Big Message Scaleforma are the Wasted/Busted and Mission Passed messages.", (MessageType[])Enum.GetValues(typeof(MessageType)));
        /// <summary>
        /// All of the other items should be the same as one of the previously mentioned.
        /// </summary>
        private static readonly NativeCheckboxItem useMouse = new NativeCheckboxItem("Use Mouse", "If the mouse should be used for selecting different items.", menu.UseMouse);
        private static readonly NativeItem flip = new NativeItem("Flip", "Flips the Menu from the left to the Right.");
        private static readonly NativeItem clear = new NativeItem("Clear", "Removes all of the items from this menu. A script restart will be needed to restore the items.", "Danger!")
        {
            RightBadge = new ScaledTexture("commonmenu", "mp_alerttriangle") // This is a Badge, shown on the left side of the item
        };
        private static readonly NativeItem addRandom = new NativeItem("Add Item to Submenu", "Adds a random item to the submenu.");
        private static readonly NativeItem removeRandom = new NativeItem("Remove Items of Submenu", "Removes all of the random items on the submenu.");

        /// <summary>
        /// This creates a loading screen that looks like the one when transitioning from story mode to GTA Online.
        /// </summary>
        private static readonly LoadingScreen loadingScreen = new LoadingScreen("LemonUI", "By Lemon", "This is a little example showing how the GTA Online Loading Screen works and how it can be used.");
        /// <summary>
        /// This is a big message scaleform. Is used on the Wasted/Busted and Mission passed messages.
        /// </summary>
        private static readonly BigMessage bigMessage = new BigMessage("Lemon UI", "By Lemon", "#1")
        {
            Weapon = WeaponHash.APPistol
        };

        public Basics()
        {
            // Add the events of the menu and the items
            menu.Shown += Menu_Shown;
            menu.Closing += Menu_Closing;
            showLoadingScreen.Activated += ShowLoadingScreen_Activated;
            showBigMessage.Activated += ShowBigMessage_Activated;
            useMouse.CheckboxChanged += UseMouse_CheckboxChanged;
            flip.Activated += Flip_Activated;
            clear.Activated += Clear_Activated;
            addRandom.Activated += AddRandom_Activated;
            removeRandom.Activated += RemoveRandom_Activated;
            // And then the items and submenus themselves
            menu.Add(keepOpen);
            menu.Add(showLoadingScreen);
            menu.Add(showBigMessage);
            menu.Add(useMouse);
            menu.Add(flip);
            menu.Add(clear);
            menu.AddSubMenu(submenu);
            submenu.Add(addRandom);
            submenu.Add(removeRandom);

            // Items need to be part of the pool, so add them
            pool.Add(menu);
            pool.Add(submenu);
            pool.Add(loadingScreen);
            pool.Add(bigMessage);

            // Add the tick event
            Tick += Basics_Tick;
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            // This is triggered when the menu is shown on the screen
            // We want to make sure that only the menu is visible, so hide the other items
            loadingScreen.Visible = false;
            bigMessage.Visible = false;
        }

        private void Menu_Closing(object sender, CancelEventArgs e)
        {
            // The Cancel property says if we should cancel the closure of the menu
            // Here, we copy the activation value of the checkbox
            e.Cancel = keepOpen.Checked;
        }

        private void ShowLoadingScreen_Activated(object sender, EventArgs e)
        {
            // Here we hide the other stuff and show the loading screen
            menu.Visible = false;
            bigMessage.Visible = false;
            loadingScreen.Visible = true;
        }

        private void ShowBigMessage_Activated(object sender, EventArgs e)
        {
            // Here, we set the type of Big Message
            bigMessage.Type = showBigMessage.SelectedItem;
            // And then we show the message
            bigMessage.Visible = true;
            // And hide everything else
            menu.Visible = false;
            loadingScreen.Visible = false;
        }

        private void UseMouse_CheckboxChanged(object sender, EventArgs e)
        {
            // This will toggle mouse usage on all of the menus
            pool.ForEach<NativeMenu>(m => m.UseMouse = useMouse.Checked);
        }

        private void Flip_Activated(object sender, EventArgs e)
        {
            // This is simple C# one-liners
            // If the main menu the right, move all of them to the left and vice versa
            pool.ForEach<NativeMenu>(m => m.Alignment = menu.Alignment == Alignment.Left ? Alignment.Right : Alignment.Left);
        }

        private void Clear_Activated(object sender, EventArgs e)
        {
            // Here we call the Clear function to remove all of the items
            menu.Clear();
        }

        private void AddRandom_Activated(object sender, EventArgs e)
        {
            // Here, we just create a new item and add it onto the submenu
            submenu.Add(new NativeItem("Random", "This is a random item that we added."));
        }

        private void RemoveRandom_Activated(object sender, EventArgs e)
        {
            // Here, we remove the random items with the exception of the add and remove items
            submenu.Remove(item => item != addRandom && item != removeRandom);
        }

#if FIVEM
        private async System.Threading.Tasks.Task Basics_Tick()
#else
        private void Basics_Tick(object sender, EventArgs e)
#endif
        {
            // Process everything in the pool
            pool.Process();

            // If the user pressed Z/Down, toggle the activation of the menu
#if SHVDN3
            if (Game.IsControlJustPressed(Control.MultiplayerInfo) && !menu.Visible && !submenu.Visible)
#else
            if (Game.IsControlJustPressed(0, Control.MultiplayerInfo) && !menu.Visible && !submenu.Visible)
#endif
            {
                menu.Visible = true;
            }
        }
    }
}
