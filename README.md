# 🖥️ IntelloDesk Client (WPF, MVP)

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

To repozytorium zawiera **MVP aplikacji desktopowej** zbudowanej w technologii **WPF (.NET 8)** zgodnie z modelem **MVVM**. Aplikacja działa jako klient REST API magazynowego dostępnego publicznie.

## 🔗 Połączenie z API

Aplikacja łączy się z publicznym API pod adresem:

🌐 https://qubity.azurewebsites.net/api/

Dokumentacja API (Swagger):  
📄 https://qubity.azurewebsites.net/swagger

🛠️ Wersja źródłowa API
Kod źródłowy API jest dostępny na GitHubie: 📦 intelloAPI

> ℹ️ Jeśli chcesz uruchomić API lokalnie, pamiętaj, że adres API jest zahardkodowany w pliku config.cs. Zmień go ręcznie, by wskazywał na lokalne środowisko (np. https://localhost:5001/api/).

## 📦 Technologia i biblioteki

- .NET 8
- WPF (.NET Desktop)
- MVVM Toolkit (CommunityToolkit.Mvvm 8.4.0)
- HttpClient
- Newtonsoft.Json / System.Text.Json

## 📁 Architektura klienta

```
├── Views/                  # Widoki XAML (UserControls)
├── ViewModels/            # Logika prezentacji (MVVM)
├── DTO/                   # Obiekty DTO zgodne z API
├── Services/              # Komunikacja z REST API
├── MainView.xaml          # Główne okno z dynamicznym wczytywaniem widoków
```

## 📌 Główne funkcje aplikacji

- 📋 Lista dokumentów przyjęcia
- 📝 Szczegóły dokumentu (symbol, data, kontrahent, pozycje)
- ➕ Tworzenie dokumentu z pierwszą pozycją
- ➕ Dodawanie pozycji do dokumentu
- ➕ Dodawanie towarów
- 🧾 Lista kontrahentów
- 📦 Lista towarów

## 🧠 Sposób działania

- Użytkownik uruchamia aplikację desktopową.
- Z ekranu głównego wybiera jeden z widoków: Dokumenty, Kontrahenci.
- Po wybraniu dokumentu może zobaczyć jego szczegóły oraz dodać nowe pozycje.
- Wszystkie operacje wykonują zapytania HTTP do REST API.

## 🧪 Uruchomienie lokalne

1. Wymagania:

   - .NET 8 SDK
   - Visual Studio 2022+ z obsługą .NET Desktop (WPF)

2. Uruchom:

```bash
dotnet restore
dotnet build
dotnet run
```

## ⚠️ Uwagi

> Projekt jest w wersji **MVP**, co oznacza:
>
> - interfejs użytkownika jest prosty i testowy
> - komunikacja z API odbywa się bez pełnej obsługi błędów
> - aplikacja skupia się na funkcjonalności, nie stylistyce

---

This project is licensed under the GNU General Public License v3.0 – see the LICENSE file for details.
