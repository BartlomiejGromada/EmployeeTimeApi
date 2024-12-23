# EmployeeTimeApi

EmployeeTimeApi to aplikacja napisana w oparciu o **Clean Architecture**, wykorzystująca **.NET 8**. Aplikacja ma na celu zarządzanie danymi dotyczącymi pracowników i ich czasu pracy.

## Struktura projektu
Projekt jest zorganizowany zgodnie z zasadami Clean Architecture, co umożliwia łatwe rozszerzanie aplikacji oraz utrzymanie wysokiej jakości kodu. Oto główne foldery w projekcie:

* src/ - zawiera główną logikę aplikacji:
  * EmployeeTimeApi.Application - logika biznesowa.
  * EmployeeTimeApi.Domain - modele i wyjątki.
  * EmployeeTimeApi.Infrastructure - implementacja dostępu do danych i innych zasobów 
    zewnętrznych.
  * EmployeeTimeApi.Presentation - API, czyli warstwa komunikacji z użytkownikiem
  * EmployeeTimeApi.Shared.Abstractions - wspólne abstrakcje, wykorzystywane przez inne 
   warstwy.
  * EmployeeTimeApi.Shared.Infrastructure - implementacja wspólnych mechanizmów (np. 
   logowanie, obsługa błędów).
* tests/ - testy aplikacji:
  * EmployeeTimeApi.Tests.Unit - testy jednostkowe.
  * EmployeeTimeApi.Tests.Integration - testy integracyjne

## Główne technologie
- **.NET 8** - główny framework użyty do budowy aplikacji.
- **Testcontainers** - narzędzie do uruchamiania baz danych (np. PostgreSQL) w kontenerach podczas testów.

## Wymagania

Aby uruchomić aplikację, potrzebujesz zainstalowanego:

- **Docker** - do uruchomienia kontenerów z bazą danych.
- **.NET SDK 8.0** - jeśli chcesz budować i uruchamiać aplikację lokalnie (opcjonalne, ponieważ Docker automatycznie zbuduje aplikację).
  
## Jak uruchomić aplikację?

1. **Sklonuj repozytorium**:

   Jeśli jeszcze nie masz lokalnej kopii, sklonuj repozytorium:

   ```bash
   git clone https://github.com/BartlomiejGromada/EmployeeTimeApi.git
   cd EmployeeTimeApi
2. **Sklonuj repozytorium**:

   W folderze EmployeeTimeApi uruchom poniższe polecenie, aby uruchomić aplikację i jej 
  zależności (np. bazę danych PostgreSQL) w kontenerach Docker.To polecenie zbuduje i 
  uruchomi aplikację oraz bazę danych PostgreSQL w kontenerach. Aplikacja będzie dostępna 
  pod adresem http://localhost:5001.

   ```bash
   docker-compose up
# Projekt EmployeeTimeApi

## 3. Uruchomienie skryptu tworzącego strukturę bazy danych

Aby utworzyć strukturę bazy danych, należy skorzystać z dowolnego narzędzia do zarządzania bazami danych. W moim przypadku użyłem **DBeaver**. Należy uruchomić przygotowany skrypt o nazwie **"script_database"**, który zawiera definicje bazy danych oraz odpowiednie uprawnienia.

### Dane dotyczące bazy danych:
- **Host**: localhost
- **Database**: `EmployeeTimeDb`
- **Username**: `postgres`
- **Password**: `postgres`

### Skrypt zawiera dwa konta użytkowników, które posiadają różne poziomy uprawnień:

- **Admin**:
  - **Email**: `admin@example.com`
  - **Hasło**: `zaq1@WSX`
  - **Uprawnienia**: Pełne uprawnienia do wszystkich funkcjonalności systemu.

- **User**:
  - **Email**: `employee@example.com`
  - **Hasło**: `zaq1@WSX`
  - **Uprawnienia**: Dostęp do swoich danych oraz rejestracji czasów pracy.

### Ważne informacje:
- Konto **Admin** ma pełne uprawnienia, umożliwiające zarządzanie wszystkimi funkcjami systemu.
- Konto **User** ma dostęp ograniczony do własnych danych oraz czasów pracy.
- Konto pracownika może zostać utworzone tylko przez użytkownika z uprawnieniami **Admin**.
- **Email** w tabeli `accounts` musi odpowiadać kolumnie **email** w tabeli `employees`(wtedy wiadomo, że pracownik ma dostęp tylko do swoich danych)

Skrypt powinien być uruchomiony w odpowiednim narzędziu SQL, po wcześniejszym połączeniu z bazą danych przy użyciu danych logowania.
