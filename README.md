# BibTeX

A C# library for reading, writing, and editing BibTeX bibliographic databases

### How to ...

#### Create a new BibTeX entry

```C#

var article = new BibTeXArticle();
var book = new BibTeXBook();
var booklet = new BibTeXBooklet();
var conference = new BibTeXConference();
var inBook = new BibTeXInBook();
var inCollection = new BibTeXInCollection();
var inProceedings = new BibTeXInProceedings();
var manual = new BibTeXManual();
var mastersThesis = new BibTeXMastersThesis();
var miscellaneous = new BibTeXMiscellaneous();
var phdThesis = new BibTeXPhDThesis();
var proceedings = new BibTeXProceedings();
var techReport = new BibTeXTechReport();
var unpublished = new BibTeXUnpublished();

```

#### Create a new BibTeX database

```C#

var database = new BibTeXDatabase();

```

#### Add an entry to a database

```C#

database.Entries.Add(entry);

```

#### Get the book entries from a BibTeX database

```C#

var books = database.GetBooks();

```

#### Get the entry from a database with a given citation key

```C#

var entry = database.GetEntryByCitationKey("Milnes2017");

```