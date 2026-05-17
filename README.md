# RPG Inventory & Crafting Simulator

Aplikacja okienkowa (WPF) napisana w języku C#, stworzona w celu demonstracji zaawansowanych mechanizmów programowania obiektowego. Tematyką projektu jest system zarządzania ekwipunkiem bohatera oraz mechanika rzemiosła (crafting) znana z gier RPG.

Projekt spełnia wszystkie wymagania zaliczeniowe, implementując 12 kluczowych zagadnień OOP, a także zawiera dodatkowe mechanizmy wykraczające poza podstawowy materiał (na ocenę bardzo dobrą).

## 🌟 Elementy na ocenę bardzo dobrą (BDB)
Aby wyjść poza standardowe mechanizmy zajęciowe, w projekcie zaimplementowano następujące funkcje:

1. **Metody rozszerzające (Extension Methods):** Stworzono statyczną klasę `ItemExtensions`, która dodaje nową funkcjonalność (`GetExclamatoryDescription()`) do wszystkich obiektów implementujących interfejs `IItem`, bez ingerencji w sam interfejs i klasy bazowe.
2. **Ograniczenia typów generycznych (Generic Type Constraints):** Klasa ekwipunku `Inventory<T>` została zabezpieczona klauzulą `where T : IItem`. Gwarantuje to, że kolekcja przyjmie tylko poprawne obiekty w grze, chroniąc przed błędami (np. próbą stworzenia ekwipunku przechowującego zwykłe liczby).
3. **Zaawansowane wiązanie danych (Data Binding & ObservableCollection):** Zamiast zwykłej listy `List<T>`, do komunikacji z interfejsem graficznym użyto `ObservableCollection<T>`. Dzięki temu UI automatycznie reaguje na zmiany w strukturze danych (np. po scraftowaniu przedmiotu), eliminując potrzebę ręcznego odświeżania kontrolek i zapobiegając błędom niespójności.
4. **LINQ:** Zastosowano zapytania LINQ (np. `.Cast<IItem>().ToList()`) do eleganckiego i bezpiecznego wyciągania zaznaczonych elementów z interfejsu do logiki programu.

---

## 📋 Realizacja punktów z wymagań

Poniżej znajduje się mapa projektu, wskazująca, w którym miejscu kodu zrealizowano poszczególne zagadnienia.

### 1. Klasy
Cała logika gry opiera się na klasach. Zdefiniowano m.in. logikę interfejsu (`MainWindow`), klasę zarządzającą (`Inventory<T>`) oraz modele przedmiotów (`Potion`, `Weapon`, `BaseItem`).

### 2. Konstruktory
Każda z klas posiada odpowiednie konstruktory. Wykorzystano również przekazywanie parametrów do konstruktora klasy bazowej przy użyciu słowa kluczowego `base(name)` (np. w klasie `Potion`).

### 3. Właściwości / Indeksatory
* **Właściwości:** Standardowy sposób hermetyzacji danych, np. `Power` lub `Damage` w klasach przedmiotów.
* **Indeksator:** Zaimplementowany w klasie `Inventory<T>` jako `public T this[int index] { get; set; }`. Pozwala to na odwoływanie się do konkretnego slotu w plecaku tak samo, jak do elementów tablicy.

### 4. Statyczne
W klasie `Potion` użyto statycznej właściwości `TotalPotionsCreated`, która śledzi łączną liczbę mikstur wygenerowanych podczas działania programu (wartość wspólna dla wszystkich instancji klasy). Utworzono również w pełni statyczną klasę `ItemExtensions`.

### 5. Dziedziczenie
Zbudowano hierarchię klas. Klasy `Potion` (mikstury) oraz `Weapon` (broń) dziedziczą wspólne cechy (np. nazwę) z bazowej klasy `BaseItem`.

### 6. Polimorfizm
Zaimplementowano wirtualną/abstrakcyjną metodę `GetDescription()` w klasie bazowej, która jest nadpisywana (`override`) w klasach dziedziczących. Dzięki temu ten sam kod odświeżający interfejs potrafi poprawnie wyświetlić specyficzne statystyki dla różnych typów przedmiotów.

### 7. Interfejsy / Abstrakcja
* **Interfejs:** `IItem` określa kontrakt, jaki musi spełnić każdy przedmiot dodawany do plecaka (musi posiadać nazwę i opis).
* **Abstrakcja:** `BaseItem` jest klasą abstrakcyjną (`abstract class`). Nie można stworzyć w grze "czystego przedmiotu" – zawsze musi to być konkretna instancja z klasy pochodnej (broń lub mikstura).

### 8. Typy ogólne / Kolekcje
Stworzono generyczną klasę `Inventory<T>`, która może przechowywać dowolny typ zgodny z określonym ograniczeniem. Wewnątrz niej wykorzystano zaawansowaną kolekcję WPF: `ObservableCollection<T>`.

### 9. Delegacje / Zdarzenia
W klasie ekwipunku zdefiniowano zdarzenie (`public event EventHandler<T> OnItemAdded`). Zdarzenie to jest wywoływane w momencie włożenia przedmiotu do plecaka. Klasa `MainWindow` "nasłuchuje" tego zdarzenia, by automatycznie dopisać odpowiedni komunikat do logów systemowych (Adventure Log).

### 10. Przeciążanie operatorów
W klasie `Potion` przeciążono operator dodawania: `public static Potion operator +(Potion a, Potion b)`. Wykorzystano to w mechanice "Craftingu" – łączenie dwóch mikstur w interfejsie faktycznie "dodaje" do siebie dwa obiekty i zwraca nową, potężniejszą miksturę.

### 11. Programowanie asynchroniczne
Operacje symulujące połączenie z bazą danych ("Auto-Load Starter Pack") oraz proces warzenia ("Combine Selected Potions") są asynchroniczne (`async` / `await`). Użyto `Task.Delay()` do symulacji upływu czasu, dzięki czemu interfejs użytkownika nie zawiesza się podczas trwania tych procesów.

### 12. Refleksja
Przycisk "Inspect Selected Item" wykorzystuje mechanizm refleksji. Program dynamicznie bada zaznaczony obiekt (nie znając z góry jego typu) używając m.in. `GetType()` oraz `GetProperties()`, aby wypisać w logach strukturę klasy bazowej, jej nazwę oraz listę wszystkich ukrytych pól i wartości w czasie rzeczywistym.
