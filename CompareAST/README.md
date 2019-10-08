Compare AST
===========

Abstract tree comparer and plagiarism detection tool. 
Currently implemented parser: C# parser adapter for Roslyn

Usage:

`compare-ast file1 file2 [-w [-s n]]`

`file1`  path to the first file to compare

`file2`  path to the second file to compare

`-w`    display similarities visually

`-s n`    set min similar subtree size (in nodes contained) not recommended to go below 6-7 as it will produce lots od type 1 errors
