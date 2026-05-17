# RPG Inventory & Crafting Simulator

> Interaktywna aplikacja okienkowa (WPF) demonstrująca zaawansowane mechanizmy programowania obiektowego w języku C#. Projekt symuluje system zarządzania ekwipunkiem i rzemiosłem (crafting) znany z gier RPG.

---

## 📖 Spis Treści
- [O projekcie](#o-projekcie)
- [Technologie](#technologie)
- [Funkcjonalności](#funkcjonalności)
- [Rozszerzenia (Ocena Bardzo Dobra)](#rozszerzenia-ocena-bardzo-dobra)
- [Dokumentacja Architektury OOP (Wymagania)](#dokumentacja-architektury-oop-wymagania)
- [Uruchomienie](#uruchomienie)

---

## 🛠 O projekcie
Aplikacja została stworzona jako projekt zaliczeniowy z programowania obiektowego. Celem było praktyczne zaimplementowanie 12 kluczowych mechanizmów OOP w jednym, spójnym środowisku z interfejsem graficznym. Zamiast izolowanych przykładów, mechanizmy te współpracują ze sobą, tworząc w pełni funkcjonalny symulator ekwipunku postaci.

## 💻 Technologie
- **Język:** C# (C# 10.0+)
- **Framework:** .NET / WPF (Windows Presentation Foundation)
- **Paradygmat:** Programowanie Zorientowane Obiektowo (OOP)

## ✨ Funkcjonalności
- **Kreator przedmiotów:** Dynamiczne tworzenie mikstur i broni z wykorzystaniem polimorfizmu.
- **Asynchroniczny Crafting:** Łączenie mikstur w potężniejsze eliksiry bez blokowania interfejsu (UI thread).
- **Inspektor Obiektów:** Narzędzie oparte na refleksji pozwalające na podgląd "wnętrza" i właściwości dowolnego obiektu w czasie działania programu.
- **Reaktywny Interfejs:** Automatyczna aktualizacja widoków (Listbox) dzięki powiązaniom danych i zdarzeniom.

---

## 🚀 Rozszerzenia (Ocena Bardzo Dobra)
Projekt wychodzi poza standardowe ramy kursu, implementując dodatkowe, zaawansowane wzorce projektowe i mechanizmy języka C#:

1. **Metody rozszerzające (Extension Methods):** - Zaimplementowano statyczną klasę `ItemExtensions`, która bezinwazyjnie rozszerza interfejs `IItem` o metodę `GetExclamatoryDescription()`.
2. **Restrykcje typów generycznych (Generic Type Constraints):** - Klasa `Inventory<T>` została ograniczona klauzulą `where T : IItem`, co wymusza silne typowanie i chroni przed błędnym wstrzyknięciem danych bazowych.
3. **Zaawansowane kolekcje WPF (ObservableCollection):** - Zastąpiono standardowe listy `List<T>` kolekcją `ObservableCollection<T>`. Eliminuje to konieczność ręcznego odświeżania UI i zapewnia integralność stanu widoku przy asynchronicznych modyfikacjach danych.
4. **LINQ (Language Integrated Query):** - Wykorzystano metody LINQ (np. `.Cast<IItem>().ToList()`) do bezpiecznego rzutowania i transformacji kolekcji obiektów wybranych z UI.

---

## 📐 Dokumentacja Architektury OOP (Wymagania)

Poniżej przedstawiono mapę implementacji 12 wymaganych punktów zaliczeniowych w strukturze projektu.

| # | Wymaganie | Implementacja w kodzie | Opis |
|---|---|---|---|
| **1** | **Klasy** | `BaseItem`, `Potion`, `Weapon`, `Inventory<T>` | Podstawowe jednostki logiczne budujące architekturę symulatora. |
| **2** | **Konstruktory** | Np. `Potion(string name, int power)` | Klasy bazowe i pochodne posiadają konstruktory, wykorzystano inicjalizację z użyciem `base()`. |
| **3** | **Właściwości / Indeksatory** | `Inventory<T>.this[int index]` | Dane hermetyzowane za pomocą properties (`get; set;`). W klasie ekwipunku użyto indeksatora dla tablicowego dostępu do obiektów. |
| **4** | **Statyczne** | `Potion.TotalPotionsCreated` | Właściwość śledząca globalną liczbę utworzonych mikstur. Utworzono również statyczną klasę z metodami rozszerzającymi. |
| **5** | **Dziedziczenie** | `class Potion : BaseItem` | Mikstury i bronie dziedziczą wspólny kontrakt z klasy bazowej, co promuje reużywalność kodu. |
| **6** | **Polimorfizm** | `override string GetDescription()` | Obiekty posiadają własne implementacje metody opisowej, interpretowane dynamicznie w czasie wykonywania programu. |
| **7** | **Interfejsy / Abstrakcja** | `IItem`, `abstract class BaseItem` | Ustanowienie kontraktów dla przedmiotów. Blokada możliwości utworzenia "pustego", niezdefiniowanego przedmiotu dzięki klasie abstrakcyjnej. |
| **8** | **Typy ogólne / Kolekcje** | `Inventory<T>` | Generyczny kontener do przechowywania przedmiotów dowolnego, określonego umową typu. |
| **9** | **Delegacje / Zdarzenia** | `event EventHandler<T> OnItemAdded` | System powiadomień. Dodanie przedmiotu do bazy emituje zdarzenie, które nasłuchiwane jest przez klasę widoku w celu logowania operacji. |
| **10** | **Przeciążanie operatorów**| `operator +(Potion a, Potion b)` | Zdefiniowanie zachowania operatora dodawania dla klas własnych – mechanizm wykorzystany w systemie warzenia (craftingu). |
| **11** | **Prog. asynchroniczne** | `async / await`, `Task.Delay()` | Symulacja operacji I/O (ładowanie z serwera, proces craftingu) na osobnych wątkach, zachowując responsywność aplikacji. |
| **12** | **Refleksja** | `selectedItem.GetType()`, `GetProperties()`| Dynamiczna analiza metadanych. Aplikacja w locie bada nieznany obiekt i wyświetla listę jego składowych oraz strukturę dziedziczenia. |

---

## ⚙️ Uruchomienie

1. Sklonuj repozytorium:
   ```bash
   git clone [https://github.com/TwojLogin/RepoName.git](https://github.com/TwojLogin/RepoName.git)