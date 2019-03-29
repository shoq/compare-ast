Compare AST
===========

Abstract tree comparer and plagiarism detection tool. 
Currently implemented parser: C# parser adapter for Roslyn

Usage:

compare-ast file1 file2 [-w [-s n]]

file1   sciezka do pierwszego pliku do porównania
file2   sciezka do drugiego pliku do porównania
-w      display similarities visually
-s n    set min similar subtree size (in nodes contained) not recommended to go below 6-7 as it will produce lots od type 1 errors