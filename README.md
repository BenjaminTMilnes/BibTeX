# BibTeX

A C# library for reading, writing, and editing BibTeX bibliographic databases

## Primary Uses

### Representing BibTeX data as C# objects

This library contains a set of classes to represent the entries of a BibTeX database.
The BibTeXDatabase class can be used to store and filter BibTeXEntry objects by type or by citation key.

### Compiling bibliographic data and converting it to the BibTeX file format

BibTeXDatabase and BibTeXEntry objects can be converted into their text forms in a standard format, with options for
using quotation marks or recurve brackets around the field values, and for whether whitespace is added for readability.


