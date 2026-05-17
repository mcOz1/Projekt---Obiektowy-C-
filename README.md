# RPG Inventory & Crafting Simulator

> An interactive Windows Presentation Foundation (WPF) application demonstrating advanced Object-Oriented Programming (OOP) mechanisms in C#. The project simulates an inventory management and crafting system commonly found in RPG games.

---

## 📖 Table of Contents
- [About the Project](#about-the-project)
- [Technologies](#technologies)
- [Features](#features)
- [Advanced Features (For Top Grade)](#advanced-features-for-top-grade)
- [OOP Architecture Documentation (Requirements)](#oop-architecture-documentation-requirements)
- [Getting Started](#getting-started)

---

## 🛠 About the Project
This application was created as a final project for an Object-Oriented Programming course. The goal was to practically implement 12 key OOP mechanisms within a single, cohesive graphical user interface environment. Instead of isolated examples, these mechanisms work together to create a fully functional character inventory simulator.

## 💻 Technologies
- **Language:** C# (C# 10.0+)
- **Framework:** .NET / WPF (Windows Presentation Foundation)
- **Paradigm:** Object-Oriented Programming (OOP)

## ✨ Features
- **Item Creator:** Dynamically create potions and weapons utilizing polymorphism.
- **Asynchronous Crafting:** Combine potions into more powerful elixirs without blocking the user interface (UI thread).
- **Object Inspector:** A reflection-based tool allowing users to inspect the "internals" and properties of any object at runtime.
- **Reactive UI:** Automatic view updates (ListBox) thanks to data binding and events.

---

## 🚀 Advanced Features (For Top Grade)
The project goes beyond the standard course framework by implementing additional, advanced design patterns and C# language features:

1. **Extension Methods:** - Implemented a static `ItemExtensions` class that non-invasively extends the `IItem` interface with a `GetExclamatoryDescription()` method.
2. **Generic Type Constraints:** - The `Inventory<T>` class is restricted by the `where T : IItem` clause, which enforces strong typing and prevents the injection of invalid base data.
3. **Advanced WPF Collections (ObservableCollection):** - Standard `List<T>` collections were replaced with `ObservableCollection<T>`. This eliminates the need for manual UI refreshing and ensures view state integrity during asynchronous data modifications.
4. **LINQ (Language Integrated Query):** - Utilized LINQ methods (e.g., `.Cast<IItem>().ToList()`) for safe casting and transformation of object collections selected from the UI.

---

## 📐 OOP Architecture Documentation (Requirements)

Below is a map illustrating where the 12 required grading points are implemented within the project's structure.

| # | Requirement | Implementation in Code | Description |
|---|---|---|---|
| **1** | **Classes** | `BaseItem`, `Potion`, `Weapon`, `Inventory<T>` | The fundamental logical units building the simulator's architecture. |
| **2** | **Constructors** | e.g., `Potion(string name, int power)` | Base and derived classes have constructors; initialization using `base()` is utilized. |
| **3** | **Properties / Indexers** | `Inventory<T>.this[int index]` | Data encapsulated using properties (`get; set;`). An indexer is used in the inventory class for array-like access to objects. |
| **4** | **Static Members** | `Potion.TotalPotionsCreated` | A property tracking the global number of crafted potions. A static class with extension methods was also created. |
| **5** | **Inheritance** | `class Potion : BaseItem` | Potions and weapons inherit a shared contract from the base class, promoting code reusability. |
| **6** | **Polymorphism** | `override string GetDescription()` | Objects have their own implementations of the descriptive method, dynamically interpreted at runtime. |
| **7** | **Interfaces / Abstraction**| `IItem`, `abstract class BaseItem` | Establishing contracts for items. Preventing the creation of an "empty", undefined item via an abstract class. |
| **8** | **Generics / Collections** | `Inventory<T>` | A generic container for storing items of any type that complies with the specified contract. |
| **9** | **Delegates / Events** | `event EventHandler<T> OnItemAdded` | Notification system. Adding an item to the database emits an event, which is listened to by the view class to log the operation. |
| **10** | **Operator Overloading** | `operator +(Potion a, Potion b)` | Defining the behavior of the addition operator for custom classes – a mechanism used in the crafting system. |
| **11** | **Async Programming** | `async / await`, `Task.Delay()` | Simulating I/O operations (server loading, crafting process) on separate threads, maintaining application responsiveness. |
| **12** | **Reflection** | `selectedItem.GetType()`, `GetProperties()`| Dynamic metadata analysis. The application inspects an unknown object on the fly and displays its members and inheritance structure. |

---

## ⚙️ Getting Started

1. Clone the repository:
   ```bash
   git clone [https://github.com/YourUsername/RepoName.git](https://github.com/YourUsername/RepoName.git)