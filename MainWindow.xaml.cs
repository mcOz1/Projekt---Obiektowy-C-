using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace WpfApp
{
    // --- EXTRA: Extension Methods ---
    public static class ItemExtensions
    {
        public static string GetExclamatoryDescription(this IItem item)
        {
            return item.GetDescription() + " (Legendary rarity!)";
        }
    }

    // 7. Interfaces / Abstraction
    public interface IItem
    {
        // 3. Properties
        string Name { get; set; }
        string DisplayInfo { get; }
        string GetDescription();
    }

    // 1. Classes & 7. Abstraction (Abstract class)
    public abstract class BaseItem : IItem
    {
        public string Name { get; set; }

        // Property used for ListBox display binding
        public string DisplayInfo => GetDescription();

        // 2. Constructors
        protected BaseItem(string name)
        {
            Name = name;
        }

        // 6. Polymorphism
        public abstract string GetDescription();
    }

    // 5. Inheritance
    public class Potion : BaseItem
    {
        public int Power { get; set; }

        // 4. Static (Tracking instances)
        public static int TotalPotionsCreated { get; private set; } = 0;

        public Potion(string name, int power) : base(name)
        {
            Power = power;
            TotalPotionsCreated++;
        }

        public override string GetDescription() => $"[Potion] {Name} (+{Power} HP/MP)";

        // 10. Operator overloading
        public static Potion operator +(Potion a, Potion b)
        {
            return new Potion($"Elixir of {a.Name} & {b.Name}", a.Power + b.Power + 10); // Bonus +10 power on combo
        }
    }

    // 5. Inheritance
    public class Weapon : BaseItem
    {
        public int Damage { get; set; }

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        public override string GetDescription() => $"[Weapon] {Name} ({Damage} DMG)";
    }

    // 8. Generics / Collections (With constraint)
    // 8. Generics / Collections (With constraint & ObservableCollection for WPF)
    public class Inventory<T> where T : IItem
    {
        // Using ObservableCollection which automatically notifies UI of changes
        private ObservableCollection<T> _items = new ObservableCollection<T>();

        // 9. Delegates / Events
        public event EventHandler<T>? OnItemAdded;

        public void Add(T item)
        {
            _items.Add(item);
            OnItemAdded?.Invoke(this, item);
        }

        // 3. Indexers
        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        // Return ObservableCollection instead of List
        public ObservableCollection<T> GetAllItems() => _items;
    }

    public partial class MainWindow : Window
    {
        private Inventory<IItem> _playerInventory;

        public MainWindow()
        {
            InitializeComponent();
            _playerInventory = new Inventory<IItem>();

            // Subscribe to the custom event
            _playerInventory.OnItemAdded += Inventory_OnItemAdded;
        }

        private void Log(string message)
        {
            OutputTextBox.Text += $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            OutputTextBox.ScrollToEnd();
        }

        // Event handler responding to the delegate/event in Inventory
        private void Inventory_OnItemAdded(object? sender, IItem item)
        {
            Log($"Stored in backpack: {item.Name}");
            RefreshUI();
        }

        private void RefreshUI()
        {
            // Rebind the ListBox to update UI
            InventoryListBox.ItemsSource = null;
            InventoryListBox.ItemsSource = _playerInventory.GetAllItems();
        }

        // --- USER INTERACTION LOGIC ---

        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {
            string name = InputNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter an item name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(InputPowerTextBox.Text, out int powerVal))
            {
                MessageBox.Show("Power/Damage must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ItemTypeComboBox.SelectedIndex == 0)
            {
                Potion p = new Potion(name, powerVal);
                _playerInventory.Add(p);
            }
            else
            {
                Weapon w = new Weapon(name, powerVal);
                _playerInventory.Add(w);
                Log($"Extension feature trigger: {w.GetExclamatoryDescription()}");
            }

            InputNameTextBox.Clear();
            InputPowerTextBox.Clear();
        }

        // 11. Asynchronous programming
        private async void CombinePotions_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = InventoryListBox.SelectedItems.Cast<IItem>().ToList();

            if (selectedItems.Count != 2)
            {
                Log("Warning: You must select exactly TWO items to combine.");
                return;
            }

            if (selectedItems[0] is Potion p1 && selectedItems[1] is Potion p2)
            {
                Log("Brewing potions... please wait...");
                CombinePotions_Click_ButtonSafe(false);

                // 11. Asynchronous programming (Simulating crafting time)
                await Task.Delay(2000);

                // 10. Operator overloading
                Potion combined = p1 + p2;

                Log("Brewing successful!");
                _playerInventory.Add(combined);
                Log($"Total potions existing in the world: {Potion.TotalPotionsCreated}");

                CombinePotions_Click_ButtonSafe(true);
            }
            else
            {
                Log("Error: You can only combine two Potions. Weapons cannot be brewed!");
            }
        }

        private void CombinePotions_Click_ButtonSafe(bool isEnabled)
        {
            // Just a helper to prevent spam-clicking during async operations
            var btn = (Button)LogicalTreeHelper.FindLogicalNode(this, "CombinePotionsButton");
            if (btn != null) btn.IsEnabled = isEnabled;
        }

        // 12. Reflection
        private void Reflection_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListBox.SelectedItem is not IItem selectedItem)
            {
                Log("Select a single item from the list to inspect.");
                return;
            }

            Log($"--- Inspecting inner structure of: {selectedItem.Name} ---");
            Type itemType = selectedItem.GetType();

            Log($"Class Type: {itemType.Name}");
            Log($"Inherits from: {itemType.BaseType?.Name}");

            Log("Public Properties:");
            foreach (PropertyInfo prop in itemType.GetProperties())
            {
                object? value = prop.GetValue(selectedItem);
                Log($" - {prop.Name} [{prop.PropertyType.Name}] = {value}");
            }
        }

        // 11. Asynchronous programming
        private async void LoadInventory_Click(object sender, RoutedEventArgs e)
        {
            Log("Loading starter pack from server...");
            await Task.Delay(1500);

            _playerInventory.Add(new Potion("Minor Healing", 15));
            _playerInventory.Add(new Potion("Minor Mana", 10));
            _playerInventory.Add(new Weapon("Rusty Dagger", 5));

            Log("Starter pack loaded!");
        }
    }
}