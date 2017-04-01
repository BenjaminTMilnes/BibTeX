using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibTeX
{
    public class BibTeXDatabase
    {
        public IList<IBibTeXEntry> Entries { get; set; }

        public BibTeXDatabase()
        {
            Entries = new List<IBibTeXEntry>();
        }

        #region GetEntryByCitationKey

        public IBibTeXEntry GetEntryByCitationKey(string citationKey)
        {
            return Entries.Single((entry) => entry.CitationKey == citationKey);
        }

        #endregion

        #region GetEntriesByType

        protected IEnumerable<IBibTeXEntry> GetEntriesByType<T>()
        {
            return Entries.Where((entry) => entry is T);
        }

        public IEnumerable<IBibTeXEntry> GetArticles()
        {
            return GetEntriesByType<BibTeXArticle>();
        }

        public IEnumerable<IBibTeXEntry> GetBooks()
        {
            return GetEntriesByType<BibTeXBook>();
        }

        public IEnumerable<IBibTeXEntry> GetBooklets()
        {
            return GetEntriesByType<BibTeXBooklet>();
        }

        public IEnumerable<IBibTeXEntry> GetConferences()
        {
            return GetEntriesByType<BibTeXConference>();
        }

        public IEnumerable<IBibTeXEntry> GetInBooks()
        {
            return GetEntriesByType<BibTeXInBook>();
        }

        public IEnumerable<IBibTeXEntry> GetInCollections()
        {
            return GetEntriesByType<BibTeXInCollection>();
        }

        public IEnumerable<IBibTeXEntry> GetInProceedings()
        {
            return GetEntriesByType<BibTeXInProceedings>();
        }

        public IEnumerable<IBibTeXEntry> GetManuals()
        {
            return GetEntriesByType<BibTeXManual>();
        }

        public IEnumerable<IBibTeXEntry> GetMastersTheses()
        {
            return GetEntriesByType<BibTeXMastersThesis>();
        }

        public IEnumerable<IBibTeXEntry> GetMiscellaneous()
        {
            return GetEntriesByType<BibTeXMiscellaneous>();
        }

        public IEnumerable<IBibTeXEntry> GetPhDTheses()
        {
            return GetEntriesByType<BibTeXPhDThesis>();
        }

        public IEnumerable<IBibTeXEntry> GetProceedings()
        {
            return GetEntriesByType<BibTeXProceedings>();
        }

        public IEnumerable<IBibTeXEntry> GetTechReports()
        {
            return GetEntriesByType<BibTeXTechReport>();
        }

        public IEnumerable<IBibTeXEntry> GetUnpublished()
        {
            return GetEntriesByType<BibTeXUnpublished>();
        }

        #endregion
    }
}
