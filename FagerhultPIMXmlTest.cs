using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Fagerhult.Data.Xml;


namespace CommonTests
{
    /// <summary>
    /// Summary description for FagerhultPIMXmlTest
    /// </summary>
    [TestClass]
    public class FagerhultPIMXmlTest
    {
        const int TABLE_WIDTH = 100;

        public FagerhultPIMXmlTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;
        private FpkExport productData = null;
        List<FpkExport> files = new List<FpkExport>();
        private Produkt[] allProducts;
        private Artikel[] allArticles;
        private SokOrdGrupp[] allKeywordGroups;
        private SokOrd[] allKeywords;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EnumerateArticleProperties()
        {
            string path = @"d:\temp\Fagerhult\xml\140307\Noc_XML\1300\International_noc20140307081256.xml";

            XmlReader reader = new XmlTextReader(path);
            XDocument doc = XDocument.Load(reader);

            XmlNamespaceManager ns = new XmlNamespaceManager(reader.NameTable);
            ns.AddNamespace("fpk", "http://www.fagerhult.se/fpk");

            IEnumerable<XElement> articles = doc.Root.XPathSelectElements("//fpk:ArtikelLista/fpk:Artikel", ns);

            foreach (XElement article in articles)
            {
                XElement bestnr = article.Element(XName.Get("Bestnr", "http://www.fagerhult.se/fpk"));
                string[] settings = article.XPathSelectElements("fpk:EgenskapLista/fpk:Egenskap", ns)
                            .Select(e => e.Attribute("Typ").Value)
                            .ToArray();

                TestContext.WriteLine("{0}: {1}",
                    bestnr.Value,
                    string.Join(", ", settings)
                );

            }
        }

        [TestMethod]
        public void PrintDistinctProperties()
        {
            string[] paths = new string[] {
                @"d:\temp\Fagerhult\xml\140307\Noc_XML\1300\International_noc20140307081256.xml",
                @"d:\temp\Fagerhult\xml\140307\Noc_XML\1300\Sweden_noc20140307081300.xml",
                @"d:\temp\Fagerhult\xml\140307\Noc_XML\1228\International_noc20140307081224.xml",
                @"d:\temp\Fagerhult\xml\140307\Noc_XML\1228\Sweden_noc20140307081228.xml"
            };

            foreach (string path in paths)
            {
                XmlReader reader = new XmlTextReader(path);
                XDocument doc = XDocument.Load(reader);

                XmlNamespaceManager ns = new XmlNamespaceManager(reader.NameTable);
                ns.AddNamespace("fpk", "http://www.fagerhult.se/fpk");

                IEnumerable<XElement> articles = doc.Root.XPathSelectElements("//fpk:ArtikelLista/fpk:Artikel", ns);

                string[] properties = articles
                    .SelectMany(a => a.XPathSelectElements("fpk:EgenskapLista/fpk:Egenskap", ns)
                        .Select(e => e.Attribute("Typ").Value))
                    .Distinct()
                    .ToArray();

                TestContext.WriteLine("{0} ({1}) st: {2}",
                    Path.GetFileName(path),
                    articles.Count(),
                    string.Join(", ", properties));
            }
        }

        [TestMethod]
        [TestCategory("Fagerhult XML")]
        public void DeserializeXmlFolder()
        {
            string folder = @"d:\temp\Fagerhult\xml\Faro-140519";
            string[] fileNames = Directory.EnumerateFiles(folder, "*.xml").Select(s => Path.GetFileName(s)).ToArray();

            DeserializeXml(folder, fileNames);

        }

        [TestMethod]
        [TestCategory("Fagerhult XML")]
        public void DeserializeXmlFiles()
        {
            //string folder = @"d:\temp\Fagerhult\xml\Faro-140519";
            string folder = @"d:\temp\Fagerhult\xml\data (temp)";

            string[] fileNames = new string[]
            { 
                "Russia_Ryssland20140909113203.xml"
            };

            DeserializeXml(folder, fileNames);
        }

        public void DeserializeXml(string folder, string[] fileNames)
        {
            Console.WriteLine("====={0}=====", new string('=', TABLE_WIDTH));
            Console.WriteLine("===== {0} =====", folder);
            Console.WriteLine("====={0}=====", new string('=', TABLE_WIDTH));
            Console.WriteLine();
            foreach (string fileName in fileNames)
            {
                files.Add(FpkExport.Deserialize(Path.Combine(folder, fileName)));
            }

            foreach (FpkExport file in files)
            {
                Console.WriteLine("====={0}=====", new string('=', TABLE_WIDTH));
                Console.WriteLine("===== {0} {1}", file.FileName, new string('=', TABLE_WIDTH - file.FileName.Length + 3));
                Console.WriteLine("====={0}=====", new string('=', TABLE_WIDTH));
                productData = file;

                if (productData.SerieLista != null && productData.SerieLista.Serie != null)
                {
                    foreach (Serie serie in productData.SerieLista.Serie)
                    {
                        Console.WriteLine("{0,-14}{1,-8} {2}", "SERIE", serie.WebSerieId, serie.Namn);

                        IEnumerable<Produkt> products = productData.ProduktLista.Produkt
                            .Where(p => p.SerieKoppling == serie.Id);

                        foreach (Produkt product in products)
                        {
                            Console.WriteLine("  {0,-12}{1,-8} {2} ({3})", "PRODUKT ", product.WebProdId, product.Namn, product.SprakId);

                            PrintArticleTable(product);

                            PrintKeywords(product);

                            PrintAccessoryTable(product);

                            PrintDocumentTable(product);

                            Console.WriteLine();
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        private void PrintAccessoryTable(Produkt product)
        {
            IEnumerable<Produkt> allProducts = GetAllProducts();
            IEnumerable<Artikel> allArticles = GetAllArticles();

            IEnumerable<string> AccessoryIds = files
                .Where(f => f.ArtikelLista != null && f.ArtikelLista.Artikel != null)
                .SelectMany(f => f.ArtikelLista.Artikel)
                .Where(a => a.ProduktKoppling == product.Id && a.SprakId == product.SprakId && a.SprakId == product.SprakId)
                .SelectMany(a => a.TillbehorsKopplingar)
                .Distinct()
                .ToArray();

            var accessories = allArticles
                .Join(allProducts,
                    a => new { key1 = a.ProduktKoppling, key2 = a.SprakId },
                    p => new { key1 = p.Id, key2 = p.SprakId },
                    (a, p) => new ArtikelGroup() { ProductName = p.Namn, Article = a })
                .Where(g => g.Article.SprakId == product.SprakId && AccessoryIds.Any(nr => nr == g.Article.Id))
                .Distinct(new ArtikelGroupComparer())
                .OrderBy(a => a.Article.Bestnr).ToArray();

            if (accessories.Any())
            {
                Console.WriteLine();
                Console.WriteLine("===== Tillbehör till {0} {1} =====", product.WebProdId, product.Namn);
                PrintArticleTable(accessories);
            }
        }

        private void PrintArticleTable(Produkt product)
        {
            IEnumerable<Artikel> articles = productData.ArtikelLista.Artikel
                .Where(a => a.ProduktKoppling == product.Id)
                .OrderBy(a => a.Bestnr);

            PrintArticleTable(articles);
        }

        private void PrintArticleTable(IEnumerable<ArtikelGroup> articleGroups)
        {
            IEnumerable<string> productNames = articleGroups
                .OrderBy(g => g.Article.Bestnr)
                .Select(g => g.ProductName)
                .Distinct();

            foreach (string productName in productNames)
            {
                Console.WriteLine(productName);
                PrintArticleTable(articleGroups.Where(g => g.ProductName == productName).Select(g => g.Article));
                Console.WriteLine();
            }
        }

        private void PrintArticleTable(IEnumerable<Artikel> articles)
        {
            IEnumerable<HeaderInfo> headers = articles
                .SelectMany(a => a.EgenskapLista)
                .Select(p => p.Rubrik)
                .Distinct()
                .Select(h => new HeaderInfo()
                {
                    Header = h,
                    Size = articles
                        .SelectMany(a => a.EgenskapLista)
                        .Where(p => p.Rubrik == h)
                        .Max(p => p.Varde.Length > p.Rubrik.Length ? p.Varde.Length + 3 : p.Rubrik.Length + 3)
                });

            Console.WriteLine(new string('-', headers.Sum(h => h.Size) + 11));
            Console.Write("    {0,-10}", "Artnr");

            foreach (var header in headers)
            {
                Console.Write("{0,-" + header.Size.ToString() + "}", header.Header);
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', headers.Sum(h => h.Size) + 11));

            foreach (Artikel article in articles)
            {
                Console.Write("    {0,-10}", article.Bestnr);

                foreach (var header in headers)
                {
                    string value = article.EgenskapLista
                        .Where(p => p.Rubrik.Equals(header.Header, StringComparison.CurrentCultureIgnoreCase))
                        .Select(p => p.Varde)
                        .FirstOrDefault();

                    Console.Write("{0,-" + header.Size.ToString() + "}", value);
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', headers.Sum(h => h.Size) + 11));
        }

        private void PrintDocumentTable(Produkt product)
        {
            if (product.DokumentLista.Length == 0)
                return;

            int tableWidth = product.DokumentLista.Select(d => (d.Typ + Path.GetFileName(d.URI)).Length + 3).Max();

            Console.WriteLine(new string('-', tableWidth));
            Console.WriteLine("DOKUMENT för {0} {1}", product.WebProdId, product.Namn);
            Console.WriteLine(new string('-', tableWidth));

            int maxTypeLength = product.DokumentLista.Select(d => d.Typ.Length).Max();

            foreach (string ext in product.DokumentLista.Select(d => Path.GetExtension(d.URI)).Distinct().OrderBy(p => p))
            {
                foreach (ProduktDokument doc in product.DokumentLista.Where(d => Path.GetExtension(d.URI) == ext))
                {
                    Console.WriteLine("{0,-" + (maxTypeLength + 3).ToString() + "} {1}",
                        doc.Typ,
                        Path.GetFileName(doc.URI));
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', tableWidth));

        }

        private void PrintKeywords(Produkt product)
        {
            var tree = GetAllKeywordGroups().Join(
                GetAllKeywords(),
                g => new { key = g.SokOrdGruppTyp, lang = g.SprakId },
                k => new { key = k.SokOrdGruppId, lang = k.SprakId },
                (g, k) => new { Group = g, Keywords = k }
                )
                .Where(x => x.Group.SprakId == product.SprakId & product.SokordTypKopplingar.Any(k => x.Keywords.SokOrdTyp == k))
                .OrderBy(x => x.Group.SokOrdGruppTyp)
                .GroupBy(
                    x => x.Group, 
                    x => x.Keywords,
                    (x, y) => new { Group = x, Keywords = y.ToArray() },
                    new SokOrdGruppComparer());

            Console.WriteLine();
            Console.WriteLine("==== Keywords ====");

            foreach (var branch in tree)
            {
                Console.WriteLine("{0,-30} {1,-5} {2}", branch.Group.Namn, branch.Group.SprakId, branch.Group.SokOrdGruppTyp);
                foreach(var keyword in branch.Keywords)
                {
                    Console.WriteLine("    {0}", keyword.Namn);
                }
            }
        }

        private IEnumerable<Artikel> GetAllArticles()
        {
            if (allArticles == null)
                allArticles = files
                    .Where(f => f.ArtikelLista != null && f.ArtikelLista.Artikel != null)
                    .SelectMany(f => f.ArtikelLista.Artikel)
                    .ToArray();
            return allArticles;
        }

        private IEnumerable<Produkt> GetAllProducts()
        {
            if (allProducts == null)
                allProducts = files
                    .Where(f => f.ProduktLista != null && f.ProduktLista.Produkt != null)
                    .SelectMany(f => f.ProduktLista.Produkt)
                    .ToArray();
            return allProducts;
        }

        private IEnumerable<SokOrdGrupp> GetAllKeywordGroups()
        {
            if (allKeywordGroups == null)
                allKeywordGroups = files
                    .Where(f => f.SokordGrupperLista != null)
                    .SelectMany(f => f.SokordGrupperLista)
                    .Distinct(new SokOrdGruppComparer())
                    .ToArray();

            return allKeywordGroups;
        }

        private IEnumerable<SokOrd> GetAllKeywords()
        {
            if (allKeywords == null)
                allKeywords = files
                    .Where(f => f.SokordLista != null)
                    .SelectMany(f => f.SokordLista)
                    .Distinct(new SokOrdComparer())
                    .ToArray();

            return allKeywords;
        }

        private static T Max<T>(params T[] numbers)
        {
            return numbers.Max();
        }

        #region Help classes
        private class HeaderInfo
        {
            public string Header { get; set; }
            public int Size { get; set; }

        }
        private class ArtikelGroup
        {
            public string ProductName { get; set; }
            public Artikel Article { get; set; }
        }

        // Custom comparer for the ArtikelGroup class
        private class ArtikelGroupComparer : IEqualityComparer<ArtikelGroup>
        {
            // Products are equal if their product numbers are equal.
            public bool Equals(ArtikelGroup x, ArtikelGroup y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the products' properties are equal.
                return x.ProductName == y.ProductName && x.Article.Id == y.Article.Id;
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(ArtikelGroup articleGroup)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(articleGroup, null)) return 0;

                //Get hash code for the article id field.
                int hashCodeArticle = articleGroup.Article.Id.GetHashCode();

                //Get hash code for the product name.
                int hashCodeProductName = articleGroup.ProductName.GetHashCode();

                //Calculate the hash code for the product.
                return hashCodeArticle ^ hashCodeProductName;
            }
        }

        // Custom comparer for the Artikel class
        private class ArtikelComparer : IEqualityComparer<Artikel>
        {
            // Products are equal if their product numbers are equal.
            public bool Equals(Artikel x, Artikel y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the products properties are equal.
                return x.Id == y.Id && x.SprakId == y.SprakId;
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(Artikel article)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(article, null)) return 0;

                //Get hash code for the Code field.
                int hashCode = article.Id.GetHashCode();

                //Calculate the hash code for the product.
                return hashCode;
            }
        }

        // Custom comparer for the SokOrdGrupp class
        private class SokOrdGruppComparer : IEqualityComparer<SokOrdGrupp>
        {
            public bool Equals(SokOrdGrupp x, SokOrdGrupp y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the objects properties are equal.
                return x.SokOrdGruppTyp == y.SokOrdGruppTyp && x.SprakId == y.SprakId;
            }

            public int GetHashCode(SokOrdGrupp keywordGroup)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(keywordGroup, null)) return 0;

                //Get hash code for the Code field.
                int hashCode = keywordGroup.SokOrdGruppTyp.GetHashCode() ^ keywordGroup.SprakId.GetHashCode();

                //Calculate the hash code for the product.
                return hashCode;
            }
        }

        private class SokOrdComparer : IEqualityComparer<SokOrd>
        {
            public bool Equals(SokOrd x, SokOrd y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the objects properties are equal.
                return x.SokOrdTyp == y.SokOrdTyp && x.SprakId == y.SprakId;
            }

            public int GetHashCode(SokOrd obj)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(obj, null)) return 0;

                //Get hash code for the Code field.
                int hashCode = obj.SokOrdTyp.GetHashCode() ^ obj.SprakId.GetHashCode();

                //Calculate the hash code for the product.
                return hashCode;
            }
        }


        #endregion

    }

}
