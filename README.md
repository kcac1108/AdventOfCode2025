# Advent of Code 2025 Solutions

This repository contains solutions for the [Advent of Code 2025](https://adventofcode.com/2025) challenges, implemented in C#.

## Project Structure

The project is organized into a `Days` directory, which contains:
- `DayXX/`: Folders for each day's solution (e.g., `Day01`, `Day02`).
- `Program.cs`: The entry point that automatically finds and runs all implemented solutions using reflection.
- `ISolution.cs`: Interface and base class for solutions.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (or compatible version).

## Setup

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd AdvOfCode2025
   ```

2. **Input Files:**
   The solutions expect input files to be located in a directory named `Input Files` within the `Days` directory. This directory is ignored by git to avoid committing personal puzzle inputs.

   - Create the directory:
     ```bash
     mkdir "Days/Input Files"
     ```
   - Add your input files as text files named after the day (e.g., `01.txt` for Day 1) inside `Days/Input Files/`.

   Example structure:
   ```
   Days/
   ├── Input Files/
   │   ├── 01.txt
   │   └── ...
   ├── Day01/
   ├── ...
   ```

## How to Run

To run all implemented solutions, navigate to the `Days` directory and use `dotnet run`:

```bash
cd Days
dotnet run
```

Alternatively, from the root directory:

```bash
dotnet run --project Days
```

The program will output the results for Part 1 and Part 2 of each day found.

## Adding a New Day

1. Create a new folder `DayXX` inside `Days/`.
2. Create a `Solution.cs` class that inherits from `BaseSolution` (or implements `ISolution`).
3. Implement `RunPart1()` and `RunPart2()`.
4. Ensure the namespace follows the convention `Days.DayXX`.

The `Program.cs` will automatically pick up the new solution.
